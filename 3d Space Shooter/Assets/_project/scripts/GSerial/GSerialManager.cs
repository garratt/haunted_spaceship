/**
 * Ardity (Serial Communication for Arduino + Unity)
 * Author: Daniel Wilches <dwilches@gmail.com>
 *
 * This work is released under the Creative Commons Attributions license.
 * https://creativecommons.org/licenses/by/2.0/
 */

using UnityEngine;
using System.Collections.Generic;
using System.IO.Ports;

/**
 * This class allows a Unity program to continually check for messages from a
 * serial device.
 *
 * It creates a Thread that communicates with the serial port and continually
 * polls the messages on the wire.
 * That Thread puts all the messages inside a Queue, and this SerialController
 * class polls that queue by means of invoking SerialThread.GetSerialMessage().
 *
 * The serial device must send its messages separated by a newline character.
 * Neither the SerialController nor the SerialThread perform any validation
 * on the integrity of the message. It's up to the one that makes sense of the
 * data.
 */
public class GSerialManager : MonoBehaviour
{

    [Tooltip("Baud rate that the serial device is using to transmit data.")]
    public int baudRate = 9600;

    [Tooltip("Reference to an scene object that will receive the events of connection, " +
             "disconnection and the messages from the serial device.")]
    public GameObject messageListener;
    TurretHandler _TurretHandler => GetComponent<TurretHandler>();


    // Constants used to mark the start and end of a connection. There is no
    // way you can generate clashing messages from your serial device, as I
    // compare the references of these strings, no their contents. So if you
    // send these same strings from the serial device, upon reconstruction they
    // will have different reference ids.
    public const string SERIAL_DEVICE_CONNECTED = "__Connected__";
    public const string SERIAL_DEVICE_DISCONNECTED = "__Disconnected__";

    // Internal reference to the Thread and the object that runs in it.
    private List<GSerialThread> serialThreads = new List<GSerialThread>();


    // ------------------------------------------------------------------------
    // Invoked whenever the SerialController gameobject is activated.
    // It creates a new thread that tries to connect to the serial device
    // and start reading from it.
    // ------------------------------------------------------------------------
    void ConnectIfNew(string portName)
    {

        // Debug.Log("Should we connect to: " + portName);
        foreach (var ser in  serialThreads) {
            if (ser.portName == portName) {
                return;
            }
        }
        // New port! we shall connect.
        serialThreads.Add(new GSerialThread(portName, baudRate));
    }

    // ------------------------------------------------------------------------
    // Invoked whenever the SerialController gameobject is deactivated.
    // It stops and destroys the thread that was reading from the serial device.
    // ------------------------------------------------------------------------
    void OnDisable()
    {
        foreach (var ser in  serialThreads) {
            ser.RequestStop();
        }
    }

    // ------------------------------------------------------------------------
    // Polls messages from the queue that the SerialThread object keeps. Once a
    // message has been polled it is removed from the queue. There are some
    // special messages that mark the start/end of the communication with the
    // device.
    // ------------------------------------------------------------------------
    void Update()
    {
        // Check for new ports:
        string[] ports = SerialPort.GetPortNames();
        foreach (string port in ports) {
            if (port.Contains("USB") || port.Contains("ACM")) {
                // Debug.Log($"Found port {port}");
                ConnectIfNew(port);
            }
        }

        // Now get messages from each port:
        foreach (var ser in  serialThreads) {
            string message = (string)ser.ReadMessage();
            if (message != null) {
                ParseMessage(message, ser);
                ser.SendMessage("1");
            }
        }
        if (Input.GetMouseButton(1)) {
            EnableElights(true);
        }
    }

    void ParseMessage(string message, GSerialThread ser) {
        Debug.Log("Arrived: " + message);
        string[] fields = message.Split(',');
        // First field is the id, second is the message type
        if (fields[0] == "A1") {
            ser.board_function = BoardFunction.Elights;
            _TurretHandler.ParseTurretMessage(message);
        }
    }

    // ------------------------------------------------------------------------
    // Puts a message in the outgoing queue. The thread object will send the
    // message to the serial device when it considers it's appropriate.
    // ------------------------------------------------------------------------
    public void SendSerialMessage(BoardFunction board_function, string message)
    {
        foreach (var ser in  serialThreads) {
            if (ser.board_function == board_function) {
                ser.SendMessage(message);
            }
        }
    }

    public void EnableElights(bool enable) {
        SendSerialMessage(BoardFunction.Elights, enable ? "E1" : "E0");
    }

}


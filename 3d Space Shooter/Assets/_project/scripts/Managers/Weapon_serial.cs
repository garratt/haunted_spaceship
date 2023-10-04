using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;

public class Weapon_serial : MonoBehaviour
{
    [SerializeField] Transform _turret1;

    private SerialController serialController; 
    private Blaster blaster1;
    private float _blaster1_yaw = 0;
    // Start is called before the first frame update
    void Start()
    {
     serialController = GameObject.Find("GameManager").GetComponent<SerialController>();
     blaster1 = GameObject.Find("Canon").GetComponent<Blaster>();
    }

    // Update is called once per frame
    void Update()
    {
     string[] ports = SerialPort.GetPortNames();
     foreach (string port in ports) {
        if (port.Contains("USB") || port.Contains("ACM")) {
            Debug.Log($"Found port {port}");
        }
     }
     _turret1.localRotation = Quaternion.Euler(0, _blaster1_yaw, 0);
    }

    void ParseMessage(string msg) {
        string[] fields = msg.Split(',');
        // First field is the id, second is the message type
        if (fields[0] == "A1") {
            //two message types: control, 'C', and data 'D'
            if (fields[1] == "D") {
                // Expect 3 fields: pot, button1, button2
                if (fields.Length != 5) {
                    Debug.Log("Failed to parse message: " + msg);
                }
                int pot, button1, button2;
                if (!int.TryParse(fields[2], out pot)) { Debug.Log("Failed to parse A1 Data: " + msg); }
                if (!int.TryParse(fields[3], out button1)) { Debug.Log("Failed to parse A1 Data: " + msg); }
                if (!int.TryParse(fields[4], out button2)) { Debug.Log("Failed to parse A1 Data: " + msg); }
                _blaster1_yaw = (pot - 512f)/4.0f;
                if(button1 > 0) {
                    blaster1.FireProjectile();
                }
            }
        }
    }

    void OnMessageArrived(string msg) {
        Debug.Log("Arrived: " + msg);
        ParseMessage(msg);
        serialController.SendSerialMessage("1");
    }

    void OnConnectionEvent(bool success) {
        Debug.Log(success ? "Device Connected" : "Device Disconnected");
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TurretHandler : MonoBehaviour
{
    // UnityEvent _TurretFiredEvent;
    public float TurretAngle { get; private set; }

    public UnityEvent TurretFired;

    public void ParseTurretMessage(string message) {
                            Debug.Log("ParseTurretMessage: " + message);

        string[] fields = message.Split(',');
        // First field is the id, second is the message type
        if (fields[0] == "A1") {
            //two message types: control, 'C', and data 'D'
            if (fields[1] == "D") {
                // Expect 3 fields: pot, button1, button2
                if (fields.Length < 5) {
                    Debug.Log("Failed to parse message: " + message);
                }
                int pot, button1, button2;
                if (!int.TryParse(fields[2], out pot)) { Debug.Log("Failed to parse A1 Data: " + message); }
                if (!int.TryParse(fields[3], out button1)) { Debug.Log("Failed to parse A1 Data: " + message); }
                if (!int.TryParse(fields[4], out button2)) { Debug.Log("Failed to parse A1 Data: " + message); }
                TurretAngle = (pot - 512f)/4.0f;
                if(button1 > 0) {

                    Debug.Log("Turret Fired");
                    TurretFired.Invoke();
                }
            }
        }
    }

}

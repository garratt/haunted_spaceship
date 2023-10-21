using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TurretHandler : AbstractHandlerBase
{
    // UnityEvent _TurretFiredEvent;
    public float TurretAngle { get; private set; }

    public UnityEvent TurretFired;
    protected override int numFields => 2;
    protected override string BoardIdentifier => "TURR";

    protected override void ParseMessageData(int[] data) {
        // Field 0: Turret pot
        // Field 1: Fire button
        TurretAngle = (data[0] - 512f)/4.0f;
        if(data[1] > 0) {
            Debug.Log("Turret Fired");
            TurretFired.Invoke();
        }
    }
    // public void ParseTurretMessage(string message) {
    //                         Debug.Log("ParseTurretMessage: " + message);

    //     string[] fields = message.Split(',');
    //     // First field is the id, second is the message type
    //     if (fields[0] == "TURR") {
    //         //two message types: control, 'C', and data 'D'
    //         if (fields[1] == "D") {
    //             // Expect 3 fields: pot, button1, button2
    //             if (fields.Length < 5) {
    //                 Debug.Log("Failed to parse message: " + message);
    //             }
    //             int pot, button1, button2;
    //             if (!int.TryParse(fields[2], out pot)) { Debug.Log("Failed to parse A1 Data: " + message); }
    //             if (!int.TryParse(fields[3], out button1)) { Debug.Log("Failed to parse A1 Data: " + message); }
    //             if (!int.TryParse(fields[4], out button2)) { Debug.Log("Failed to parse A1 Data: " + message); }
    //             TurretAngle = (pot - 512f)/4.0f;
    //             if(button1 > 0) {

    //                 Debug.Log("Turret Fired");
    //                 TurretFired.Invoke();
    //             }
    //         }
    //     }
    // }

}

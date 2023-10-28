using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MissileHandler  : AbstractHandlerBase
{
    public UnityEvent MissileFired, Missile2Fired, BigButtonPress;
    protected override int numFields => 5;
    protected override string BoardIdentifier => "MISS";

    protected override void ParseMessageData(int[] data) {
        // Field 0: Big Button
        // Field 1: Auto1 button
        // Field 2: Auto2 button
        // Field 3: Fire1 button
        // Field 4: Fire2 button
        if(data[0] > 0) {
            Debug.Log("Big Button");
            BigButtonPress.Invoke();
        }
        if(data[3] > 0) {
            Debug.Log("Turret Fired");
            MissileFired.Invoke();
        }
        if(data[4] > 0) {
            Debug.Log("Turret Fired");
            Missile2Fired.Invoke();
        }
    }

}
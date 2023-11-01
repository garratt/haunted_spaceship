using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NavControlHandler  : AbstractHandlerBase
{
    [SerializeField] float _lightspeed1_trigger_on, _lightspeed1_trigger_off;
    public UnityEvent LightspeedEngage, LightspeedDisengage;
    public float ThrustAmount { get; private set; }

    protected override int numFields => 3;
    protected override string BoardIdentifier => "NAVC";

    bool _lightspeed_engaged = true;
    protected override void ParseMessageData(int[] data) {
        // Field 0: thrust
        // Field 1: lightspeed1
        // Field 2: lightspeed2
        ThrustAmount = data[0];

        Debug.Log("Lightspeed: " + data[1] + ",  " + data[2] + "  Thrust: " + data[0]);

        if(data[1] > _lightspeed1_trigger_on && !_lightspeed_engaged) {
            Debug.Log("Lightspeed engaged");
            LightspeedEngage.Invoke();
            _lightspeed_engaged = true;
        }
        if(data[1] < _lightspeed1_trigger_off && _lightspeed_engaged) {
            Debug.Log("Lightspeed disengage");
            LightspeedDisengage.Invoke();
            _lightspeed_engaged = false;
        }
    }

}
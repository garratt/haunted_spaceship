using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesktopMovementControls : MovementControlsBase
{
    [SerializeField] float _deadZoneRadius = .1f;

    Vector2 ScreenCenter => new Vector2(Screen.width*.5f, Screen.height *.5f);
   public override float YawAmount {
        get
        {
        if (Input.GetKey(KeyCode.A)) {
            return 1f;
        }
        return Input.GetKey(KeyCode.D) ? -1f : 0f;
        }
    }
   public override float PitchAmount {
        get
        {
        if (Input.GetKey(KeyCode.W)) {
            return 1f;
        }
        return Input.GetKey(KeyCode.S) ? -1f : 0f;
        }  
   }
   public override float RollAmount {
    get
    {
        if (Input.GetKey(KeyCode.Q)) {
            return 1f;
        }
        return Input.GetKey(KeyCode.E) ? -1f : 0f;
    }
   }
   public override float ThrustAmount  {
    get
    {
        if (Input.GetKey(KeyCode.Period)) {
            return 1f;
        }
        return Input.GetKey(KeyCode.Slash) ? -1f : 0f;
    }
   }

}

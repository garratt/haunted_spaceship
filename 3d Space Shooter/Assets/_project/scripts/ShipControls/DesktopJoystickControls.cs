using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesktopJoystickControls : MovementControlsBase
{
    [SerializeField] float _deadZoneRadius = .1f;

    Vector2 ScreenCenter => new Vector2(Screen.width * .5f, Screen.height * .5f);
    public override float YawAmount => Input.GetAxis("Yaw");

    public override float PitchAmount => Input.GetAxis("Vertical");
    public override float RollAmount => Input.GetAxis("Horizontal");

    public override float ThrustAmount => Input.GetAxis("Thrust");

}

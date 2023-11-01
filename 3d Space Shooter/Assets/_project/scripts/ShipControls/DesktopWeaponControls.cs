using UnityEngine;

public class DesktopWeaponControls : WeaponControlsBase
{
    public override bool PrimaryFired => Input.GetKey("joystick button 0");


    public override bool SecondaryFired => Input.GetButtonDown("Fire2");
}
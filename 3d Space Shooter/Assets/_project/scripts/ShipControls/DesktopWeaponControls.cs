using UnityEngine;

public class DesktopWeaponControls : WeaponControlsBase
{
    public override bool PrimaryFired => Input.GetButton("Fire1");

    public override bool SecondaryFired => Input.GetButtonDown("Fire2");
}
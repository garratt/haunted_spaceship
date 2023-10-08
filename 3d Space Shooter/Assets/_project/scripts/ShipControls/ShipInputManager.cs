using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.Reflection.Editor;
using UnityEngine;

public class ShipInputManager : MonoBehaviour
{
    public enum InputType {
        HumanDesktop,
        HumanMobile,
        Bot
    }

    public static ImovementControls GetMovementControls(InputType inputType) {
        return inputType switch {
            InputType.HumanDesktop => new DesktopMovementControls(),
            InputType.HumanMobile => null,
            InputType.Bot => null,
            _ => throw new ArgumentOutOfRangeException(nameof(inputType), inputType, null),
        };

    }
        public static IWeaponControls GetWeaponControls(InputType inputType) {
        return inputType switch {
            InputType.HumanDesktop => new DesktopWeaponControls(),
            InputType.HumanMobile => null,
            InputType.Bot => null,
            _ => throw new ArgumentOutOfRangeException(nameof(inputType), inputType, null),
        };

    }

}

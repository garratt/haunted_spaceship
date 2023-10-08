using UnityEngine;

public class ShipInputControls : MonoBehaviour
{
    [SerializeField] ShipInputManager.InputType _inputType = ShipInputManager.InputType.HumanDesktop;

    public ImovementControls MovementControls {get; private set;}
    public IWeaponControls WeaponControls {get; private set;}

    // Start is called before the first frame update
    void Start()
    {
        MovementControls = ShipInputManager.GetMovementControls(_inputType);
        WeaponControls = ShipInputManager.GetWeaponControls(_inputType);
        
    }

    // Update is called once per frame
    void OnDestroy()
    {
        MovementControls = null;
        WeaponControls = null;
        
    }
}

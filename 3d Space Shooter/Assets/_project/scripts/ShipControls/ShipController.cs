using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    [InlineEditor(InlineEditorObjectFieldModes.Boxed)]
    [SerializeField] [Required] ShipInputControls _inputControls;

    [BoxGroup("Ship Movement Values")][SerializeField] [Range(1000f, 10000f)]
    float _thrustForce = 7500f,
    _pitchForce = 6000f,
    _yawForce = 2000f,
    _rollForce = 1000f;

    [BoxGroup("Ship Components")]    [SerializeField]
     private List<Blaster> _blasters;
    
    Rigidbody _rigidbody;
    
    [ShowInInspector][Range(-1.0f,1.0f)]
    float _thrustAmount, _pitchAmount, _yawAmount, _rollAmount;
    
    ImovementControls MovementInput => _inputControls.MovementControls;
    IWeaponControls WeaponInput => _inputControls.WeaponControls;

    void Start() {
        foreach (Blaster blaster in _blasters) {
            blaster.Init(WeaponInput);
        }
    }

    void Awake() {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update() {
        _thrustAmount = MovementInput.ThrustAmount;
        _pitchAmount = MovementInput.PitchAmount;
        _rollAmount = MovementInput.RollAmount;
        _yawAmount = MovementInput.YawAmount;
    }

    void FixedUpdate() {
        if(!Mathf.Approximately(0f, _pitchAmount)) {
            _rigidbody.AddTorque(transform.right * (_pitchAmount * _pitchForce * Time.fixedDeltaTime));
        }
        if(!Mathf.Approximately(0f, _rollAmount)) {
            _rigidbody.AddTorque(transform.forward * (_rollAmount * _rollForce * Time.fixedDeltaTime));
        }       
        if(!Mathf.Approximately(0f, _yawAmount)) {
            _rigidbody.AddTorque(transform.up * (_yawAmount * _yawForce * Time.fixedDeltaTime));
        }       
        if(!Mathf.Approximately(0f, _thrustAmount)) {
            _rigidbody.AddForce(transform.forward * (_thrustAmount * _thrustForce * Time.fixedDeltaTime));
        }       
    }



}

using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class ShipController : MonoBehaviour
{

    [SerializeField]
    protected MovementControlsBase _movementControls;

    [SerializeField]
    protected WeaponControlsBase _weaponControls;   

    [SerializeField]
    ShipDataSo _shipData;

    [SerializeField]
    List<Blaster> _blasters;
    
    // [SerializeField] protected List<MissileLauncher> _missileLaunchers;

    Rigidbody _rigidBody;
    [Range(-1.0f,1.0f)]
    float _thrustAmount, _pitchAmount, _yawAmount, _rollAmount;
    
    ImovementControls MovementInput => _movementControls;
    IWeaponControls WeaponInput => _weaponControls;

    void Start() {
        foreach (Blaster blaster in _blasters) {
            blaster.Init(WeaponInput);
        }
    }

    void Awake() {
        _rigidBody = GetComponent<Rigidbody>();
    }

    void Update() {
        _thrustAmount = MovementInput.ThrustAmount;
        _pitchAmount = MovementInput.PitchAmount;
        _rollAmount = MovementInput.RollAmount;
        _yawAmount = MovementInput.YawAmount;
    }

    void FixedUpdate() {
                if (!Mathf.Approximately(0f, _pitchAmount))
        {
            _rigidBody.AddTorque(transform.right * (_shipData.PitchForce * _pitchAmount * Time.fixedDeltaTime));
        }

        if (!Mathf.Approximately(0f, _rollAmount))
        {
            _rigidBody.AddTorque(transform.forward * (_shipData.RollForce * _rollAmount * Time.fixedDeltaTime));
        }

        if (!Mathf.Approximately(0f, _yawAmount))
        {
            _rigidBody.AddTorque(transform.up * (_yawAmount * _shipData.YawForce * Time.fixedDeltaTime));
        }       
        if(!Mathf.Approximately(0f, _thrustAmount)) {
            _rigidBody.AddForce(transform.forward * (_thrustAmount * _shipData.ThrustForce * Time.fixedDeltaTime));
        }       
    }



}

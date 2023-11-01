using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using Sirenix.OdinInspector;
using UnityEngine;

public class ShipController : MonoBehaviour
{
 [SerializeField] AudioClip _alertSound;
    
    [SerializeField]
    protected MovementControlsBase _movementControls;

    [SerializeField]
    protected WeaponControlsBase _weaponControls;   
         [SerializeField] GameObject _asteroidPrefab;
     [SerializeField] float _astroid_distance;

    [SerializeField] float _game_boundary = 500f;
    [SerializeField]
    ShipDataSo _shipData;

    [SerializeField]
    List<Blaster> _blasters;
    AudioSource _audioSource;
    
    [SerializeField] protected List<MissileLauncher> _missileLaunchers;

    Rigidbody _rigidBody;
    [Range(-1.0f,1.0f)]
    float _thrustAmount, _pitchAmount, _yawAmount, _rollAmount;
    ImovementControls MovementInput => _movementControls;
    IWeaponControls WeaponInput => _weaponControls;

    void Start() {
        _audioSource = SoundManager.Configure3DAudioSource(GetComponent<AudioSource>());

        foreach (Blaster blaster in _blasters) {
            blaster.Init(WeaponInput);
        }
        foreach (MissileLauncher launcher in _missileLaunchers)
        {
            launcher.Init(WeaponInput);
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


    //  if (Input.GetKeyDown(KeyCode.H))
    //     {
    //         GameObject asteroid = Instantiate(_asteroidPrefab,
    //            transform.position + transform.forward * _astroid_distance, Quaternion.identity);
    //         var aster = asteroid.GetComponent<Asteroid>();
    //         // asteroid.gameObject.SetActive(true);
    //         aster.Init(transform);

    //     }
    }
    void OnCollisionEnter(Collision other)
    {
        if (_alertSound) _audioSource.PlayOneShot(_alertSound);

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

        if (Math.Abs(transform.position.x) > _game_boundary || Math.Abs(transform.position.y) > _game_boundary || Math.Abs(transform.position.z) >_game_boundary) {
            var temp_position = transform.position;
            if(transform.position.x > _game_boundary) temp_position.x -= _game_boundary*2f;
            if(transform.position.x < -_game_boundary) temp_position.x += _game_boundary*2f;
            if(transform.position.y > _game_boundary) temp_position.y -= _game_boundary*2f;
            if(transform.position.y < -_game_boundary) temp_position.y += _game_boundary*2f;
            if(transform.position.z > _game_boundary) temp_position.z -= _game_boundary*2f;
            if(transform.position.z < -_game_boundary) temp_position.z += _game_boundary*2f;
            _rigidBody.position = temp_position;
        }
        
    }



}

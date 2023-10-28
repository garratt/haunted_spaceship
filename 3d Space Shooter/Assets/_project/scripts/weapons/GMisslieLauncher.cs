using System;
using System.Collections;
using System.Collections.Generic;
//using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GMisslieLauncher : MonoBehaviour
{
     [SerializeField] GameObject _missilePrefab;
    [SerializeField] AudioClip _fireSound;
    [SerializeField] Transform _muzzle;
    [SerializeField] [Range(0f, 5f)] float _coolDownTime = 1.5f;

    Transform _transform, _target;
    private IWeaponControls _weaponInput;
    
    bool CanFire {
        get {
            _coolDown -= Time.deltaTime;
            return _coolDown <= 0f;
        }
    }

    float _coolDown;

    AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = SoundManager.Configure3DAudioSource(GetComponent<AudioSource>());
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_coolDown > 0 ) {
            _coolDown -= Time.deltaTime;
        }
        if (_weaponInput != null && _weaponInput.SecondaryFired)
        {
            FireMissile();
        }
        // if (_weaponInput != null && _weaponInput is {SecondaryFired: true})
        // {
        //     FireMissile();
        // }
        
    }

    void Awake()
    {

        _transform = transform;
    }

    public void FireMissile() 
    {
        if (_coolDown > 0) {
            return;
        }
        _coolDown = _coolDownTime;
        if (_fireSound)
        {
            _audioSource.PlayOneShot(_fireSound);
        }
        var missile = Instantiate(_missilePrefab, _transform.position, _transform.rotation).GetComponent<Missile>();
        // if (_radarScreen)
        // {
        //     missile.Init(_target ? _target : _radarScreen.LockedOnTarget);
        // }
        
        missile.gameObject.SetActive(true);
                Debug.Log($"Launched Missle");
    }


    internal void Init(IWeaponControls weaponInput)
    {
        _weaponInput = weaponInput;
    }
}

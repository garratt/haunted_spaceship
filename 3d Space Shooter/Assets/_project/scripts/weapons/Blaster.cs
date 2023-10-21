using System;
using System.Collections;
using System.Collections.Generic;
//using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Blaster : MonoBehaviour
{
    [SerializeField] Projectile _projectilePrefab;
    [SerializeField] AudioClip _fireSound;
    [SerializeField] Transform _muzzle;
    [SerializeField] [Range(0f, 5f)] float _coolDownTime = .25f;

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
        if (_weaponInput != null && _weaponInput.PrimaryFired)
        {
            FireProjectile();
        }
        
    }

    void Awake()
    {
    }

    public void FireProjectile() 
    {
        if (_coolDown > 0) {
            return;
        }
        _coolDown = _coolDownTime;
        if (_fireSound)
        {
            _audioSource.PlayOneShot(_fireSound);
        }
        Instantiate(_projectilePrefab, _muzzle.position, transform.rotation);
    }

    internal void Init(IWeaponControls weaponInput)
    {
        _weaponInput = weaponInput;
    }
}

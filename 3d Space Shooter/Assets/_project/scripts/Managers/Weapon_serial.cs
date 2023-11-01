using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_serial : MonoBehaviour
{
    [SerializeField] Transform _turret1;
    [SerializeField] TurretHandler _turretHandler;
    [SerializeField] MissileHandler _missileHandler;
        [SerializeField] AudioClip _destructSound;

 AudioSource _audioSource;
  
    // private TurretHandler _turretHandler;
    private Blaster blaster1;
    private MissileLauncher ml1, ml2;
    float time_since_last_destruct=0;
    void Start()
    {
        _audioSource = SoundManager.Configure3DAudioSource(GetComponent<AudioSource>());    
    //  _turretHandler = GameObject.Find("SerialManager").GetComponent<TurretHandler>();
     blaster1 = GameObject.Find("Canon").GetComponent<Blaster>();
     ml1 = GameObject.Find("Big_Launcher2").GetComponent<MissileLauncher>();
     ml2 = GameObject.Find("Big_Launcher1").GetComponent<MissileLauncher>();
    }
    void OnEnable()
    {
        _turretHandler.TurretFired.AddListener(OnTurretFired);
        _missileHandler.MissileFired.AddListener(OnMissileFired);
        _missileHandler.Missile2Fired.AddListener(OnMissile2Fired);
        _missileHandler.BigButtonPress.AddListener(OnBigButtonPress);
    }

    void OnDisable()
    {
        _turretHandler.TurretFired.RemoveListener(OnTurretFired);
    }

    void OnTurretFired()
    {
        Debug.Log("OnTurretFire");

        blaster1.FireProjectile();
    }
    void OnBigButtonPress() {
        if (time_since_last_destruct > 30) {
          if (_destructSound) _audioSource.PlayOneShot(_destructSound);
          time_since_last_destruct = 0;
        }
    }
    void OnMissileFired() {
        ml1.FireMissile();
    }
    void OnMissile2Fired() {
        ml2.FireMissile();
    }

    // Update is called once per frame
    void Update()
    {
        time_since_last_destruct += Time.deltaTime;
    //  _turret1.localRotation = Quaternion.Euler(0, _turretHandler.TurretAngle, 0);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_serial : MonoBehaviour
{
    [SerializeField] Transform _turret1;
    [SerializeField] TurretHandler _turretHandler;
    [SerializeField] MissileHandler _missileHandler;


    // private TurretHandler _turretHandler;
    private Blaster blaster1;
    private MissileLauncher ml1, ml2;
    void Start()
    {
    //  _turretHandler = GameObject.Find("SerialManager").GetComponent<TurretHandler>();
     blaster1 = GameObject.Find("Canon").GetComponent<Blaster>();
     ml1 = GameObject.Find("Big_Launcher1").GetComponent<MissileLauncher>();
     ml2 = GameObject.Find("Big_Launcher2").GetComponent<MissileLauncher>();
    }
    void OnEnable()
    {
        _turretHandler.TurretFired.AddListener(OnTurretFired);
        _missileHandler.MissileFired.AddListener(OnMissileFired);
        // _missileHandler.Missile2Fired.AddListener(OnMissile2Fired);
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
    void OnMissileFired() {
        ml1.FireMissile();
    }

    // Update is called once per frame
    void Update()
    {
     _turret1.localRotation = Quaternion.Euler(0, _turretHandler.TurretAngle, 0);
    }

}

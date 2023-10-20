using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_serial : MonoBehaviour
{
    [SerializeField] Transform _turret1;
    [SerializeField] TurretHandler _turretHandler;


    // private TurretHandler _turretHandler;
    private Blaster blaster1;
    void Start()
    {
    //  _turretHandler = GameObject.Find("SerialManager").GetComponent<TurretHandler>();
     blaster1 = GameObject.Find("Canon").GetComponent<Blaster>();
    }
    void OnEnable()
    {
        _turretHandler.TurretFired.AddListener(OnTurretFired);
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
    // Update is called once per frame
    void Update()
    {
     _turret1.localRotation = Quaternion.Euler(0, _turretHandler.TurretAngle, 0);
    }

}

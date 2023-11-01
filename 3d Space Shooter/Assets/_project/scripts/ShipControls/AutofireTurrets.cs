using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutofireTurrets : MonoBehaviour
{
    [SerializeField] GameObject _missilePrefab;
    [SerializeField] AudioClip _fireSound;
    [SerializeField] float _fire_distance = 200f;
    
    [SerializeField] Transform _target;
    float _cooldown_time = 1.0f, _time_since_fire = 0;
    
    AudioSource _audioSource;

    void Start()
    {
        _audioSource = SoundManager.Configure3DAudioSource(GetComponent<AudioSource>());
        // _target = GameObject.Find("escort_ship").GetComponent<Rigidbody>().transform;
        _time_since_fire = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (_time_since_fire > _cooldown_time) {
            var diff_trans = transform.position - _target.position;
            if (diff_trans.magnitude < _fire_distance) {
                FireMissiles();
                _time_since_fire = 0;
            }
        } else {
            _time_since_fire += Time.deltaTime;
        }
    }
    void FireMissiles() {
            var missile = Instantiate(_missilePrefab, transform.position, transform.rotation).GetComponent<Missile>();
            missile.SetTarget(_target);
            missile.gameObject.SetActive(true);
    }
}


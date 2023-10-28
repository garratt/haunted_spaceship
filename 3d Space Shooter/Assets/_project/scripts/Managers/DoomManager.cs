using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoomManager : MonoBehaviour
{
         [SerializeField] GameObject _asteroidPrefab;
     [SerializeField] float _astroid_distance;
         [SerializeField] Transform _target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.H))
        {
            var asteroid = Instantiate(_asteroidPrefab,
               _target.position + _target.forward * _astroid_distance, Quaternion.identity).GetComponent<Asteroid>();
            asteroid.Init(_target);

        }
        
   }
}

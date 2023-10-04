using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FracturedAstroid : MonoBehaviour
{
    [SerializeField] [Range(1f, 60f)] private float _duration = 10f;
    // Start is called before the first frame update
    void OnEnable()
    {
        Destroy(gameObject, _duration);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

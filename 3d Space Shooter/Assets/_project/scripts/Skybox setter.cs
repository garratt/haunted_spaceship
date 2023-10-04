using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Skybox))]


public class Skyboxsetter : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] List<Material> _skyBoxMaterials;
    Skybox _skybox;

    // Update is called once per frame
    void Awake()
    {
        _skybox = GetComponent<Skybox>();
    }

    void OnEnable()
    {
        ChangeSkybox(0);
    }

    private void ChangeSkybox(int skyBox)
    {
        if (_skybox != null && skyBox >= 0 && skyBox <= _skyBoxMaterials.Count) {
            _skybox.material = _skyBoxMaterials[skyBox];
        }
    }
}


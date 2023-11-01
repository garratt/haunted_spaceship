using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hyperspace : MonoBehaviour
{
        GameObject _hyperspace;
        [SerializeField] NavControlHandler _navHandler;

    void OnEnable()
    {
        _navHandler.LightspeedEngage.AddListener(OnLightspeedEngage);
        _navHandler.LightspeedDisengage.AddListener(OnLightspeedDisengage);
    }

    void OnDisable()
    {
        _navHandler.LightspeedEngage.RemoveListener(OnLightspeedEngage);
        _navHandler.LightspeedDisengage.RemoveListener(OnLightspeedDisengage);
    }

    void OnLightspeedEngage()
    {
        Debug.Log("OnLightSpeedEngage");
        _hyperspace.SetActive(true);
    }
    void OnLightspeedDisengage()
    {
        Debug.Log("OnLightSpeedDisengage");
        _hyperspace.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        // var objects = GameObject.FindGameObjectsWithTag("Hyperspace");
        // if (objects.Length > 0) {
        //     _hyperspace = objects[0];
        // } else {
        //     Debug.Log("Could not find hyperspace object");
        // }
        _hyperspace = transform.GetChild(0).gameObject;
        
    }


    // Update is called once per frame
    void Update()
    {

     if (Input.GetKeyDown(KeyCode.H))        {
        _hyperspace.SetActive(!_hyperspace.activeSelf);
     }
    }
}

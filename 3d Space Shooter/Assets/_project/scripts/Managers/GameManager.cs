using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using Codice.Client.BaseCommands;
using Sirenix.Utilities;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
     [SerializeField] GameObject _asteroidPrefab;
     [SerializeField] float _astroid_distance;
    
    [SerializeField] Transform _target;
    bool ShouldQuitGame => Input.GetKeyUp(KeyCode.Escape);

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
    void Start()
    {
     Cursor.lockState = CursorLockMode.Confined;
     Cursor.visible = false;        
    }

    // Update is called once per frame
    void Update()
    {
    //  string[] ports = SerialPort.GetPortNames();
    //  foreach (string port in ports) {
    //     if (port.Contains("USB") || port.Contains("ACM")) {
    //         Debug.Log($"Found port {port}");
    //     }
    //  }
        if(ShouldQuitGame) {
            ShouldGame();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            InRedAlert(true);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            var asteroid = Instantiate(_asteroidPrefab,
               _target.position + _target.forward * _astroid_distance, Quaternion.identity).GetComponent<Asteroid>();
            asteroid.Init(_target);

        }
        
    }

        public void InRedAlert(bool inRedAlert)
    {
        if (inRedAlert)
        {
            MusicManager.Instance.PlayRedAlert();
            return;
        }

        MusicManager.Instance.PlayPatrolMusic();
    }

    // void OnMessageArrived(string msg) {
    //     Debug.Log("Arrived: " + msg);
    //     serialController.SendSerialMessage("1");
    // }

    // void OnConnectionEvent(bool success) {
    //     Debug.Log(success ? "Device Connected" : "Device Disconnected");
    // }

    private void ShouldGame()
    {
#if UNITY_EDITOR
    UnityEditor.EditorApplication.isPlaying=false;
#else
    //Todo: Handle Webgl
    Application.Quit();
#endif
    }
}

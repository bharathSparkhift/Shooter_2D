using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{
    private DateTime _initialTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        _initialTime = DateTime.Now;
        LogHandler.OnLogHandler?.Invoke($"scene {SceneManager.GetActiveScene().name.ToString()} \t Opened at {DateTime.Now}");
    }

    /// <summary>
    /// Load game scene on button click
    /// </summary>
    public void EnterGameScene()
    {
        var timeSpent = DateTime.Now - _initialTime;
        Debug.Log($"Time spent {timeSpent}");
        LogHandler.OnLogHandler?.Invoke($"Time spent in {SceneManager.GetActiveScene().name.ToString()} scene {timeSpent.ToString()}");
        AsyncOperation asyncOperation  = SceneManager.LoadSceneAsync(2);
        
    }
}

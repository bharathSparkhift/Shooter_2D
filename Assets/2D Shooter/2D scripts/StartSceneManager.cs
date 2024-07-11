using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{

    public delegate void StartSceneManagerDelegate();
    public static StartSceneManagerDelegate OnStartSceneManager;

    [SerializeField] string openURL = "https://google.com";
    [SerializeField] Animation redBoxAnimation;

    private DateTime _initialTime;


    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        _initialTime = DateTime.Now;
        LogHandler.OnLogHandler?.Invoke($"scene {SceneManager.GetActiveScene().name.ToString()} \t Opened at {DateTime.Now}");
        redBoxAnimation.Play();
        OnStartSceneManager += SwitchGameScene;
    }

    private void OnDisable()
    {
        OnStartSceneManager -= SwitchGameScene;
    }

    /// <summary>
    /// Load game scene on button click
    /// </summary>
    public void EnterGameScene()
    {
        var timeSpent = DateTime.Now - _initialTime;
        Debug.Log($"Time spent {timeSpent}");
        LogHandler.OnLogHandler?.Invoke($"Time spent in {SceneManager.GetActiveScene().name.ToString()} scene {timeSpent.ToString()}");
        // SceneManager.LoadSceneAsync(2);
    }

    /// <summary>
    /// Open browser on button click
    /// </summary>
    public void OpenUrlOnButtonClick()
    {
        Application.OpenURL(openURL);   
    }

    async void SwitchGameScene()
    {
        await Task.Delay(2000);
        SceneManager.LoadSceneAsync(2);
        Debug.Log($"<color=green>switching to the game scene...</color>");
    }

}

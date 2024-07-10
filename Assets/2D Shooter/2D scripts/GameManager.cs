using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public enum DifficultLevel
    {
        Easy,
        Medium,
        Hard
    }

    /// <summary>
    /// Defines the difficult level of the game.
    /// </summary>
    [field:SerializeField] public DifficultLevel Difficult_Level { get; private set; }

    public DifficultLevel GetDifficultLevel => Difficult_Level;

    DateTime _initialTime;

    #region Monobehaviour callbacks
    void Start()
    {
        
    }

    private void OnEnable()
    {
        _initialTime = DateTime.Now;
        LogHandler.OnLogHandler?.Invoke($"scene {SceneManager.GetActiveScene().name.ToString()} \t Opened at {DateTime.Now}");
    }


    #endregion

    /// <summary>
    /// Load start scene on button click
    /// </summary>
    public void LoadStartScene()
    {
        var timeSpent = DateTime.Now - _initialTime;
        Debug.Log($"Time spent {timeSpent}");
        LogHandler.OnLogHandler?.Invoke($"Time spent in {SceneManager.GetActiveScene().name.ToString()} scene {timeSpent.ToString()}");
        SceneManager.LoadSceneAsync(1);
    }

    public void PauseGame()
    {
        if(Time.timeScale != 0)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
        Debug.Log($"{nameof(PauseGame)}");
        
    }

    


}

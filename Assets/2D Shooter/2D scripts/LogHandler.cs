using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogHandler : MonoBehaviour
{

    public delegate void LogHandlerDelegate(string message);
    public static LogHandlerDelegate OnLogHandler;

    public string LogFilePath => Application.persistentDataPath + "/dev_log_file.txt";


    private void Awake()
    {
        if (!File.Exists(LogFilePath))
        {
            File.Create(LogFilePath);
            Debug.Log("<color=green>Log file created</color>");
            Debug.Log($"{LogFilePath}");
        }
    }

    

    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    private void OnEnable()
    {
        OnLogHandler += UpdateLog;
        
        
    }

    private void OnDisable()
    {
        OnLogHandler -= UpdateLog;
    }

    void UpdateLog(string message)
    {
        if (!File.Exists(LogFilePath))
            return;

        try
        {
            // File.AppendAllText(LogFilePath, "\n");
            File.AppendAllText(LogFilePath, "\n"+message);
        }
        catch (Exception ex)
        {
            Debug.Log($"{ex.Message}");
        }
        
    }
}

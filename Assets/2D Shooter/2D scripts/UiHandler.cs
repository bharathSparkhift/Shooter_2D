using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UiHandler : MonoBehaviour
{


    public delegate void UiHandlerDelegate(int index);
    public static UiHandlerDelegate OnUiHandler;

    public enum CanvasType
    {
        Notification,
        Player,
        Stats

    }

    [HideInInspector]
    public CanvasType Canvas_Type;

    [SerializeField] Button loginButton;
    [SerializeField] Transform currentCanvas;
    [SerializeField] Transform[] canvas;
   

    PlayerData _playerData;

    // Start is called before the first frame update
    void Start()
    {
        currentCanvas = canvas[0];
    }

    private void OnEnable()
    {
        OnUiHandler += ToggleCanvas;
    }

    

    private void OnDisable()
    {
        OnUiHandler -= ToggleCanvas;
    }


    /// <summary>
    /// index represent, which canvas to enable
    /// </summary>
    /// <param name="index"></param>
    void ToggleCanvas(int index)
    {
        currentCanvas.gameObject.SetActive(false);
        switch (index)
        {
            case 0: // Login canvas
                currentCanvas = canvas[0];
                currentCanvas.gameObject.SetActive(true);
                break;
            case 1: // Player canvas
                currentCanvas = canvas[1];
                currentCanvas.gameObject.SetActive(true);
                break; 
            case 2: // Stats canvas
                currentCanvas = canvas[2];
                currentCanvas.gameObject.SetActive(true);
                break;
        }
    }

    /// <summary>
    /// Invoke this method on button click
    /// </summary>
    public void LoginOnButtonClick()
    {
        
    }

    public void LogoutOnButtonClick()
    {
        
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


/// <summary>
/// Not in use of now.
/// </summary>
public class GameUiHandler : MonoBehaviour
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
   

    PlayerData _playerData;

    // Start is called before the first frame update
    void Start()
    {

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
        
    }

}

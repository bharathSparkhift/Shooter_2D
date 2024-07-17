using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneUiHandler : MonoBehaviour
{

    public delegate void StartSceneUiHandlerDelegate(string value);
    public static StartSceneUiHandlerDelegate OnStartSceneUiHandler;

    [SerializeField] private Transform canvas;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        OnStartSceneUiHandler += ToggleStartSceneCanvas;
    }

    private void OnDisable()
    {
        OnStartSceneUiHandler -= ToggleStartSceneCanvas;
    }

    void ToggleStartSceneCanvas(string value)
    {
        bool result;
        bool.TryParse(value, out result);
        canvas.gameObject.SetActive(result);
    }

    
}

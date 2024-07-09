using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class start : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    /// <summary>
    /// Load game scene on button click
    /// </summary>
    public void EnterGameScene()
    {
        SceneManager.LoadSceneAsync(1);
    }
}

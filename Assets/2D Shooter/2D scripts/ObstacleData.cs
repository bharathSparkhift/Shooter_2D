using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleData : MonoBehaviour
{

    public delegate void ObstacleDataDelegate(); 
    public static ObstacleDataDelegate OnObstacleData;


    [SerializeField] private RectTransform      gameCanvas;
    [SerializeField] private float              delay;
    [SerializeField] private bool[]             obstaclePositionAvailable = new bool[] { true, true, true, true };
    [SerializeField] private RectTransform[]    obstacles;



    #region Monobehaviour callbacks
    private void OnEnable()
    {
        OnObstacleData += EnableObstacle;
    }

    private void OnDisable()
    {
        OnObstacleData -= EnableObstacle;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Enable the obstacle one after the other after every two seconds
        InvokeRepeating(nameof(EnableObstacle), 0, 2);
    }

 
    #endregion


    

    /// <summary>
    /// Loop through the obstacles and enable 
    /// </summary>
    void EnableObstacle()
    {
        foreach(RectTransform obstacle in obstacles)
        {
            if(!obstacle.gameObject.activeInHierarchy)
            {
                obstacle.gameObject.SetActive(true);
                break;
                
            }
        }
    }

}

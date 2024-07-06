using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleData : MonoBehaviour
{

    public delegate void ObstacleDataDelegate(Transform transform);
    public ObstacleDataDelegate OnObstacleData;


    [SerializeField] private Transform      obstacleParentTransform;
    [SerializeField] private Transform[]    obstacles;

  

    #region Monobehaviour callbacks
    private void OnEnable()
    {
        OnObstacleData += InstantiateObject;
    }

    private void OnDisable()
    {
        OnObstacleData -= InstantiateObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    #endregion

    void InstantiateObject(Transform transform)
    {
        transform.gameObject.SetActive(true);
        
    }

}

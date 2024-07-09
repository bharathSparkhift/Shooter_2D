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
        InvokeRepeating(nameof(EnableObstacle), 0, 2);
    }

    private void Update()
    {
  

    }
    #endregion


    bool AreRectTransformsOverlapping(RectTransform obstaclePosition, RectTransform obstacle)
    {
        // Get the corners of the rectangles in world space
        Vector3[] corners1 = new Vector3[4];
        Vector3[] corners2 = new Vector3[4];

        obstaclePosition.GetWorldCorners(corners1);
        obstacle.GetWorldCorners(corners2);

        // Check for overlap
        Rect rect1 = new Rect(corners1[0], corners1[2] - corners1[0]);
        Rect rect2 = new Rect(corners2[0], corners2[2] - corners2[0]);

        return rect1.Overlaps(rect2);
    }

    void InstantiateObject(Transform transform = null)
    {
        transform.gameObject.SetActive(true);
        
    }

    /// <summary>
    /// 
    /// </summary>
    void EnableObstacle()
    {
        foreach(RectTransform obstacle in obstacles)
        {
            if(!obstacle.gameObject.activeInHierarchy)
            {
                obstacle.gameObject.SetActive(true);
                // obstacle.anchoredPosition = new Vector2(Random.Range(-(gameCanvas.sizeDelta.x / 2 + obstacle.sizeDelta.x / 2),gameCanvas.sizeDelta.x/2 + obstacle.sizeDelta.x/2),0);
                break;
                
            }
        }
    }

}

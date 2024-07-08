using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopPanel : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(ToggleObstacles), 0, 3);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        
        
    }

    void ToggleObstacles()
    {
        ObstacleData.OnObstacleData?.Invoke();
        Debug.Log($"{nameof(TopPanel)} \t {nameof(ToggleObstacles)}");
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomPanel : MonoBehaviour
{

    [SerializeField] RectTransform gameCanvas;
    [SerializeField] BoxCollider2D boxCollider2D;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider2D.size = new Vector2(gameCanvas.rect.size.x, 100);
    }

    private void OnCollisionExit(Collision collision)
    {
        // ObstacleData.OnObstacleData?.Invoke();
    }



}

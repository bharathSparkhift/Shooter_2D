using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Obstacle : MonoBehaviour
{
    public enum Type
    {
        square,
        circle,
        triangle,
        diamond,

    }


    [field : SerializeField] public Type ObstacleType { get; private set; }
    /// <summary>
    /// Game canvas rect transform.
    /// </summary>
    [SerializeField] RectTransform gameCanvasRectTransform;
    /// <summary>
    /// Current object Rect transform
    /// </summary>
    [SerializeField] RectTransform rectTransform;
    [SerializeField] int hitCount = 0;
    [SerializeField] int maxHitCount = 5;
    [SerializeField] float fallingSpeed = 2f;
    //[SerializeField] float initialXposition;
    [SerializeField] Image image;
    [SerializeField] bool enableGravity;

    GameManager _gameManager;

    #region Monobehaviour callbacks

    private void Awake()
    {
        
    }

    void Start()
    {
        _gameManager = new GameManager();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Bottom Panel")
            return;

        hitCount++;
        switch (hitCount)
        {
            case 1:
                image.color = new Color32(255, 204, 204, 255);
                break;
            case 2:
                image.color = new Color32(255, 154, 154, 255);
                break;
            case 3:
                image.color = new Color32(255, 103, 103, 255);
                break;
            case 4:
                image.color = new Color32(255, 52, 52, 255);
                break;
            case 5:
                image.color = new Color32(255, 0, 0, 255);
                break;
            case 6:
                // Vibrate
                Handheld.Vibrate();

                // Update Playerdata
                PlayerData.OnPlayerData?.Invoke(null, ObstacleType.ToString());

                ResetObstacle();
                break;
        }
        
    }

    private void Update()
    {
        if(enableGravity)
        {
            rectTransform.anchoredPosition += new Vector2(0, -Time.deltaTime * fallingSpeed); 
            if(rectTransform.anchoredPosition.y < -gameCanvasRectTransform.sizeDelta.y-100 )
            {
                ResetObstacle();
           
            }
            
        }
    }
    #endregion


    void ResetObstacle()
    {
        
        
        // Reset the Y anchoredPosition
        rectTransform.anchoredPosition = new Vector2(UnityEngine.Random.Range((-gameCanvasRectTransform.sizeDelta.x / 2 + rectTransform.sizeDelta.x / 3), gameCanvasRectTransform.sizeDelta.x / 2 - rectTransform.sizeDelta.x), 0);

        // Reset the color to white
        image.color = Color.white;

        // Reset the hit count to zero
        hitCount = 0;
    }

 

}

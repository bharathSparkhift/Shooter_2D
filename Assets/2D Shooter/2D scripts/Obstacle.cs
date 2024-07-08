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
    [SerializeField] float initialXposition;
    [SerializeField] Image image;
    [SerializeField] bool enableGravity;

    GameManager _gameManager;

    #region Monobehaviour callbacks

    private void Awake()
    {
        _gameManager = new GameManager();
    }

    void Start()
    {
        initialXposition = rectTransform.anchoredPosition.x;
    }

    private void OnEnable()
    {
        // ResetPosition();
        // initialXposition = rectTransform.anchoredPosition.x;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        hitCount++;
        switch (hitCount)
        {
            case 1:
                // Code for hitCount == 1
                image.color = new Color32(255, 204, 204, 255);
                break;
            case 2:
                // Code for hitCount == 2
                image.color = new Color32(255, 154, 154, 255);
                break;
            case 3:
                // Code for hitCount == 3
                image.color = new Color32(255, 103, 103, 255);
                break;
            case 4:
                // Code for hitCount == 4
                image.color = new Color32(255, 52, 52, 255);
                break;
            case 5:
                // Code for hitCount == 5
                image.color = new Color32(255, 0, 0, 255);
                break;
            case 6:
                hitCount = 0;
                DisableObject();
                image.color = Color.white;
                break;
        }
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        
    }

    private void Update()
    {
        if(enableGravity)
        {
            rectTransform.anchoredPosition += new Vector2(0, -Time.deltaTime * fallingSpeed); 
        }
    }
    #endregion


    void DisableObject()
    {
        PlayerData.OnPlayerData?.Invoke(ObstacleType.ToString());
        // this.gameObject.SetActive(false);
        rectTransform.anchoredPosition = new Vector2(initialXposition, 0);
        
    }

    void ResetPosition()
    {
        // rectTransform.anchoredPosition = new Vector2(Random.Range(-490f, 490f), 0);
    }

}

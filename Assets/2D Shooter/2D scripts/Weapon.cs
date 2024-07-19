using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private RectTransform playerRectTransform;
    // [SerializeField] private RectTransform playerNozzleRectTransform;
    [SerializeField] private RectTransform rectTransform;
    
    [SerializeField] private float offsetYpos = 50f;

    

    void Update()
    {
        rectTransform.anchoredPosition =  playerRectTransform.anchoredPosition + new Vector2(0, offsetYpos);
    }

}

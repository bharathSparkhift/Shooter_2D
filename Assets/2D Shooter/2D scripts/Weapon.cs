using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private RectTransform playerRectTransform;
    [SerializeField] private RectTransform playerNozzleRectTransform;
    [SerializeField] private RectTransform rectTransform;
    
    [SerializeField] private float offsetYpos = 50f;

    

    // Update is called once per frame
    void Update()
    {
        rectTransform.anchoredPosition =  playerRectTransform.anchoredPosition + new Vector2(0, offsetYpos);
        // rectTransform.anchoredPosition =  transform.TransformDirection(playerNozzleRectTransform.anchoredPosition3D) + new Vector3(0, offsetYpos);
            
    }

}

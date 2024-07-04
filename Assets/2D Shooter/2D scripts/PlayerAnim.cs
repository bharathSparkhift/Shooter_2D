using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    [SerializeField] RectTransform playerRectTransform;
    [SerializeField] Canvas canvas;
    [SerializeField] RectTransform canvasRectTransform;
    [SerializeField] float delayToChangePosition = 1f;
    [SerializeField] float minXclamp;
    [SerializeField] float maxXclamp;
    [SerializeField] float minYclamp;
    [SerializeField] float maxYclamp;
    


    // Start is called before the first frame update
    void Start()
    {
        minXclamp = -(canvasRectTransform.rect.width - playerRectTransform.sizeDelta.x) / 2;
        maxXclamp = (canvasRectTransform.rect.width - playerRectTransform.sizeDelta.x) / 2;

        minYclamp = (0 + playerRectTransform.sizeDelta.y / 2);
        maxYclamp = (canvasRectTransform.rect.height * 0.25f - playerRectTransform.sizeDelta.y / 2);

        InvokeRepeating(nameof(ChangePosition), 0, delayToChangePosition);
    }

    

    void ChangePosition()
    {
        var xRandom = Random.Range(minXclamp, maxXclamp);
        var yRandom = Random.Range(minYclamp, maxYclamp);
        playerRectTransform.anchoredPosition = new Vector2(xRandom, yRandom);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Bullet : MonoBehaviour
{
    #region Monobehaviour callbacks
    [SerializeField] RectTransform gameCanvas;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] RectTransform rectTransform;
    [SerializeField] float forceSpeed = 2f;
    [SerializeField] float disableDelay = 4f;
    #endregion


    void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        DisableGameObject();
    }

   

  
    private void OnEnable()
    {
        Vector2 upwardWorldDirection = transform.TransformDirection(transform.up);
        rb.AddForce(upwardWorldDirection * forceSpeed, ForceMode2D.Impulse);
        rectTransform.localRotation = Quaternion.Euler(0, 0, 0);
    }

    void DisableGameObject()
    {
        rectTransform.anchoredPosition = Vector3.zero;
        gameObject.SetActive(false);
    }


    private void Update()
    {
        if(rectTransform.anchoredPosition.x < -gameCanvas.sizeDelta.x/2 || rectTransform.anchoredPosition.x > gameCanvas.sizeDelta.x / 2)
        {
            DisableGameObject();
        }
    }
}

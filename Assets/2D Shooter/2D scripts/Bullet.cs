using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Bullet : MonoBehaviour
{

    [SerializeField] Rigidbody2D rb;
    [SerializeField] RectTransform rectTransform;
    [SerializeField] float forceSpeed = 2f;
    [SerializeField] float disableDelay = 4f;
    



    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        DisableGameObject();
        if (collision.collider.tag == "Obstacle")
        {
            
            // Debug.Log($"{nameof(OnCollisionEnter2D)} \t {collision.gameObject.name}");
        }
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Game Canvas")
        {
            // DisableGameObject();
            // Debug.Log($"{nameof(OnCollisionEnter2D)} \t {collision.gameObject.name}");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

    }

    private void OnEnable()
    {
        
        Vector2 upwardWorldDirection = transform.TransformDirection(transform.up);
        rb.AddForce(upwardWorldDirection * forceSpeed, ForceMode2D.Impulse);
        rectTransform.localRotation = Quaternion.Euler(0, 0, 0);
        // Invoke(nameof(DisableGameObject), disableDelay);
    }

    void DisableGameObject()
    {
        rectTransform.anchoredPosition = Vector3.zero;
        gameObject.SetActive(false);
    }
}

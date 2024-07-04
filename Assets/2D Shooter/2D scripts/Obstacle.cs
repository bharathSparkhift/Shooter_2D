using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"{nameof(OnCollisionEnter)} \t {collision.gameObject.name}");
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}

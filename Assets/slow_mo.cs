using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;


public class slow_mo : MonoBehaviour
{
    public Rigidbody2D body;
    private movement movement;

    public TextMeshPro inputText;
    public static float slow;
    private Vector3 v;


    void Start()
    {
        slow = 1;
        movement = GameObject.Find("Player").GetComponent<movement>();

      
      
        v = new Vector3(-9, 0, 0);
        body.velocity = v;
        

    }

    void OnTriggerEnter2D(Collider2D infoCollision) // le type de la variable est Collision
    {
        if (infoCollision.CompareTag("SlowMotion"))
        {
            movement.ObstacleAvoiding = true;
            slow = 0.1f;
        }
    }

    void OnTriggerExit2D(Collider2D infoCollision)
    {
        if (infoCollision.CompareTag("SlowMotion"))
        {
            movement.ObstacleAvoiding = false;
            slow = 1;
        }
    }

    
    void Update()
    {
        if (!movement.termine)
        {
            body.velocity = v * slow;
        }

        else body.velocity = v * 0;
        
    }
}

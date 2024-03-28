using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class slow_mo : MonoBehaviour
{
    //public GameObject zone_slow;
    public Rigidbody2D body;
    public movement movement;
    
    void Start()
    {
        movement = GameObject.Find("Player").GetComponent<movement>();
        body.velocity = new Vector3(-2, 0, 0);
    }
    void OnTriggerEnter2D(Collider2D infoCollision) // le type de la variable est Collision
    {
        if (infoCollision.CompareTag("SlowMotion"))
        {
            movement.ObstacleAvoiding = true;
            body.velocity = new Vector2(-0.5f,0) ;
        }
    }

    void OnTriggerExit2D(Collider2D infoCollision)
    {
        if (infoCollision.CompareTag("SlowMotion"))
        {
            movement.ObstacleAvoiding = false;
            body.velocity = new Vector2(-2f, 0);
        }
    }


    // Update is called once per frame
    void Update()
    {

    }
}

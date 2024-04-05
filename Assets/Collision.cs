using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Collision : MonoBehaviour
{
    public GameObject obstacle;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (obstacle.transform.position.x < -15) Destroy(obstacle);
    }


    void OnTriggerEnter2D(Collider2D infoCollision)
    {
        //if (infoCollision.GetType().ToString().Equals("UnityEngine.CapsuleCollider2D") && infoCollision.CompareTag("Player"))
        if (infoCollision.CompareTag("Player"))
        {
            Debug.Log("Hit");
            Vector3 push = new Vector3(-2f, 0f, 0f);
            movement.stun = true;
            Destroy(obstacle);
            infoCollision.gameObject.transform.position = infoCollision.gameObject.transform.position + push;
            
            //infoCollision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-5, 5), ForceMode2D.Impulse);
            // ou bien appeler fonction du player
            
        }
    }
}

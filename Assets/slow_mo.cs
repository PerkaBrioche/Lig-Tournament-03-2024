using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slow_mo : MonoBehaviour
{
    //public GameObject zone_slow;
    public Rigidbody2D body;
   // public ParticleSystem particle;
    // Start is called before the first frame updatepublic GameObject objetExplosion ;
    void OnTriggerEnter2D(Collider2D infoCollision) // le type de la variable est Collision
    {
        if (infoCollision.gameObject.name == "Slowmo")
        {
            body.velocity = new Vector2(-0.5f,0) ;
        }
        //if (infoCollision.gameObject.name == "Player")
        //{    Destroy(this); particle.Play();}
    }

    void OnTriggerExit2D(Collider2D infoCollision)
    {
        if (infoCollision.gameObject.name == "Slowmo")
        {
            body.velocity = new Vector2(-2f, 0);
        }
    }
    void Start()
    {
        body.velocity = new Vector3(-2, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnTriggerEnter2D(Collider2D infoCollision)
    {
        //if (infoCollision.GetType().ToString().Equals("UnityEngine.CapsuleCollider2D") && infoCollision.CompareTag("Player"))
        if (infoCollision.CompareTag("Player"))
        {
            Debug.Log("Hit");
            Vector3 push = new Vector3(-2f, 0f, 0f);
            infoCollision.gameObject.transform.position = infoCollision.gameObject.transform.position + push;
            //infoCollision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-5, 5), ForceMode2D.Impulse);
            // ou bien appeler fonction du player
            gameObject.SetActive(false);
        }
    }
}

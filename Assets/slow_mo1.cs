using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slow_mo_1 : MonoBehaviour
{
    public GameObject obstacle_1;
    public GameObject obstacle_2;


    // Start is called before the first frame update
    void Start()
    {
        obstacle_1.GetComponent<Rigidbody2D>().velocity = new Vector2(-2, 0);
        obstacle_2.GetComponent<Rigidbody2D>().velocity = new Vector2(-2,0);


    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody2D b_obstacle_1 = obstacle_1.GetComponent<Rigidbody2D>();
        Rigidbody2D b_obstacle_2 = obstacle_2.GetComponent<Rigidbody2D>();
        if (obstacle_1.transform.position.x - this.transform.position.x < 0.5)
        {
            obstacle_1.GetComponent<Rigidbody2D>().velocity = new Vector3(-0.5f, 0, 0);
        }

        if (obstacle_2.transform.position.x - this.transform.position.x < 0.5)
        {
            obstacle_2.GetComponent<Rigidbody2D>().velocity = new Vector3(-0.5f, 0, 0);
        }

        if (obstacle_1.transform.position.x - this.transform.position.x < -3.5)
        {
            obstacle_1.GetComponent<Rigidbody2D>().velocity = new Vector3(-2, 0, 0);
        }

        if (obstacle_2.transform.position.x - this.transform.position.x < -3.5)
        {
            obstacle_2.GetComponent<Rigidbody2D>().velocity = new Vector3(-2, 0, 0);
        }



    }
}

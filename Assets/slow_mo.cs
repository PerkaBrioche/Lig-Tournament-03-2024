using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slow_mo : MonoBehaviour
{
    public GameObject zone_slow;
    public Rigidbody2D body;
    bool passed = false;
    // Start is called before the first frame update
    void Start()
    {
        body.velocity = new Vector3(-2, 0, 0);
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = zone_slow.transform.position.x;
        if (this.transform.position.x - x < 0.2)
        {body.velocity = new Vector3(-0.5f,0,0); passed = true; }

        if (passed == true)
            if (this.transform.position.x - x < -2f)
            { body.velocity = new Vector3(-2, 0, 0); passed = false; }



    }
}

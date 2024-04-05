using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class seuil_pos : MonoBehaviour
{
    public static float pos_x = 10000000;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Low") || other.CompareTag("High"))
        {
                pos_x = other.gameObject.GetComponent<Transform>().position.x; // E : je récup la position de l'obstacle pour pouvoir faire les tests
 
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Low") || other.CompareTag("High"))
        {
            pos_x = 10000000000;
        }
    }
}

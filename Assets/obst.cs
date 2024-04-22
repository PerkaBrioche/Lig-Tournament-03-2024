using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class obst : MonoBehaviour
{
    // Start is called before the first frame update
    public static string tag_o;
    void OnTriggerEnter2D(Collider2D infoCollision) // le type de la variable est Collision
    {

        if (infoCollision.CompareTag("High"))
        {
            tag_o = "high";
        }
        if (infoCollision.CompareTag("Low"))
        {
            tag_o = "low";
        }
    }
}

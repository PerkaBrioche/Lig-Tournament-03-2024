using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class obst : MonoBehaviour
{
    // Start is called before the first frame update
    public static string tag_o;
    public AudioSource audio_level;
    
    void OnTriggerEnter2D(Collider2D infoCollision) // le type de la variable est Collision
    {

        if (infoCollision.CompareTag("High"))
        {
            tag_o = "high";
            audio_level.pitch = 0.75f;
        }
        if (infoCollision.CompareTag("Low"))
        {
            tag_o = "low";
            audio_level.pitch = 0.75f;
        }
    }

    void OnTriggerExit2D(Collider2D infoCollision) // le type de la variable est Collision
    {

        if (infoCollision.CompareTag("High") || infoCollision.CompareTag("Low"))
        {
            audio_level.pitch = 1;
        }
    }
}

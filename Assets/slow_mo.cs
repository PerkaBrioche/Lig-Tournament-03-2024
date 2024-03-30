using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using TMPro;


public class slow_mo : MonoBehaviour
{
    //public GameObject zone_slow;
    public Rigidbody2D body;
    public movement movement;

    public TextMeshPro inputText;
    public List<KeyCode> possibleInputs;

    private bool displayInput = false;
    public KeyCode curInput;

    void Start()
    {
        movement = GameObject.Find("Player").GetComponent<movement>();
        body.velocity = new Vector3(-2, 0, 0);

        inputText = GetComponentInChildren<TextMeshPro>();
        if (inputText != null)
        {
            inputText.text = "";
        }
        else
        {
            Debug.LogWarning("TextMeshPro component not found in the child objects of " + gameObject.name);
        }
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
        if(movement.ObstacleAvoiding && !displayInput)
        {
            Debug.Log("displaying input");
            var i = Random.Range(0, possibleInputs.Count);
            curInput = possibleInputs[i];
            inputText.text = curInput.ToString();
            displayInput = true;
        } else if(displayInput && !(movement.ObstacleAvoiding))
        {
            Debug.Log("no longer displaying input");
            inputText.text = "";
            displayInput = false;
        }
    }
}

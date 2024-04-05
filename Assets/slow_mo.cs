using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using TMPro;


public class slow_mo : MonoBehaviour
{
    //public GameObject zone_slow;
    public Rigidbody2D body;
    public movement movement;

    public TextMeshPro inputText;
    public List<KeyCode> possibleInputs;
    public static float slow;
    private Vector3 v;

    private bool displayInput = false;
    public KeyCode curInput;

    void Start()
    {
        slow = 1;
        movement = GameObject.Find("Player").GetComponent<movement>();

       // inputText = GetComponentInChildren<TextMeshPro>();
      //  if (inputText != null)
       // {
       //     inputText.text = "";
      //  }
      //  else
       // {
      //      Debug.LogWarning("TextMeshPro component not found in the child objects of " + gameObject.name);
       // }
      
        v = new Vector3(-6, 0, 0);
        body.velocity = v;
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
            slow = 0.1f;
        }
    }

    void OnTriggerExit2D(Collider2D infoCollision)
    {
        if (infoCollision.CompareTag("SlowMotion"))
        {
            movement.ObstacleAvoiding = false;
            slow = 1;
        }
    }

    
    // Update is called once per frame
    void Update()
    {
        if (movement.ObstacleAvoiding && !displayInput)
        {
            Debug.Log("displaying input");
            var i = Random.Range(0, possibleInputs.Count);
            curInput = possibleInputs[i];
            inputText.text = curInput.ToString();
            displayInput = true;
        }
        else if (displayInput && !(movement.ObstacleAvoiding))
        {
            Debug.Log("no longer displaying input");
            inputText.text = "";
            displayInput = false;
        }
        body.velocity = v * slow;
        if (movement.ObstacleAvoiding && !displayInput)
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

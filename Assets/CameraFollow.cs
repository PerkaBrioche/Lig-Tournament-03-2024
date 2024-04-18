using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Vector3 offset = new Vector3(80f, 80f, -10f);
    [SerializeField] private float smoothTime = 0.25f;
    [SerializeField] private Vector3 velocity = Vector3.zero;
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 targetPosition = new Vector3(target.position.x + offset.x, transform.position.y,target.position.z + offset.z);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}

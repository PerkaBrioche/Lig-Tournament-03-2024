using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesSpawner : MonoBehaviour
{
    private Vector3 lastPos;
    private float dist = 0f;
    public Transform player;
    private Vector3 offset;

    public GameObject obstHaut;
    public GameObject obstBas;
    public float spawnDist;
    private GameObject obs_spawn;
    private float y_obs;

    // Start is called before the first frame update
    void Start()
    {
        lastPos = transform.position;
        offset = transform.position - player.position;

        spawnDist = Random.Range(5f, 15f);
        StartCoroutine(ObstacleSpawn());
    }

    // Update is called once per frame
    void Update()
    {
        dist += Vector3.Distance(transform.position, lastPos);
        lastPos = transform.position;

        Vector3 newPos = player.position;
        newPos += offset;
        newPos.y = transform.position.y;
        newPos.z = transform.position.z;
        transform.position = newPos;
    }

    IEnumerator ObstacleSpawn()
    {
        while (true)
        {
            var type_obs = Random.Range(0, 2);
            if (type_obs == 0)
            {
                y_obs = transform.position.y;
                obs_spawn = obstHaut;
            }
            else
            {
                y_obs = transform.position.y - 3.5f;
                obs_spawn = obstBas;
            }
            if (obs_spawn != null)
            {
                var pos_obs = new Vector3(transform.position.x, y_obs);
                GameObject obstacle = Instantiate(obs_spawn, pos_obs, Quaternion.identity);
            }
            else
            {
            }
            while (dist < spawnDist)
            {
                yield return new WaitForSeconds(1);
            }
            dist = 0;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatSpawner : MonoBehaviour
{
    public float spawnRate = 1.0f;
    public GameObject beatBar;
    private float nextTimeToSpawn = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Time.time >= nextTimeToSpawn)
        {
            Instantiate(beatBar, new Vector2(transform.position.x+5,transform.position.y), Quaternion.identity);
            Instantiate(beatBar, new Vector2(transform.position.x - 5, transform.position.y), Quaternion.identity);

            nextTimeToSpawn = Time.time + 1f / spawnRate;
        }
    }
}

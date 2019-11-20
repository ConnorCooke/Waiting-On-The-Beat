using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatSpawner : MonoBehaviour
{
    [SerializeField]
    public GameObject beatBar;
    // Start is called before the first frame update
    void Awake()
    {
    }

    /*
    // Update is called once per frame
    void FixedUpdate()
    {
        if (Time.time >= nextTimeToSpawn)
        {
            Instantiate(beatBar, new Vector2(transform.position.x+10,transform.position.y), Quaternion.identity);
            Instantiate(beatBar, new Vector2(transform.position.x - 10, transform.position.y), Quaternion.identity);

            nextTimeToSpawn = Time.time + 1f / spawnRate;
        }
    }
    */

    public void SpawnBeat()
    {
        Instantiate(beatBar, new Vector2(transform.position.x + 10, transform.position.y), Quaternion.identity);
        Instantiate(beatBar, new Vector2(transform.position.x - 10, transform.position.y), Quaternion.identity);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserManager : MonoBehaviour
{
    public int spawnRate;
    private int spawnTimer = 0;
    public int startTimingAtThisBeat;
    private bool startSpawning;
    public GameObject verticalLaserPrefab;
    public GameObject horizontalLaserPrefab;
    public ObjectManager objectManager;
    private int[] playerPosition = {10, 5};
    public float removalAmount;
    private float currTipTotal = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    
    public void UpdatePosition(int[] playerPos)
    {
        playerPosition = new int[] { playerPos[0], playerPos[1] };
    }

    private void SpawnLaser(string orientation)
    {
        if (currTipTotal > 0f)
        {
            float x = 0;
            float y = 0;
            int difference = 1;
            GameObject prefab = verticalLaserPrefab;
            if (UnityEngine.Random.Range(0.0f, 1.0f) <= .5)
            {
                difference = -1;
            }
            if (orientation == "vertical")
            {
                x = (float)(playerPosition[0] - 10 + difference);
                StartCoroutine(RemoveCash(playerPosition[0] + difference, orientation));
            }
            else
            {
                y = -((float)(playerPosition[1] - 5 + difference));
                prefab = horizontalLaserPrefab;
                StartCoroutine(RemoveCash(playerPosition[1] + difference, orientation));
            }
            Instantiate(prefab, new Vector3(x, y, 0), Quaternion.identity);
        }
    }

    public IEnumerator RemoveCash(int position, string orientation)
    {
        yield return new WaitForSeconds((float)1.5);
        objectManager.RemoveCash(position, orientation, removalAmount);

    }

    public void BeatOccured()
    {
        if (startSpawning)
        {
            if(spawnTimer == spawnRate)
            {
                if(UnityEngine.Random.Range(0.0f, 1.0f) <= .5)
                {
                    SpawnLaser("vertical");
                }
                else
                {
                    SpawnLaser("horizontal");
                }
                spawnTimer = 0;
            }
            else
            {
                spawnTimer++;
            }
        }
        else
        {
            if(spawnTimer == startTimingAtThisBeat)
            {
                startSpawning = true;
                spawnTimer = 0;
            }
            else
            {
                spawnTimer++;
            }
            
        }
    }

    public void AddTip(float tip)
    {
        currTipTotal += tip;
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSpawnerTutorial : LaserManager
{
    protected override void SpawnLaser(string orientation)
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
                x = (float)(playerPosition[0] - 8 + difference);
                StartCoroutine(RemoveCash(playerPosition[0] + difference, orientation));
            }
            else
            {
                y = -((float)(playerPosition[1] - 3 + difference));
                prefab = horizontalLaserPrefab;
                StartCoroutine(RemoveCash(playerPosition[1] + difference, orientation));
            }
            Instantiate(prefab, new Vector3(x, y, 0), Quaternion.identity);
        }
    }
}

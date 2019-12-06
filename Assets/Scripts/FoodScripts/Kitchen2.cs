using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kitchen2 : Kitchen
{
    protected void toKitchenCounter()
    {

        void SetTransform(int x)
        {
            counterTop[x].transform.position = new Vector3((float)0-x, (float)5.5, 0);
            counterTop[x].GetComponent<SpriteRenderer>().sortingOrder = 61;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kitchen2 : Kitchen
{
    protected override void toKitchenCounter()
    {

        void SetTransform(int x)
        {
            counterTop[x].transform.position = new Vector3((float)0-x, (float)4.5, 0); //5.5 to 4.5
            counterTop[x].GetComponent<SpriteRenderer>().sortingOrder = 61;
        }

		 void AttemptToPlaceFoodAtIndex(int index)
        {
            if (cookedFood.Count > 0 && counterTop[index] is null)
            {
                counterTop[index] = cookedFood.Dequeue().gameObject;
                SetTransform(index);
            }
        }

        for(int x = 0; x < counterTop.Length; x++)
        {
            AttemptToPlaceFoodAtIndex(x);
        }

    }

	public override void ReceiveFoodRequest(Vector3 playerPosition)
    {
        void CheckIndex(int index)
        {
            if (playerPosition.x < counterTop[index].transform.position.x + .1 && playerPosition.x > counterTop[index].transform.position.x-.1) //changed y's to x's because hittin it from the front instead of side
            {
                objectManager.GivePlayerFood(counterTop[index]);
                counterTop[index] = null;
                return;
            }
        }

        for(int x = 0; x < kitchenSize; x++)
        {
            if(!(counterTop[x] is null))
            {
                CheckIndex(x);
            }
            
        }
    }
}

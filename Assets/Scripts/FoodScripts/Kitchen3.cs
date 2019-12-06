using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kitchen3 : Kitchen
{
    protected override void toKitchenCounter()
    {

        void SetTransform(int x)
        {
			if (x == 0 || x == 3){
				counterTop[x].transform.position = new Vector3((float)0-x, (float)0.5, 0); 
				counterTop[x].GetComponent<SpriteRenderer>().sortingOrder = 61;
			}
			else{
				if (x == 1 || x == 2){
					counterTop[x].transform.position = new Vector3((float)0-x, (float)1.5, 0); 
					counterTop[x].GetComponent<SpriteRenderer>().sortingOrder = 61;
				}
				else if (x == 4 || x == 5){
					counterTop[x].transform.position = new Vector3((float)0-x, (float)(-0.5), 0); 
					counterTop[x].GetComponent<SpriteRenderer>().sortingOrder = 61;
				}
				
			}
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
            if (index == 0 || index == 3){
				if (playerPosition.x < counterTop[index].transform.position.x + .1 && playerPosition.x > counterTop[index].transform.position.x-.1) //changed y's to x's because hittin it from the front instead of side
				{
					objectManager.GivePlayerFood(counterTop[index]);
					counterTop[index] = null;
					return;
				}
			}else{
				if (playerPosition.y < counterTop[index].transform.position.y + .1 && playerPosition.y > counterTop[index].transform.position.y-.1) //changed y's to x's because hittin it from the front instead of side
					{
						objectManager.GivePlayerFood(counterTop[index]);
						counterTop[index] = null;
						return;
					}
			
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
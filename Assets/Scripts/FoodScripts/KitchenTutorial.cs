using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenTutorial : Kitchen
{
    bool spawned = false;
    protected override void toKitchenCounter()
    {
        spawned = true;
        counterTop[0] = cookedFood.Dequeue().gameObject;
        counterTop[0].transform.position = new Vector3((float)-7.5, (float)1.5, 0);
    }

    public override void ReceiveFoodRequest(Vector3 playerPosition)
    {
        objectManager.GivePlayerFood(counterTop[0]);
        counterTop[0] = null;
    }

    public override void BeatOccured()
    {
        if (kitchenTimer > 0)
        {
            kitchenTimer--;
            if (kitchenTimer == 0)
            {
                cookedFood.Enqueue(foodBeingCooked);
                foodBeingCooked = null;
            }
        }

        if (!spawned && cookedFood.Count > 0 && counterTop[0] is null)
        {
            toKitchenCounter();
        }

    }
}

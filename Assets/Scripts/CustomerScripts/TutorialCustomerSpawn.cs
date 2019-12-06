using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCustomerSpawn : CustomerQueueTracker
{
    public virtual void BeatOccured()
    {
        if (customerRequestQueue.Count > 0 && customerEntranceQueue.Count > 0)
        {
            if (UnityEngine.Random.Range(0.0f, 1.0f) < .35)
            {
                customerRequestQueue.Enqueue(customerRequestQueue.Dequeue());
            }
            GiveCustomer(customerRequestQueue.Dequeue(), customerEntranceQueue.Dequeue());
        }
    }
}

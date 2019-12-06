using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCustomerSpawn : CustomerQueueTracker
{
    public virtual void BeatOccured()
    {
        if (customerRequestQueue.Count > 0 && customerEntranceQueue.Count > 0)
        {
            GiveCustomer(customerRequestQueue.Dequeue(), customerEntranceQueue.Dequeue());
        }
    }
}

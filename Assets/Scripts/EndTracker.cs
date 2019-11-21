using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTracker : MonoBehaviour
{
    public float tipThreshhold;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateEndScreen(float totalTips)
    {
        if(totalTips < tipThreshhold)
        {
            //todo failed screen is set to active
        }
        else
        {
            //todo success screen is set to active
        }
    }
}

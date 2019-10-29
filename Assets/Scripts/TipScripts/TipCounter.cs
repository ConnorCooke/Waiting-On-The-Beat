using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipCounter : MonoBehaviour
{
    private float totalTips = 0;
    private float tipMultiplier = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Adds customer tip to totalTips base on tipMultiplier
    void addTip(float tipValue)
    {
        float tipToAdd = 0;

        // TODO: discuss with group how tipMultiplier should work
        if (tipMultiplier != 0)
        {
            tipToAdd = tipValue * tipMultiplier;
        } else
        {
            tipToAdd = tipValue;
        }

        totalTips += tipToAdd;  
    }

    void updateTipMultiplier(float newMultiplier)
    {
        tipMultiplier = newMultiplier;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

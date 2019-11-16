using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipCounter : MonoBehaviour
{
    private float tipMultiplier;
    private float currTotalTips;
    public ObjectManager objectManager;

    // Start is called before the first frame update
    void Start()
    {
        tipMultiplier = 0f;
        currTotalTips = 0;
    }

    /**
     * Adds customer tip to totalTips based on tipMultiplier
     */
    void addTip(float tipValue)
    {
        float tipToAdd = 0f;

        // TODO: discuss with group how tipMultiplier should work
        if (tipMultiplier != 0f)
        {
            tipToAdd = tipValue * tipMultiplier;
        } else
        {
            tipToAdd = tipValue;
        }

        currTotalTips += tipToAdd;
    }

    /**
     * Updates the tipMultiplier value
     */
    void updateTipMultiplier(float newMultiplier)
    {
        tipMultiplier = newMultiplier;
    }

    /**
     * 
     */
     void receiveScoreRequest()
    {
        objectManager.GiveTipTotal(currTotalTips);
        // add tips earned from lvl to PlayerData.totalTips value
        PlayerData.setTotalTips(PlayerData.getTotalTips() + currTotalTips);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

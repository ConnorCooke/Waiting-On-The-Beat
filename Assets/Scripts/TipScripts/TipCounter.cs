using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipCounter : MonoBehaviour
{
    private float tipMultiplier;
    private float currTotalTips;
    public ObjectManager objectManager;

    // Start is called before the first frame update
    void Start()
    {
        tipMultiplier = 0f;
        currTotalTips = 0f;
        GetComponent<Text>().text = "Current Tips:: " + currTotalTips + " $";
    }

    public void RemoveCash(float removal)
    {
        currTotalTips -= removal;

        if (currTotalTips < 0)
        {
            currTotalTips = 0;
        }
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        GetComponent<Text>().text = "Current Tips:: " + currTotalTips + " $";
    }

    /**
     * Adds customer tip to totalTips based on tipMultiplier
     */
    public void AddTip(float tipValue)
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
        UpdateVisual();
    }

    /**
     * Updates the tipMultiplier value
     */
    public void UpdateTipMultiplier(float newMultiplier)
    {
        tipMultiplier = newMultiplier;
    }

    /**
     * 
     */
    public void receiveScoreRequest()
    {
        objectManager.GiveTipTotal(currTotalTips);
    }

    // Update is called once per frame
    public void Update()
    {
        
    }
}

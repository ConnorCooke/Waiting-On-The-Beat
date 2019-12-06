using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboTracker : MonoBehaviour
{
    private int totalCorrect;
    private float currentMultiplier;
    public float maxMultiplier;
    public ObjectManager objectManager;
    private float increaseAmount = 0;
    public int maxCombo;

    // Start is called before the first frame update
    void Start()
    {
        increaseAmount = (maxMultiplier - 1) / maxCombo;
        UpdateVisual();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateVisual()
    {
        GetComponent<Text>().text = "Combo: " + totalCorrect + " Multiplier: " + currentMultiplier;
        if (currentMultiplier == 1.0f && totalCorrect == 0)
        {
            objectManager.UpdateVisualiserCorrectness(0);
        }
        else if (totalCorrect == maxCombo)
        {
            objectManager.UpdateVisualiserCorrectness(2);
        }
        else if (totalCorrect > (maxCombo / 2))
        {
            objectManager.UpdateVisualiserCorrectness(1);
        }
    }

    public void ReceiveCorrectness(bool correct)
    {
        if (correct)
        {
            if(totalCorrect < maxCombo)
            {
                totalCorrect += 1;
            }
            if (currentMultiplier < maxMultiplier)
            {
                currentMultiplier += increaseAmount;
                if(currentMultiplier > maxMultiplier || totalCorrect == maxCombo)
                {
                    currentMultiplier = maxMultiplier;
                }
            }
        }
        else
        {
            totalCorrect = 0;
            currentMultiplier = 1.0f;
        }
        objectManager.GiveComboMultiplier(currentMultiplier);
        UpdateVisual();
    }
}

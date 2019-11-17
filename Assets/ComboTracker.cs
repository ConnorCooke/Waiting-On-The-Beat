using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboTracker : MonoBehaviour
{
    private int totalCorrect;
    private int totalIncorrect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReceiveCorrectness(bool correct)
    {
        if (correct)
        {
            totalCorrect += 1;
        }
        else
        {
            totalIncorrect += 1;
        }

        print("Correct:" + totalCorrect);
        print("Incorrect" + totalIncorrect);
    }
}

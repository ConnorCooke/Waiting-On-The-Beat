using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatVisualizerCorrectnessDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateCorrectnessLevel(int lvl)
    {
        GetComponent<Animator>().SetInteger("ComboLevel", lvl);
    }

    public void Input(bool correctness)
    {
        if (correctness)
        {
            GetComponent<Animator>().SetTrigger("Correct");
        }
        else
        {
            GetComponent<Animator>().SetTrigger("Incorrect");
        }
    }

    private IEnumerator WaitThenResetTriggers(bool correctness)
    {
        yield return new WaitForSeconds((float)0.099);
        if (correctness)
        {
            GetComponent<Animator>().ResetTrigger("Correct");
        }
        else
        {
            GetComponent<Animator>().ResetTrigger("Incorrect");
        }
    }
}

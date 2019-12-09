using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowInstructions : MonoBehaviour
{
    protected GameObject[] pauseObjects;
    protected GameObject[] tutorialObjects;

    // Start is called before the first frame update
    void Start()
    {
        pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
        tutorialObjects = GameObject.FindGameObjectsWithTag("TutorialPause");
        StartCoroutine(stupid());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator stupid()
    {
        yield return new WaitForSeconds((float)0.05);
        foreach (GameObject g in pauseObjects)
        {
            print("true");
            g.SetActive(true);
        }
        foreach (GameObject g in tutorialObjects)
        {
            print("false");
            g.SetActive(false);
        }
    }

    public void dippidydooStuff()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(false);
        }
        foreach (GameObject g in tutorialObjects)
        {
            print("True");
            g.SetActive(true);
        }
    }
}

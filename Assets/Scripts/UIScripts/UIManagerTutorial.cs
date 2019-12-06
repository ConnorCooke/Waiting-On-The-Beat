using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManagerTutorial : UIManager
{
    protected int index = 0;
    GameObject[] tutorialObjects;
    public int[] numberOfTextDuringPause;
    bool paused = false;
    public TypeItOut typeItOut;
    protected int currentLimits = 0;

    protected override void Start()
    {
        base.Start();
        tutorialObjects = GameObject.FindGameObjectsWithTag("TutorialPause");
        hideTutorialPaused();
    }

    public void NextTutorialSection()
    {
        if(currentLimits < numberOfTextDuringPause.Length)
        {
            if (paused)
            {
                typeItOut.WriteOutTutorial();
                index++;
                if (index == numberOfTextDuringPause[currentLimits])
                {
                    paused = false;
                    index = 0;
                    currentLimits++;
                }
            }
            else
            {
                tutorialPauseControl();
                paused = true;
            }
        }
        else
        {

        }
    }
    //controls the pausing of the scene
    public void tutorialPauseControl()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0f;
            showTutorialPaused();
        }
        else if (Time.timeScale == 0)
        {
            Time.timeScale = 1f;
            hideTutorialPaused();
        }
    }

    //shows objects with ShowOnPause tag
    public void showTutorialPaused()
    {
        foreach (GameObject g in tutorialObjects)
        {
            g.SetActive(true);
        }
        nextLevelButton.SetActive(false);
        AudioListener.pause = true;
    }

    //hides objects with ShowOnPause tag
    public void hideTutorialPaused()
    {
        foreach (GameObject g in tutorialObjects)
        {
            g.SetActive(false);
        }
        AudioListener.pause = false;
    }
}

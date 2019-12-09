using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    protected GameObject[] pauseObjects;
    public string nextLevel;
    public float tipThreshold;
    public GameObject nextLevelButton;
    public GameObject playButton;
    public GameObject pauseText;
    protected float tipTotal;

    // Use this for initialization
    protected virtual void Start()
    {
        Time.timeScale = 1; //1
        pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
        hidePaused();
    }

    // Update is called once per frame
    protected virtual void Update()
    {

        //uses the p button to pause and unpause the game
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
                showPaused();
            }
            else if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
                hidePaused();
            }
        }
    }

    public void EndOfLevel(float tipTot)
    {
        tipTotal = tipTot;
        Time.timeScale = 0;
        showPaused();
        playButton.SetActive(false);
        if(tipTotal >= tipThreshold)
        {
            nextLevelButton.SetActive(true);
            pauseText.GetComponent<Text>().text = "SUCCESS";
        }
        else
        {
            pauseText.GetComponent<Text>().text = "YOU'RE FIRED";
        }
    }

    //Reloads the Level
    public void Reload()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    //controls the pausing of the scene
    public void pauseControl()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            showPaused();
        }
        else if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            hidePaused();
        }
    }

    //shows objects with ShowOnPause tag
    public void showPaused()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(true);
        }
        nextLevelButton.SetActive(false);
        AudioListener.pause = true;
    }

    //hides objects with ShowOnPause tag
    public void hidePaused()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(false);
        }
        AudioListener.pause = false;
    }

    //loads inputted level
    public void LoadLevel(string level)
    {
        Application.LoadLevel(level);
    }
}

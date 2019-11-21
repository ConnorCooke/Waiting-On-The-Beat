using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public GameObject MenuPanel;
//public GameObject LevelSelectPanel;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MenuPanel.SetActive(true);
        LevelSelectPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


public void ShowLevelPanel()
    {
         MenuPanel.SetActive(false);
         //LevelSelectPanel.SetActive(true);
    }
 
public void ShowMenuPanel()
    {
         MenuPanel.SetActive(true);
         //LevelSelectPanel.SetActive(false);
    }

public void ReveiveTips()
    {
        //Receive level tips and add them to total tips form the player script and display on menu

        //Display success or fail
    }

public void ReveiveEndOfLevelInfo()
    {
        //If failure, activate restart level button

        //If success, activate NextLevel Button

        //Compare the score(tips) to the mutable threshhold for that level to determine success or failure
    }

public void RestartButton()
    {
        //load same level
    }

public void NextLevel()
    {
        //Load next level
    }

}

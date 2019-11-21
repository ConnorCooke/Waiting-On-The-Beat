using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndTracker : MonoBehaviour
{
    public float tipThreshhold;
    public string curSceneName;
    
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateEndScreen(float totalTips)
    {
        curSceneName = SceneManager.GetActiveScene().name;
        if(totalTips < tipThreshhold)
        {
            //todo failed screen is set to active
            SceneManager.LoadScene("FailResult");
            //FailScreen.setScene(curSceneName);
            GameObject ev = GameObject.FindGameObjectsWithTag("EndResult")[0];
            ev.GetComponent<FailScreen>().setScene(curSceneName);
        }
        else
        {
            //todo success screen is set to active
            SceneManager.LoadScene("SuccessResult");

        }
    }
}

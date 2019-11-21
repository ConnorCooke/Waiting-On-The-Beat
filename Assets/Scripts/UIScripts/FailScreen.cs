using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FailScreen : MonoBehaviour
{
    private string sceneName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setScene(string name)
    {
        sceneName = name;
    }
    public void ExitButton()
    {
        Debug.Log("Exit Button pressed, application would quit in a built project");
        Application.Quit();
    }
    public void RestartButton()
    {
        //load same level
        SceneManager.LoadScene(sceneName);
    }

}

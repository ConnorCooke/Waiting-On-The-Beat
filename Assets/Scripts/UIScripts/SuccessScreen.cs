using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SuccessScreen : MonoBehaviour
{
    public GameObject scoreField;
    private string sceneName;
    // Start is called before the first frame update
    void Start()
    {
        scoreField.GetComponent<UnityEngine.UI.Text>().text = "Score: " + PreviousScene.getTip().ToString();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
    public void ExitButton()
    {
        Debug.Log("Exit Button pressed, application would quit in a built project");

        Application.Quit();
    }
    public void NextLevelButton()
    {
        //load same level
        SceneManager.LoadScene(PreviousScene.getScene().buildIndex + 1);
    }
}

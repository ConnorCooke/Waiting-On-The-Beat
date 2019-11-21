using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PreviousScene : MonoBehaviour
{
    private static float score;
    private static Scene scene;
    public PreviousScene()
    {
        scene = SceneManager.GetActiveScene();
        score = 0f;
    }

    // SETTERS
    public static void setScene(Scene sc)
    {
        scene = sc;
    }
    public static void setTip(float tip)
    {
        score = tip;
    }
    //Getters
    public static Scene getScene()
    {
        return scene;
    }
    public static float getTip()
    {
        return score;
    }
}

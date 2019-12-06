using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeItOut : MonoBehaviour
{
    public Text writeTo;
    public string[] text;
    protected int index = 0;
    bool locked = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WriteOutTutorial()
    {
        if(index< text.Length)
        {
            StartCoroutine(TypeIt(text[index]));
            index++;
        }
    }

    IEnumerator TypeIt(string message)
    {
        if (!locked)
        {
            locked = true;
            writeTo.text = "";
            foreach (char letter in message.ToCharArray())
            {
                writeTo.text += letter;
                yield return new WaitForSecondsRealtime(0.03f);
            }
            yield return new WaitForSecondsRealtime(0.4f);
            locked = false;
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDeleter : MonoBehaviour
{
    public GameObject LaserParent;
    private bool timing = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!timing)
        {
            timing = true;
            StartCoroutine(WaitThenDelete());
        }
    }

    public IEnumerator WaitThenDelete()
    {
        yield return new WaitForSeconds((float)1);
        LaserParent.GetComponent<Animator>().SetTrigger("Beam");
        yield return new WaitForSeconds((float)0.95);
        DestroyImmediate(LaserParent);
    }
}

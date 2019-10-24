using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class filereader : MonoBehaviour
{
    // Reads in text file
    [RuntimeInitializeOnLoadMethod]
    static void LoadJson()
    {
        List<float> beats = new List<float>();
        StreamReader r = new StreamReader("./Assets/beat-test.txt");

        while(!r.EndOfStream)
        {
            string ln = r.ReadLine();
            beats.Add(float.Parse(ln));
        }

        r.Close();

        // Debug.Log(beats[beats.Count-1]);
    }
}


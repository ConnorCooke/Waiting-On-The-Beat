using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float bpm;
    private float moveSpeed;


    //the current position of the song (in seconds)
    float songPosition;
    //the current position of the song (in beats)
    float songPosInBeats;
    //How long a beat lasts
    float secPerBeat;
    //how much time has passed since the song began
    float timesong;


    // Start is called before the first frame update
    void Start()
    {
        //If we're taking a music file and getting the bpm from it, it should happen here


        secPerBeat = 60f / bpm;
        timesong = (float)AudioSettings.dspTime;
        //eventually put a song in here
        //GetComponent<AudioSource>().Play();
    }

    // Update is called once per frame
    void Update()
    {
        //update the position in seconds
        songPosition = (float)(AudioSettings.dspTime - timesong);

        //update the position in beats
        songPosInBeats = songPosition / secPerBeat;

    }

}

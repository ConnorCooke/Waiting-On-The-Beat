using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatRunner : MonoBehaviour
{
    public string fileName;
    public int currentBeat;
    public AudioSource musicSource;
    private float[] beatPositionsInTime;
    public float inputLeeway;
    /***
    
    void Start()
    {
        musicSource = GetComponent<AudioSource>();
        /**
         * TODO:: Convert a file with the name filename into an array of floats equivalent to beat positions
         * 
         *
        currentBeat = 1;

        musicSource.Play();

    }

    void Update()
    {
        if(musicSource.time > beatPositionsInTime[currentBeat + 1])
        {
            currentBeat++;
        }

    }

    private void initializeBeatPositionArray()
    {

    }

    /**
     * When called checks if the current time is close enough to a beat to be considered correct input and returns true if it is.
     * As long as the players input is before the next beat by that time - leeway or it is after the previous beat but before that beat time + leeway
     * the input is acceptable.
     *
    public bool PlayerInputCloseToCurrentBeat()
    {
        float inputTime = musicSource.time;
        if(inputTime < beatPositionsInTime[currentBeat] + inputLeeway || inputTime > beatPositionsInTime[currentBeat+1] - inputLeeway)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    // gets the array of beat positions for use by other object managers
    public float[] getBeatPositions()
    {
        return beatPositionsInTime;
    }

    // getter for current time
    public float getSongTime()
    {
        return musicSource.time;
    }

    */
}

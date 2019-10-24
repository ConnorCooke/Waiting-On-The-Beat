using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatRunner : MonoBehaviour
{
    
    public int currentBeat;
    public AudioSource musicSource;
    private float[] beatPositionsInTime=null;
    public float inputLeeway;
    public TextAsset beatFilePath;
    public ObjectManager objectManager;
    private bool isActing;
    
    
    void Start()
    {
        beatPositionsInTime = JsonHelper.FromJson<float>(beatFilePath.text);
        
        currentBeat = 1;

        musicSource.Play();

    }

    void Update()
    {
        float currentSongTime = musicSource.time;
        if (currentSongTime > beatPositionsInTime[currentBeat + 1])
        {
            currentBeat++;
            objectManager.BeatOccured();
        }
        else if (currentSongTime> beatPositionsInTime[currentBeat]+inputLeeway)
        {
            objectManager.GiveCorrectness(false);
        }

        if (!isActing)
        {
            Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            if (input != Vector2.zero )
            {
                isActing = true;
                objectManager.GiveCorrectness(PlayerInputCloseToCurrentBeat());
                StartCoroutine(WaitForActionToComplete());
            }
        }
    }

    IEnumerator WaitForActionToComplete()
    {
        yield return new WaitForSeconds((float).1);
        isActing = false;
    }

    /**
     * When called checks if the current time is close enough to a beat to be considered correct input and returns true if it is.
     * As long as the players input is before the next beat by that time - leeway or it is after the previous beat but before that beat time + leeway
     * the input is acceptable.
     */
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

    
}

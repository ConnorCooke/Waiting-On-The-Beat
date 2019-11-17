using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class BeatRunner : MonoBehaviour
{
    
    public int currentBeat;
    public AudioSource musicSource;
    private static List<float> beatPositionsInTime = new List<float>();
    private static List<float> beatObjectSpawnTime = new List<float>();
    public float inputLeeway;
    public static string beatFilePath = "./Assets/Songs/beat-test.txt";
    public ObjectManager objectManager;
    private bool isActing;
    private bool beatHit = false;
    private bool noInput = true;
    private bool beatHitReset = false;
    private bool noInputReset = false;
    public int currentSpawn = 0;
    
    
    void Start()
    {
        LoadJson();
        
        currentBeat = 1;

        musicSource.Play();

    }

    static void LoadJson()
    {
        StreamReader r = new StreamReader(beatFilePath);
        int count = 0;
        while (!r.EndOfStream)
        {
            string ln = r.ReadLine();
            float time = float.Parse(ln);
            beatPositionsInTime.Add(time);
            if (count<3)
            {
                count++;
            }
            else
            {
                beatObjectSpawnTime.Add(time - 2);
            }
            
        }

        r.Close();

        // Debug.Log(beats[beats.Count-1]);
    }

    void Update()
    {
        float currentSongTime = musicSource.time;
        if (currentSongTime > beatPositionsInTime[currentBeat + 1])
        {
            currentBeat++;
            objectManager.BeatOccured();
            noInputReset = false;
            beatHitReset = false;
        }
        else if (currentSongTime > beatPositionsInTime[currentBeat] + inputLeeway)
        {
            if(!noInputReset && !beatHitReset)
            {
                if (!beatHit && noInput) {
                    objectManager.GiveCorrectness(false);
                }
                noInput = true;
                beatHit = false;
                noInputReset = true;
                beatHitReset = true;
            }
        }

        if (currentSongTime > beatObjectSpawnTime[currentSpawn])
        {
            objectManager.SpawnBeatVisual();
            currentSpawn++;
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
        yield return new WaitForSeconds((float).25);
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
        noInput = false;
        if(inputTime < beatPositionsInTime[currentBeat] + inputLeeway || inputTime > beatPositionsInTime[currentBeat+1] - inputLeeway)
        {
            beatHit = true;
            return true;
        }
        else
        {
            beatHit = false;
            return false;
        }

    }

    // gets the array of beat positions for use by other object managers
    public List<float> getBeatPositions()
    {
        return beatPositionsInTime;
    }

    // getter for current time
    public float getSongTime()
    {
        return musicSource.time;
    }

    
}

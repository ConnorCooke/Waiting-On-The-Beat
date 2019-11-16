using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Globalization;

public class BeatRunner : MonoBehaviour
{
    
    public int currentBeat;
    public AudioSource musicSource;
    private static List<double> beatPositionsInTime = new List<double>();
    private static List<double> beatObjectSpawnTime = new List<double>();
    public float inputLeeway;
    public static string beatFilePath = "./Assets/Songs/beat-test.txt";
    public ObjectManager objectManager;
    private bool isActing;
    bool beatHit = false;
    bool alreadyFailed = false;
    public int currentSpawn = 0;
    
    
    void Start()
    {
        loadBeatsFromFile();
        
        currentBeat = 1;

        musicSource.Play();

    }

    static void loadBeatsFromFile()
    {
        StreamReader r = new StreamReader(beatFilePath);
        int count = 0;
        while (!r.EndOfStream)
        {
            string ln = r.ReadLine();            
            double time = double.Parse(ln, CultureInfo.InvariantCulture.NumberFormat);
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
    }

    void Update()
    {
        float currentSongTime = musicSource.time;
        // Debug.Log(beatPositionsInTime);
        if (currentSongTime > beatPositionsInTime[currentBeat + 1])
        {
            currentBeat++;
            objectManager.BeatOccured();
        }
        else if (currentSongTime > beatPositionsInTime[currentBeat] + inputLeeway)
        {
            if (!beatHit && !alreadyFailed) {
                objectManager.GiveCorrectness(false);
                alreadyFailed = true;
            }
            else
            {
                beatHit = false;
            }
        }

        if(currentSongTime > beatObjectSpawnTime[currentSpawn])
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
    public List<double> getBeatPositions()
    {
        return beatPositionsInTime;
    }

    // getter for current time
    public float getSongTime()
    {
        return musicSource.time;
    }

    
}

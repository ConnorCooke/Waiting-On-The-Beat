using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Globalization;

public class BeatRunner : MonoBehaviour
{
    
    private int currentBeat = 0;
    public AudioSource musicSource;
    private static List<double> beatPositionsInTime = new List<double>();
    private static List<double> beatObjectSpawnTime = new List<double>();
    public float inputLeeway;
    public static string beatFilePath = "./Assets/Songs/beat-test.txt";
    public ObjectManager objectManager;
    private bool isActing;
    private bool beatHit = false;
    private bool noInput = true;
    private bool beatHitReset = false;
    private bool noInputReset = false;
    private int currentSpawn = 0;
    private static int spawnCount = 0;
    private static int count = 0;
    
    
    void Start()
    {
        loadBeatsFromFile();
        
        currentBeat = 0;

        musicSource.Play();

    }

    static void loadBeatsFromFile()
    {
        StreamReader r = new StreamReader(beatFilePath);
        int beatCount = 0;
        while (!r.EndOfStream)
        {
            string ln = r.ReadLine();            
            double time = double.Parse(ln, CultureInfo.InvariantCulture.NumberFormat);
            beatPositionsInTime.Add(time);            
            
            if (beatCount<3)
            {
                beatCount++;
            }
            else
            {
                beatObjectSpawnTime.Add(time - 2);
            }
            
        }
        count = beatPositionsInTime.Count;
        spawnCount = beatObjectSpawnTime.Count;
        r.Close();
    }

    void Update()
    {
        float currentSongTime = musicSource.time;
        // Debug.Log(beatPositionsInTime);
        if ((currentBeat + 1) < count && (currentSpawn + 1) < spawnCount)
        {
            if (currentSongTime > beatPositionsInTime[currentBeat + 1])
            {
                currentBeat++;
                objectManager.BeatOccured();
                noInputReset = false;
                beatHitReset = false;
            }
            else if (currentSongTime > beatPositionsInTime[currentBeat] + inputLeeway)
            {
                if (!noInputReset && !beatHitReset)
                {
                    if (!beatHit && noInput)
                    {
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

                if (input != Vector2.zero)
                {
                    isActing = true;
                    objectManager.GiveCorrectness(PlayerInputCloseToCurrentBeat());
                    StartCoroutine(WaitForActionToComplete());
                }
            }
        }
        else
        {
            objectManager.EndLevel();
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

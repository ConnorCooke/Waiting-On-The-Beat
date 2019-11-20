using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCreator : MonoBehaviour
{
    public PlayerSpriteManager playerSpriteManager;
    private int direction = 0;

    // Start is called before the first frame update
    void Start()
    {
        RandomizePlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RandomizePlayer()
    {
        PlayerVisual.setHeadshape(UnityEngine.Random.Range(0, 6));

        PlayerVisual.setAccessory(UnityEngine.Random.Range(0, 16));

        PlayerVisual.setNose(UnityEngine.Random.Range(0, 6));

        PlayerVisual.setSkinTone(UnityEngine.Random.Range(0, 6));

        PlayerVisual.setHairType(UnityEngine.Random.Range(0, 14));

        PlayerVisual.setHairColour(UnityEngine.Random.Range(0, 8));

        playerSpriteManager.LoadAll();
    }

    public void ChangeHeadshape()
    {
        int newHeadshape = PlayerVisual.getHeadshape() + 1;
        if (newHeadshape == 6)
        {
            newHeadshape = 0;
        }
        PlayerVisual.setHeadshape(newHeadshape);
        playerSpriteManager.LoadHeadshape();
        FaceDirection();
    }

    public void ChangeAccessory()
    {
        int newAccessory = PlayerVisual.getAccessory()+1;
        if (newAccessory == 16)
        {
            newAccessory = 0;
        }
        PlayerVisual.setAccessory(newAccessory);
        playerSpriteManager.LoadAccessory();
        FaceDirection();
    }

    public void ChangeNose()
    {
        int newNose = PlayerVisual.getNose() + 1;
        if (newNose == 6)
        {
            newNose = 0;
        }
        PlayerVisual.setNose(newNose);
        playerSpriteManager.LoadNose();
        FaceDirection();
    }

    public void ChangeSkinTone()
    {
        int skinTone = PlayerVisual.getSkinTone() + 1;
        if (skinTone == 6)
        {
            skinTone = 0;
        }
        PlayerVisual.setSkinTone(skinTone);
        playerSpriteManager.LoadNose();

        playerSpriteManager.LoadHeadshape();
        FaceDirection();
    }

    public void ChangeMouth()
    {
        int newMouth = PlayerVisual.getMouth() + 1;
        if (newMouth == 6)
        {
            newMouth = 0;
        }
        PlayerVisual.setMouth(newMouth);
        playerSpriteManager.LoadMouth();
        FaceDirection();
    }

    public void ChangeHairType()
    {
        int newHairType = PlayerVisual.getHairType() + 1;
        if (newHairType == 14)
        {
            newHairType = 0;
        }
        PlayerVisual.setHairType(newHairType);
        playerSpriteManager.LoadHairType();
        FaceDirection();
    }

    public void ChangeHairColour()
    {
        int newHairColour = PlayerVisual.getHairColour() + 1;
        if (newHairColour == 8)
        {
            newHairColour = 0;
        }
        PlayerVisual.setHairColour(newHairColour);
        playerSpriteManager.LoadHairType();
        FaceDirection();
    }

    private void FaceDirection()
    {
        if (direction == 0)
        {
            playerSpriteManager.faceSouth(45);
        }
        else if (direction == 1)
        {
            playerSpriteManager.faceEast(45);
        }
        else if (direction == 2)
        {
            playerSpriteManager.faceNorth(45);
        }
    }

    public void ChangeDirection()
    {
        direction++;
        if(direction == 3)
        {
            direction = 0;
        }
        FaceDirection();
        GetComponent<Animator>().SetInteger("Direction", direction);
    }

}

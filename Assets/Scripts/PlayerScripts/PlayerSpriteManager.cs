using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteManager : MonoBehaviour
{
    public GameObject headShape;
    public GameObject nose;
    public GameObject mouth;
    public GameObject accessory;
    public GameObject hair;
    public GameObject body;
    public GameObject legontheleft;
    public GameObject legontheright;
    public GameObject armontheleft;
    public GameObject armontheright;
    public GameObject handontheleft;
    public GameObject handontheright;
    public Sprite empty;
    private Sprite[][] sprites = new Sprite[][]
    {
        new Sprite[3],//headshapes 0
        new Sprite[3],//noses 1 
        new Sprite[3],//accessory 2
        new Sprite[4],//hair 3
        new Sprite[4],//body 4
        new Sprite[4],//legontheleft 5
        new Sprite[4],//legontheright 6
        new Sprite[4],//armontheleft 7
        new Sprite[4],//armontheright 8
        new Sprite[1]//mouth 9
    };

    public void faceNorth(int baseLayer)
    {
        headShape.GetComponent<SpriteRenderer>().sprite = sprites[0][0];
        nose.GetComponent<SpriteRenderer>().sprite = empty;
        mouth.GetComponent<SpriteRenderer>().sprite = empty;
        accessory.GetComponent<SpriteRenderer>().sprite = empty;
        hair.GetComponent<SpriteRenderer>().sprite = sprites[3][2];
        body.GetComponent<SpriteRenderer>().sprite = sprites[4][2];
        legontheleft.GetComponent<SpriteRenderer>().sprite = sprites[5][2];
        legontheright.GetComponent<SpriteRenderer>().sprite = sprites[6][2];
        armontheleft.GetComponent<SpriteRenderer>().sprite = sprites[7][2];
        armontheright.GetComponent<SpriteRenderer>().sprite = sprites[8][2];

        headShape.GetComponent<SpriteRenderer>().sortingOrder = baseLayer - 3;
        nose.GetComponent<SpriteRenderer>().sortingOrder = baseLayer + 2;
        accessory.GetComponent<SpriteRenderer>().sortingOrder = baseLayer + 3;
        hair.GetComponent<SpriteRenderer>().sortingOrder = baseLayer + 4;
        body.GetComponent<SpriteRenderer>().sortingOrder = baseLayer + -2;
        legontheleft.GetComponent<SpriteRenderer>().sortingOrder = baseLayer - 4;
        legontheright.GetComponent<SpriteRenderer>().sortingOrder = baseLayer - 4;
        armontheleft.GetComponent<SpriteRenderer>().sortingOrder = baseLayer;
        armontheright.GetComponent<SpriteRenderer>().sortingOrder = baseLayer;
        handontheleft.GetComponent<SpriteRenderer>().sortingOrder = baseLayer - 1;
        handontheright.GetComponent<SpriteRenderer>().sortingOrder = baseLayer - 1;
        mouth.GetComponent<SpriteRenderer>().sortingOrder = baseLayer + 1;
    }
    
    public void faceSouth(int baseLayer)
    {
        headShape.GetComponent<SpriteRenderer>().sprite = sprites[0][0];
        nose.GetComponent<SpriteRenderer>().sprite = sprites[1][0];
        accessory.GetComponent<SpriteRenderer>().sprite = sprites[2][0];
        hair.GetComponent<SpriteRenderer>().sprite = sprites[3][0];
        body.GetComponent<SpriteRenderer>().sprite = sprites[4][0];
        legontheleft.GetComponent<SpriteRenderer>().sprite = sprites[5][0];
        legontheright.GetComponent<SpriteRenderer>().sprite = sprites[6][0];
        armontheleft.GetComponent<SpriteRenderer>().sprite = sprites[7][0];
        armontheright.GetComponent<SpriteRenderer>().sprite = sprites[8][0];
        mouth.GetComponent<SpriteRenderer>().sprite = sprites[9][0];

        headShape.GetComponent<SpriteRenderer>().sortingOrder = baseLayer-2;
        nose.GetComponent<SpriteRenderer>().sortingOrder = baseLayer + 2;
        accessory.GetComponent<SpriteRenderer>().sortingOrder = baseLayer + 3;
        hair.GetComponent<SpriteRenderer>().sortingOrder = baseLayer + 4;
        body.GetComponent<SpriteRenderer>().sortingOrder = baseLayer + -3;
        legontheleft.GetComponent<SpriteRenderer>().sortingOrder = baseLayer - 4;
        legontheright.GetComponent<SpriteRenderer>().sortingOrder = baseLayer - 4;
        armontheleft.GetComponent<SpriteRenderer>().sortingOrder = baseLayer;
        armontheright.GetComponent<SpriteRenderer>().sortingOrder = baseLayer;
        handontheleft.GetComponent<SpriteRenderer>().sortingOrder = baseLayer - 1;
        handontheright.GetComponent<SpriteRenderer>().sortingOrder = baseLayer - 1;
        mouth.GetComponent<SpriteRenderer>().sortingOrder = baseLayer + 1;
    }

    public void faceEast(int baseLayer)
    {
        headShape.GetComponent<SpriteRenderer>().sprite = sprites[0][2];
        nose.GetComponent<SpriteRenderer>().sprite = sprites[1][2];
        accessory.GetComponent<SpriteRenderer>().sprite = sprites[2][2];
        hair.GetComponent<SpriteRenderer>().sprite = sprites[3][1];
        body.GetComponent<SpriteRenderer>().sprite = sprites[4][1];
        legontheleft.GetComponent<SpriteRenderer>().sprite = sprites[5][1];
        legontheright.GetComponent<SpriteRenderer>().sprite = sprites[6][1];
        armontheleft.GetComponent<SpriteRenderer>().sprite = sprites[7][1];
        armontheright.GetComponent<SpriteRenderer>().sprite = sprites[8][1];
        mouth.GetComponent<SpriteRenderer>().sprite = empty;

        headShape.GetComponent<SpriteRenderer>().sortingOrder = baseLayer - 2;
        nose.GetComponent<SpriteRenderer>().sortingOrder = baseLayer + 2;
        accessory.GetComponent<SpriteRenderer>().sortingOrder = baseLayer + 3;
        hair.GetComponent<SpriteRenderer>().sortingOrder = baseLayer + 4;
        body.GetComponent<SpriteRenderer>().sortingOrder = baseLayer + -3;
        legontheleft.GetComponent<SpriteRenderer>().sortingOrder = baseLayer - 5;
        legontheright.GetComponent<SpriteRenderer>().sortingOrder = baseLayer - 4;
        armontheleft.GetComponent<SpriteRenderer>().sortingOrder = baseLayer;
        armontheright.GetComponent<SpriteRenderer>().sortingOrder = baseLayer - 4;
        handontheleft.GetComponent<SpriteRenderer>().sortingOrder = baseLayer - 5;
        handontheright.GetComponent<SpriteRenderer>().sortingOrder = baseLayer - 1;
        mouth.GetComponent<SpriteRenderer>().sortingOrder = baseLayer + 1;
    }

    public void faceWest(int baseLayer)
    {
        headShape.GetComponent<SpriteRenderer>().sprite = sprites[0][1];
        nose.GetComponent<SpriteRenderer>().sprite = sprites[1][1];
        mouth.GetComponent<SpriteRenderer>().sprite = empty;
        accessory.GetComponent<SpriteRenderer>().sprite = sprites[2][1];
        hair.GetComponent<SpriteRenderer>().sprite = sprites[3][3];
        body.GetComponent<SpriteRenderer>().sprite = sprites[4][3];
        legontheleft.GetComponent<SpriteRenderer>().sprite = sprites[5][3];
        legontheright.GetComponent<SpriteRenderer>().sprite = sprites[6][3];
        armontheleft.GetComponent<SpriteRenderer>().sprite = sprites[7][3];
        armontheright.GetComponent<SpriteRenderer>().sprite = sprites[8][3];

        headShape.GetComponent<SpriteRenderer>().sortingOrder = baseLayer - 2;
        nose.GetComponent<SpriteRenderer>().sortingOrder = baseLayer + 2;
        accessory.GetComponent<SpriteRenderer>().sortingOrder = baseLayer + 3;
        hair.GetComponent<SpriteRenderer>().sortingOrder = baseLayer + 4;
        body.GetComponent<SpriteRenderer>().sortingOrder = baseLayer + -3;
        legontheleft.GetComponent<SpriteRenderer>().sortingOrder = baseLayer - 5;
        legontheright.GetComponent<SpriteRenderer>().sortingOrder = baseLayer - 4;
        armontheleft.GetComponent<SpriteRenderer>().sortingOrder = baseLayer - 4;
        armontheright.GetComponent<SpriteRenderer>().sortingOrder = baseLayer;
        handontheleft.GetComponent<SpriteRenderer>().sortingOrder = baseLayer - 5;
        handontheright.GetComponent<SpriteRenderer>().sortingOrder = baseLayer - 1;
        mouth.GetComponent<SpriteRenderer>().sortingOrder = baseLayer + 1;
    }

    private int GetHand()
    {
        switch (PlayerVisual.getSkinTone())
        {
            case 0:
                return 10;
            case 1:
                return 20;
            case 2:
                return 11;
            case 3:
                return 21;
            case 4:
                return 12;
            default:
                return 22;
        }
    }

    public void LoadAccessory()
    {
        Sprite[] accessory = Resources.LoadAll<Sprite>("Sprites/Customers/accessories");

        sprites[2][0] = accessory[PlayerVisual.getAccessory()];
        sprites[2][1] = accessory[PlayerVisual.getAccessory() + 16];

        Sprite[] accessoryEast = Resources.LoadAll<Sprite>("Sprites/Customers/accessorieseast");
        sprites[2][2] = accessoryEast[PlayerVisual.getAccessory()];
    }

    public void LoadHeadshape()
    {
        Sprite[] heads = Resources.LoadAll<Sprite>("Sprites/Customers/faceshapes");

        sprites[0][0] = heads[PlayerVisual.getHeadshape() + PlayerVisual.getSkinTone() * 12];
        sprites[0][1] = heads[PlayerVisual.getHeadshape() + 6 + PlayerVisual.getSkinTone() * 12];

        Sprite[] headEast = Resources.LoadAll<Sprite>("Sprites/Customers/faceeast");

        sprites[0][2] = headEast[PlayerVisual.getHeadshape() + 6 * PlayerVisual.getSkinTone()];
        Sprite[] bodyParts = Resources.LoadAll<Sprite>(PlayerVisual.getBody());
        handontheleft.GetComponent<SpriteRenderer>().sprite = bodyParts[GetHand()];
        handontheright.GetComponent<SpriteRenderer>().sprite = bodyParts[GetHand()];
    }

    public void LoadNose()
    {
        Sprite[] noses = Resources.LoadAll<Sprite>("Sprites/Customers/noses");

        sprites[1][0] = noses[PlayerVisual.getNose()];
        sprites[1][1] = noses[PlayerVisual.getNose() + 6 * (PlayerVisual.getSkinTone() + 1)];

        Sprite[] noseEast = Resources.LoadAll<Sprite>("Sprites/Customers/nosesEast");
        sprites[1][2] = noseEast[PlayerVisual.getNose() + 6 * PlayerVisual.getSkinTone()];
    }

    public void LoadHairType()
    {
        Sprite[] hairTypes = Resources.LoadAll<Sprite>("Sprites/Customers/hair");

        sprites[3][0] = hairTypes[PlayerVisual.getHairType() + PlayerVisual.getHairColour() * 56];
        sprites[3][1] = hairTypes[PlayerVisual.getHairType() + 28 + PlayerVisual.getHairColour() * 56];
        sprites[3][2] = hairTypes[PlayerVisual.getHairType() + 42 + PlayerVisual.getHairColour() * 56];
        sprites[3][3] = hairTypes[PlayerVisual.getHairType() + 14 + PlayerVisual.getHairColour() * 56];
    }

    public void LoadMouth()
    {
        Sprite[] mouths = Resources.LoadAll<Sprite>("Sprites/Customers/mouths");

        sprites[9][0] = mouths[PlayerVisual.getMouth()];
    }

    public void LoadBody()
    {
        Sprite[] bodyParts = Resources.LoadAll<Sprite>(PlayerVisual.getBody());

        sprites[4][0] = bodyParts[8];//body
        sprites[4][1] = bodyParts[13];
        sprites[4][2] = bodyParts[1];
        sprites[4][3] = bodyParts[5];

        sprites[5][0] = bodyParts[9];//legonleft
        sprites[5][1] = bodyParts[23];
        sprites[5][2] = bodyParts[15];
        sprites[5][3] = bodyParts[17];

        sprites[6][0] = bodyParts[19];//legonright
        sprites[6][1] = bodyParts[14];
        sprites[6][2] = bodyParts[16];
        sprites[6][3] = bodyParts[6];

        sprites[7][0] = bodyParts[2];//armonleft
        sprites[7][1] = bodyParts[4];
        sprites[7][2] = bodyParts[18];
        sprites[7][3] = bodyParts[3];

        sprites[8][0] = bodyParts[0];//armonright
        sprites[8][1] = bodyParts[3];
        sprites[8][2] = bodyParts[7];
        sprites[8][3] = bodyParts[4];
    }

    public void LoadAll()
    {
        LoadHeadshape();

        LoadNose();

        LoadAccessory();

        LoadHairType();

        LoadBody();

        LoadMouth();
        faceSouth(35);
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadAll();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

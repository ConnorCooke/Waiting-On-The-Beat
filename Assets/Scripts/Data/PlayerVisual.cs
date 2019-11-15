using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisual
{
    private static string body = "Sprites/Customers/body/suitbody";
    private static int accessory = 13;
    private static int hairColour = 4;
    private static int hairType = 5;
    private static int mouth = 5;
    private static int nose = 0;
    private static int skintone = 4;
    private static int headShape = 0;

    public static string getBody()
    {
        return body;
    }

    public static void setBody(string bod)
    {
        body = bod;
    }

    public static int getHairColour()
    {
        return hairColour;
    }

    public static void setHairColour(int colour)
    {
        hairColour = colour;
    }

    public static int getHairType()
    {
        return hairType;
    }

    public static void setHairType(int type)
    {
        hairType = type;
    }

    public static int getMouth()
    {
        return mouth;
    }

    public static void setMouth(int muth)
    {
        mouth = muth;
    }

    public static int getHeadshape()
    {
        return headShape;
    }

    public static void setHeadshape(int shape)
    {
        headShape = shape;
    }

    public static int getSkinTone()
    {
        return skintone;
    }

    public static void setSkinTone(int tone)
    {
        skintone = tone;
    }

    public static int getNose()
    {
        return nose;
    }

    public static void setNose(int nos)
    {
        nose = nos;
    }

    public static int getAccessory()
    {
        return accessory;
    }

    public static void setAccessory(int access)
    {
        accessory = access;
    }
}

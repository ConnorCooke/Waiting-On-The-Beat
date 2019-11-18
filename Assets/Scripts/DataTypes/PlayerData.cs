using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    private static float totalTips;
    private static string playerName;

    public PlayerData() 
    {
        totalTips = 0f;
        playerName = "";
    } 

    // SETTERS
    public static void setTotalTips(float newTipVal)
    {
        totalTips = newTipVal;
    }

    public static void setPlayerName(string newName)
    {
        playerName = newName;
    }

    // GETTERS
    public static float getTotalTips()
    {
        return totalTips;
    }

    public static string getPlayerName()
    {
        return playerName;
    }
}

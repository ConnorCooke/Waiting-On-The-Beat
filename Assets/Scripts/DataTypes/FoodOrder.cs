using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FoodOrder : MonoBehaviour
{
    
    public string FoodName { get; set; }
    
    public int CookTimer { get; set; }

    public Food FoodObject;

    public void Spawn(int spot)
    {
        Instantiate<Food>(FoodObject, new Vector3(1.46f, (5.49f - spot), 6.58f), Quaternion.identity);
    }

    
}




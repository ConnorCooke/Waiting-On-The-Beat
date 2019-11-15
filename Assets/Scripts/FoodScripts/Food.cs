using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    // Start is called before the first frame update
    string foodName;
    float foodValue;

    void Start()
    {
        
    }

    void Update()
    {

    }

    public void CreateFood(FoodOrder order)
    {
        foodName = order.GetFoodName();
    }

    public string GetName()
    {
        return foodName;
    }
}

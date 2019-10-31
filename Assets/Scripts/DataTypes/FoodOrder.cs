using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodOrder
{
    private string name;
    private int cookingTime;
    private float price;

    public FoodOrder(string nam, int timer, float prce)
    {
        this.name = nam;
        this.cookingTime = timer;
        this.price = prce;
    }

    public string GetFoodName()
    {
        return this.name;
    }

    public int GetCookingTime()
    {
        return this.cookingTime;
    }

    public float GetPrice()
    {
        return this.price;
    }
}




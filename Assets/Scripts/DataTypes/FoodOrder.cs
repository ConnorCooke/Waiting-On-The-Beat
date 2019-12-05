using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodOrder
{
    private int name;
    private int cookingTime;
    private float price;

    public FoodOrder(int nam, int timer, float prce)
    {
        this.name = nam;
        this.cookingTime = timer;
        this.price = prce;
    }

    public int GetFoodName()
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




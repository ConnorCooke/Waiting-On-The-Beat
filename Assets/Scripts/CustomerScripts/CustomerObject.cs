using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CustomerObject : MonoBehaviour
{
    //Delete these:
    private FoodOrder foodOrder;
    private int impatienceLevel = 0;
    private bool isEating = false;
    private int index;
    private Table parentTable;
    private float tipValue;
    private int timer;

    public void SetTimer(int time)
    {
        timer = time;
    }

    public void SetTable(Table table)
    {
        parentTable = table;
    }

    public void SetFoodOrder(FoodOrder order)
    {
        foodOrder = order;
    }

    public void SetIndex(int idx)
    {
        index = idx;
    }

    public void SetTipValue(float tip)
    {
        tipValue = tip;
    }

    public int GetIndex()
    {
        return index;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //leave empty
    }

    public void TrackImpatience()
    {
        if(timer > 12) //cooking time + 2* max walk distance
        {
            impatienceLevel++;
        }
    }

    public void ReceiveFood(GameObject food)
    {
        if(food.GetComponent<Food>().GetName() == foodOrder.GetFoodName())
        {
            parentTable.CustomerReceivedFood(index, food);
        }
        
    }

    public void ReceiveOrderRequest()
    {
        parentTable.GiveFoodOrder(foodOrder);
    }

    public void ReceivePayRequest()
    {
        if (isEating)
        {
            impatienceLevel++;
        }
        else
        {
            parentTable.CustomerPaid(foodOrder.GetPrice() * (tipValue/100), index);
        }
    }

    //customer has an integer with its index within the parent tables array with getter and setter

    public void BeatOccured()
    {   
        if(isEating){
            if(timer > 0){
                timer--;
            }
            else{
                isEating = false;
            }
        }
    }

}
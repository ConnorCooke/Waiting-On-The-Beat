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

    private int timer;

    public void SetFoodOrder(FoodOrder order)
    {
        foodOrder = order;
    }

    void SetIndex(int idx)
    {
        index = idx;
    }

    private int GetIndex()
    {
        return index;
    }
2
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //leave empty
    }

    void TrackImpatience()
    {
        if(timer > 12) //cooking time + 2* max walk distance
        {
            customer.impatienceLevel =+ 1;
        }
    }

    void ReceiveFood(Food food)
    {
        /*
        if(food.GetFoodName() == foodOrder.GetFoodName())
        {
            parentTable.CustomerReceivedFood(index, food);
        }
        */

        // TODO once josh has pushed food related code
    }

    void receiveOrderRequest()
    {
        parentTable.GiveFoodOrder(foodOrder);
    }

    bool isCustomerDoneEating()
    {
        if(isEating == false)
        {
            return true;
        }
        else
        {
            false;
        }
    }

    //customer has an integer with its index within the parent tables array with getter and setter

    void BeatOccured()
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
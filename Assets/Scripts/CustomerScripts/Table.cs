using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Table : MonoBehaviour
{
    public ObjectManager objectManager;

    private int tableID;
    private GameObject[] customers = new GameObject[4];
    private bool requestedCustomer;
    private GameObject[] customersFood = new GameObject[4];

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!requestedCustomer)
        {
            int index = 0;
            while(index < 4)
            {
                if(customers[index] is null)
                {
                    RequestCustomer();
                    index = 4;
                }
                index++;
            }
        }
    }

    /*
     * Based off of the transform of the player the table determines which
     * customer is closest to the player
     */
    private int FindNearestCustomer(Vector3 position)
    {
        float playerx = position.x;
        float playery = position.y;
        float smallestDifference = 200000;
        int nearestCustomer = -1;

        for (int index = 0; index < customers.Length; index++)
        {
            if(!(customers[index] is null)) {
                float diff = Math.Abs(customers[index].transform.position.x - playerx) + Math.Abs(customers[index].transform.position.y - playery);
                if (smallestDifference > diff)
                {
                    smallestDifference = diff;
                    nearestCustomer = index;
                }
            }
        }
        return nearestCustomer;
    }

    public void SetTableID(int id)
    {
        tableID = id;
    }

    public int GetTableID()
    {
        return tableID;
    }

    private void RequestCustomer()
    {
        objectManager.RequestCustomer(GetTableID());
        requestedCustomer = true;
    }

    /*
     * Determines where no customer is seated, places the customer in that position of
     * the customers array and then changes its transform so it is seated in the correct 
     * position. When this is completed the table is able to request customers again
     */
    public void ReceiveCustomer(GameObject customer)
    {
        void SetCustomerTransform(int idx)
        {
            void SetTransform(float x, float y)
            {
                float tablex = this.transform.position.x;
                float tabley = this.transform.position.y;
                float tablez = this.transform.position.z;
                customers[idx].transform.position = new Vector3((tablex + x), (tabley + y), tablez);
                customers[idx].GetComponent<CustomerObject>().SetTable(this);
            }
            if (idx == 0)
            {
                SetTransform(-1, (float)1.5);
            }
            else if (idx == 1)
            {
                SetTransform(1, (float)1.5);
            }
            else if (idx == 2)
            {
                SetTransform(-1, (float)-1.5);
            }
            else
            {
                SetTransform(1, (float)-1.5);
            }
        }

        bool notPlaced = true;
        int index = 0;
        while (notPlaced)
        {
            if(customers[index] is null)
            {
                customers[index] = customer;
                customer.GetComponent<CustomerObject>().SetIndex(index);
                SetCustomerTransform(index);
                CustomerReceived(index);
                notPlaced = false;
            }
            index++;
        }
        requestedCustomer = false;
    }

    private void CustomerReceived(int index)
    {
        objectManager.CustomerReadyToOrder(customers[index].transform.position);
    }

    public void CustomerReceivedFood(int index, GameObject food)
    {
        void SetFoodTransform(int indx)
        {
            void SetTransform(float yChange )
            {
                Vector3 customerPosition = customers[indx].transform.position;
                customersFood[indx].transform.position = new Vector3(customerPosition.x, customerPosition.y + yChange, customerPosition.z);
            }

            if(indx < 2){
                SetTransform(-1);
            }
            else{
                SetTransform(1);
            }
        }
        objectManager.CustomerEating(customers[index].transform.position);
        customersFood[index] = food;
        SetFoodTransform(index);
    }

    /*
     * When a pay request is successful the customer is despawned and the tip amount is added
     * to the total tips, as well the tile that customer was on is no longer interactable
     */
    public void CustomerPaid(float tip, int index)
    {
        Vector3 pos = customers[index].transform.position;
        objectManager.CustomerPaid(tip, new Vector3(pos.x, pos.y, pos.z));
        //todo remove customer animation activations
        DestroyImmediate(customers[index]);
        print(customersFood[index].GetComponent<Food>().GetName());
        DestroyImmediate(customersFood[index]);
        customers[index] = null;
        customersFood[index] = null;
    }

    /*
     * Whenever a beat occurs the table informs the customers that the beat occured
     */
    public void BeatOccurred()
    {
        for(int index = 0; index < customers.Length; index++)
        {
            if(!(customers[index] is null))
            {
                customers[index].GetComponent<CustomerObject>().BeatOccured();
            }
        }
    }

    public void ReceiveFood(GameObject food, Vector3 position)
    {
        customers[FindNearestCustomer(position)].GetComponent<CustomerObject>().ReceiveFood(food);
    }

    public void ReceiveOrderRequest(Vector3 position)
    {
        int nearest = FindNearestCustomer(position);
        customers[nearest].GetComponent<CustomerObject>().ReceiveOrderRequest();
        objectManager.CustomerOrdered(customers[nearest].transform.position);
    }

    public void ReceivePayRequest(Vector3 position)
    {
        customers[FindNearestCustomer(position)].GetComponent<CustomerObject>().ReceivePayRequest();
    }

    public void GiveFoodOrder(FoodOrder order){
        objectManager.GivePlayerOrder(order);
    }
}

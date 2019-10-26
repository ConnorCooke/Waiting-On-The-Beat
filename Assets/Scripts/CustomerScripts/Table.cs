using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Table : MonoBehaviour
{
    public ObjectManager objectManager;

    private int tableID;
    public GameObject[] customers;
    private bool requestedCustomer;

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
            while(true)
            {
                if(index == 4)
                {
                    break;
                }
                if(customers[index] is null)
                {
                    RequestCustomer();
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
        float smallestDifference = 0;
        int nearestCustomer = -1;

        for (int index = 0; index < customers.Length; index++)
        {
            float diff = Math.Abs(customers[index].transform.position.x - playerx) + Math.Abs(customers[index].transform.position.y - playery);
            if (smallestDifference > diff)
            {
                smallestDifference = diff;
                nearestCustomer = index;
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

    private void CustomerReceivedFood(int index)
    {
        objectManager.CustomerEating(customers[index].transform.position);
    }

    /*
     * When a pay request is successful the customer is despawned and the tip amount is added
     * to the total tips, as well the tile that customer was on is no longer interactable
     */
    private void CustomerPaid(float tip, int index)
    {
        Vector3 pos = customers[index].transform.position;
        objectManager.CustomerPaid(tip, new Vector3(pos.x, pos.y, pos.z));
        //todo remove customer animation activations
        customers[index] = null;
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
                //TODO: customers[index].GetComponent<Customer>().BeatOccured();
            }
        }
    }

    public void ReceiveFood(GameObject food, Vector3 position)
    {
        // TODO:: customers[FindNearestCustomer(position)].GetComponent<Customer>().ReceiveFood(food);
    }

    public void ReceiveOrderRequest(Vector3 position)
    {
        // TODO:: customers[FindNearestCustomer(position)].GetComponent<Customer>().ReceiveOrderRequest();
    }

    public void ReceivePayRequest(Vector3 position)
    {
        // TODO:: customers[FindNearestCustomer(position)].GetComponent<Customer>().ReceivePayRequest();
    }
}

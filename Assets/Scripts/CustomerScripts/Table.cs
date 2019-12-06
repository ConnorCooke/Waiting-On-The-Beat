using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Table : MonoBehaviour
{
    public ObjectManager objectManager;

    protected int tableID;
    protected GameObject[] customers = new GameObject[4];
    protected bool requestedCustomer;
    protected GameObject[] customersFood = new GameObject[4];
    public GameObject[] visualsForCommunication;

    protected static Sprite[] drinkSprites = null;

    public int lowerBaseLayer;
    public int upperBaseLayer;
    public float middleX;
    public float middleY;


    // Start is called before the first frame update
    protected virtual void Start()
    {
        drinkSprites = Resources.LoadAll<Sprite>("Sprites/SetDressing/drinksnoanimation");
    }

    // Update is called once per frame
    protected virtual void Update()
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
    protected virtual int FindNearestCustomer(Vector3 position)
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

    public virtual void SetTableID(int id)
    {
        tableID = id;
    }

    public virtual int GetTableID()
    {
        return tableID;
    }

    protected virtual void RequestCustomer()
    {
        objectManager.RequestCustomer(GetTableID());
        requestedCustomer = true;
    }

    /*
     * Determines where no customer is seated, places the customer in that position of
     * the customers array and then changes its transform so it is seated in the correct 
     * position. When this is completed the table is able to request customers again
     */
    public virtual void ReceiveCustomer(GameObject customer)
    {
        bool notPlaced = true;
        int index = 0;
        while (notPlaced)
        {
            if(customers[index] is null)
            {
                customers[index] = customer;
                customer.GetComponent<CustomerObject>().SetIndex(index);
                StartCoroutine(SpawnAfterAnimation(index, customer));
                notPlaced = false;
            }
            index++;
        }
        requestedCustomer = false;
    }

    IEnumerator SpawnAfterAnimation(int index, GameObject customer)
    {
        visualsForCommunication[index].GetComponent<Animator>().SetTrigger("SpawnCustomer");
        ResetTriggersForIndex(index, "SpawnCustomer");
        yield return new WaitForSeconds((float).99);
        SetCustomerTransform(index, customer);
        CustomerReceived(index);
    }

    protected virtual void SetCustomerTransform(int idx, GameObject customer)
    {
        void SetTransform(float x, float y)
        {
            float tablex = middleX;
            float tabley = middleY;
            float tablez = this.transform.position.z;
            customers[idx].transform.position = new Vector3((tablex + x), (tabley + y), tablez);
            customers[idx].GetComponent<CustomerObject>().SetTable(this);
        }
        if (idx == 0)
        {
            SetTransform(-1, (float)1.5);
            customer.GetComponent<CustomerSpriteManager>().faceSouth(upperBaseLayer);
        }
        else if (idx == 1)
        {
            SetTransform(1, (float)1.5);
            customer.GetComponent<CustomerSpriteManager>().faceSouth(upperBaseLayer);
        }
        else if (idx == 2)
        {
            SetTransform(-1, (float)-.5);
            customer.GetComponent<CustomerSpriteManager>().faceNorth(lowerBaseLayer);
        }
        else
        {
            SetTransform(1, (float)-.5);
            customer.GetComponent<CustomerSpriteManager>().faceNorth(lowerBaseLayer);
        }
    }

    protected virtual void CustomerReceived(int index)
    {
        Vector3 pos = customers[index].transform.position;
        if (index > 1)
        {
            objectManager.CustomerReadyToOrder(new Vector3(pos.x, pos.y-1, pos.z));
        }
        else
        {
            objectManager.CustomerReadyToOrder(new Vector3(pos.x,pos.y,pos.z));
        }
        
    }

    public virtual void CustomerReceivedFood(int index, GameObject food)
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
        Vector3 pos = customers[index].transform.position;
        if (index > 1)
        {
            objectManager.CustomerEating(new Vector3(pos.x,pos.y-1, pos.z));
        }
        else
        {
            objectManager.CustomerEating(new Vector3(pos.x, pos.y, pos.z));
        }
        customersFood[index] = food;
        SetFoodTransform(index);
        visualsForCommunication[index].GetComponent<Animator>().SetBool("Waiting", false);
    }

    /*
     * When a pay request is successful the customer is despawned and the tip amount is added
     * to the total tips, as well the tile that customer was on is no longer interactable
     */
    public virtual void CustomerPaid(float tip, int index)
    {
        Vector3 pos = customers[index].transform.position;
        if(index > 1)
        {
            objectManager.CustomerPaid(tip, new Vector3(pos.x, pos.y-1, pos.z));
        }
        else
        {
            objectManager.CustomerPaid(tip, new Vector3(pos.x, pos.y, pos.z));
        }
       
        //todo remove customer animation activations
        DestroyImmediate(customers[index]);
        DestroyImmediate(customersFood[index]);
        customers[index] = null;
        customersFood[index] = null;
        visualsForCommunication[index].GetComponent<Animator>().SetTrigger("Paid");
        ResetTriggersForIndex(index, "Paid");
    }

    protected virtual IEnumerator ResetTriggersForIndex(int index, string name)
    {
        yield return new WaitForSeconds((float)0.25);
        Animator needsReset = visualsForCommunication[index].GetComponent<Animator>();
        needsReset.ResetTrigger(name);
    }

    /*
     * Whenever a beat occurs the table informs the customers that the beat occured
     */
    public virtual void BeatOccurred()
    {
        for(int index = 0; index < customers.Length; index++)
        {
            if(!(customers[index] is null))
            {
                customers[index].GetComponent<CustomerObject>().BeatOccured();
            }
        }
    }

    public virtual void ReceiveFood(GameObject food, Vector3 position)
    {
        customers[FindNearestCustomer(position)].GetComponent<CustomerObject>().ReceiveFood(food);
    }

    public virtual void ReceiveOrderRequest(Vector3 position)
    {
        int nearest = FindNearestCustomer(position);
        Vector3 pos = customers[nearest].transform.position;
        customers[nearest].GetComponent<CustomerObject>().ReceiveOrderRequest();
        if (nearest > 1)
        {
            objectManager.CustomerOrdered(new Vector3(pos.x, pos.y-1, pos.z));
        }
        else
        {
            objectManager.CustomerOrdered(new Vector3(pos.x, pos.y, pos.z));
        }
    }

    public virtual void ReceivePayRequest(Vector3 position)
    {
        customers[FindNearestCustomer(position)].GetComponent<CustomerObject>().ReceivePayRequest();
    }

    public virtual void GiveFoodOrder(FoodOrder order, int index){
        objectManager.GivePlayerOrder(order);
        visualsForCommunication[index].GetComponent<Animator>().SetTrigger("Ordering");
        visualsForCommunication[index].GetComponent<Animator>().SetInteger("Order", order.GetFoodName());
        visualsForCommunication[index].GetComponent<Animator>().SetBool("Waiting", true);
        ResetTriggersForIndex(index, "Ordering");
        StartCoroutine(ResetOrderAnimation(index));
    }

    protected virtual IEnumerator ResetOrderAnimation(int index)
    {
        yield return new WaitForSeconds((float) 3);
        visualsForCommunication[index].GetComponent<Animator>().SetInteger("Order", -1);
    }
}

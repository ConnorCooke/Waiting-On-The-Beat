using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CustomerQueueTracker : MonoBehaviour
{
    public ObjectManager objectManager;
    public GameObject customer;

    public String[] bodypaths;

    public int numberOfStartingCustomers;
    public int  diversityOfDrinks;

    protected Queue<GameObject> customerEntranceQueue = new Queue<GameObject>();
    protected Queue<int> customerRequestQueue = new Queue<int>();

    public int spawnTimer = 10;
    protected int timer;
    public int[] customerTimerValues;
    public int[] customerTipValues;
    public FoodOrder[] possibleOrders = new FoodOrder[] { new FoodOrder(0, 2, 20) };


    /*
    * This is for generating any new customers for the game
    */
    protected virtual void generateCustomer()
    {
        GameObject newCustomer = Instantiate(customer, new Vector3(15, 0, 0), Quaternion.identity);

        System.Random picker = new System.Random();

        CustomerObject customerObject = newCustomer.GetComponent<CustomerObject>();

        customerObject.SetFoodOrder(new FoodOrder(UnityEngine.Random.Range(0, diversityOfDrinks), 2, 20));
        customerObject.SetTimer(customerTimerValues[picker.Next(0, customerTimerValues.Length)]);
        customerObject.SetTipValue(customerTipValues[picker.Next(0, customerTipValues.Length)]);
        customerEntranceQueue.Enqueue(newCustomer);

        newCustomer.GetComponent<CustomerSpriteManager>().LoadSprites(
            UnityEngine.Random.Range(0, 6), // headshape
            UnityEngine.Random.Range(0, 6), // skintone
            UnityEngine.Random.Range(0, 6), // nose
            UnityEngine.Random.Range(0, 16), // accessory
            UnityEngine.Random.Range(0, 14), // hairtype
            UnityEngine.Random.Range(0, 8), // haircolour
            bodypaths[UnityEngine.Random.Range(0, bodypaths.Length)], //bodypath
            UnityEngine.Random.Range(0, 6)); // mouth
        
    }


    // Start is called before the first frame update
    protected virtual void Start()
    {
        timer = 0;
        for(int count = 0; count< numberOfStartingCustomers; count++)
        {
            generateCustomer();
        }
    }


    // Update is called once per frame
    protected virtual void Update()
    {
        //leave empty
    }


    /*
    * Receives customer request from a table
    */
    public virtual void ReceiveCustomerRequest(int tableNumber)
    {
        customerRequestQueue.Enqueue(tableNumber);
    }

    /*
    * Send customer to the table that requested one
    */
    public virtual void GiveCustomer(int tableNumber, GameObject customer)//NOT DONE
    {
        //attach customerObject to table
        objectManager.GiveCustomer(customer, tableNumber);
    }


    /*
    * Increments the timer for spawning customers and gives a customer to the earliest tables request,
    * occasionally changes the order of the requests to appear more random
    */
    public virtual void BeatOccured()
    {
        timer++;
        if(timer == spawnTimer)
        {          
            generateCustomer();
            timer = 0;
        }

        if(customerRequestQueue.Count > 0 && customerEntranceQueue.Count > 0)
        {
            if(UnityEngine.Random.Range(0.0f,1.0f) < .35)
            {
                customerRequestQueue.Enqueue(customerRequestQueue.Dequeue());
            }
            GiveCustomer(customerRequestQueue.Dequeue(), customerEntranceQueue.Dequeue());
        }
    }
}
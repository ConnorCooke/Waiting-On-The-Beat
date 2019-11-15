using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CustomerQueueTracker : MonoBehaviour
{
    public ObjectManager objectManager;
    public GameObject customer;

    public int numberOfStartingCustomers;

    private Queue<GameObject> customerEntranceQueue = new Queue<GameObject>();
    private Queue<int> customerRequestQueue = new Queue<int>();

    public int spawnTimer = 10;
    private int timer;
    public int[] customerTimerValues;
    public int[] customerTipValues;
    public FoodOrder[] possibleOrders = new FoodOrder[] { new FoodOrder("temp", 2, 20) };


    /*
    * This is for generating any new customers for the game
    */
    private void generateCustomer()
    {
        GameObject newCustomer = Instantiate(customer, new Vector3(15, 0, 0), Quaternion.identity);

        System.Random picker = new System.Random();

        CustomerObject customerObject = newCustomer.GetComponent<CustomerObject>();

        customerObject.SetFoodOrder(possibleOrders[picker.Next(0, possibleOrders.Length)]);
        customerObject.SetTimer(customerTimerValues[picker.Next(0, customerTimerValues.Length)]);
        customerObject.SetTipValue(customerTipValues[picker.Next(0, customerTipValues.Length)]);
        customerEntranceQueue.Enqueue(newCustomer);
        //customer.index
        
    }


    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        for(int count = 0; count< numberOfStartingCustomers; count++)
        {
            generateCustomer();
        }
    }


    // Update is called once per frame
    void Update()
    {
        //leave empty
    }


    /*
    * Receives customer request from a table
    */
    public void ReceiveCustomerRequest(int tableNumber)
    {
        customerRequestQueue.Enqueue(tableNumber);
    }

    /*
    * Send customer to the table that requested one
    */
    public void GiveCustomer(int tableNumber, GameObject customer)//NOT DONE
    {
        //attach customerObject to table
        objectManager.GiveCustomer(customer, tableNumber);
    }


    /*
    * Increments the timer for spawning customers and gives a customer to the earliest tables request,
    * occasionally changes the order of the requests to appear more random
    */
    public void BeatOccured()
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CustomerQueue : MonoBehaviour
{
    public CustomerObject customerObject;
    public ObjectManager objectManager;
    public GameObject customer;

    private Queue<customer> customerEntranceQueue = new Queue<customer>();

    public int spawnTimer = 10;
    private int timer;
    public int[] customerTimerValues;

    public FoodOrder[] possibleOrders;
    public int index;  
    public int impatienceLevel;
    public bool isEating;


    /*
    * This is for generating any new customers for the game
    */
    private generateCustomer();
    {
        Instantiate(customer, new Vector3(0, 15, 0));

        customer.impatienceLevel = 0;

        customer.isEating = false;

        customer.order = Random.Range(1.0f - 4.0f)

        //customer.index
    }


    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        generateCustomer()
    }


    // Update is called once per frame
    void Update()
    {
        //leave empty
    }


    /*
    * Receives customer request from a table
    */
    void ReceiveCustomerRequest(int tableNumber)//NOT DONE
    {
        //table number int requests a customer, then call GiveCustomer()
        GiveCustomer(tableNumber, customerEntranceQueue.Dequeue());
    }


    /*
    * Send customer to the table that requested one
    */
    void GiveCustomer(int tableNumber, GameObject customer)//NOT DONE
    {
        //attach customerObject to table

    }


    /*
    * This is called each time ObjectManager calls BeatOccured
    */
    void BeatOccured()
    {
        timer++;
        if(timer == spawnTimer)
        {          
            generateCustomer();
            timer =0;
        }
    }
}

// Spawn outside of screen -> (0, 15);
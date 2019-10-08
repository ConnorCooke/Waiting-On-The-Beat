using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjectManager : MonoBehaviour
{
    public GameObject[] Tables;
    public GameObject PlayerCharacter;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private int FindNearestTable()
    {
        float playerx = PlayerCharacter.transform.position.x;
        float playery = PlayerCharacter.transform.position.y;
        float smallestDifference = 0;
        int nearestTable = -1;
        for (int index = 0; index < Tables.Length; index++)
        {
            float diff = Math.Abs(Tables[index].transform.position.x - playerx) + Math.Abs(Tables[index].transform.position.y - playery);
            if (smallestDifference<diff)
            {
                smallestDifference = diff;
                nearestTable = index;
            }
        }
        return nearestTable;
    }

    /*
     * Determines the table closest to player and then tells the Table to give the
     * player the nearest customer to players order
     */
    void RequestOrder()
    {
        //TODO
    }

    /*
     * Determines the table closest to player and then tells the Table to determine
     * closest customer to player and have that customer pay for their food
     */
    void RequestPayment()
    {
        //TODO
    }

    /*
     * Determines the table closest to player and then tells the table to give
     * the food to the nearest customer to the player
     * @param food GameObject A food prefab that the player is currently holding
     */
    void DeliverFood(GameObject food)
    {
        //TODO
    }

    /*
     * Tells player to hold the food
     * @param food GameObject A food prefab player is picking up
     */
    void GivePlayerFood(GameObject food)
    {
        //TODO
    }

    /*
     * Tells the kitchen to take in the food order
     * @param order FoodOrder adt taht tells kitchen relevant info to "cook" a new food prefab
     */
    void DeliverOrderToKitchen(FoodOrder order)
    {
        //TODO
    }

    /*
     * Tells the player to carry order
     * @param order FoodOrder adt that holds info for "cooking" a new food prefab
     */
    void GivePlayerOrder(FoodOrder order)
    {
        //TODO
    }

    /*
     * Tells tiptracker the current multiplier based on the players combo
     * @param multiplier float
     */
    void GiveComboMultiplier(float multiplier)
    {
        //TODO
    }

    /*
     * Tells the result tracker the total amount of tips the player made
     * @param tipTotal float
     */
    void GiveTipTotal(float tipTotal)
    {
        //TODO
    }

    /*
     * Tells the CustomerQueueTracker the request for a customer
     */
    void RequestCustomer(int tableNumber)
    {
        //TODO
    }

    /*
     * Tells the table at index tableNumber to add the customer gameobject
     * to its array of customers
     * @param customer GameObject
     * @param tableNumber int
     */
    void GiveCustomer(GameObject customer, int tableNumber)
    {
        //TODO
    }

    /*
     * Tells combotracker the corretness of the last input
     */
    void GiveCorrectness(bool isCorrect)
    {
        //TODO
    }

    /*
     * Informs all relevant objects when a beat has occured, namely all objects
     * with beat based timers
     */
    void BeatOccured()
    {
        //TODO
    }
}

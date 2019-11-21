using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjectManager : MonoBehaviour
{
    public GameObject[] tables;
    public GameObject playerCharacter;
    public BeatRunner beatRunner;
    public BeatSpawner beatSpawner;
    public Kitchen kitchen;
    public CustomerQueueTracker customerQueueTracker;
    public ComboTracker comboTracker;
    public LaserManager laserManager;
    public TipCounter tipCounter;
    public BeatVisualizerCorrectnessDisplay visualizer;
    public EndTracker endTracker;
    private int[] playerPosition= {10, 5};

    // Start is called before the first frame update
    void Start()
    {
        for(int index = 0; index < tables.Length; index++)
        {
            tables[index].GetComponent<Table>().SetTableID(index);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private int FindNearestTable(int direction)
    {
        float playerx = playerCharacter.transform.position.x;
        float playery = playerCharacter.transform.position.y;
        float smallestDifference = 20000;
        int nearestTable = -1;
        int startPoint = 2;
        int endPoint = 4;

        if(direction == 0)
        {
            startPoint = 0;
            endPoint = 2;
        }

        for (int index = startPoint; index < endPoint; index++)
        {
            float diff = Math.Abs(tables[index].transform.position.x - playerx) + Math.Abs(tables[index].transform.position.y - playery);
            if (smallestDifference > diff)
            {
                smallestDifference = diff;
                nearestTable = index;
            }
        }
        return nearestTable;
    }

    /*
     * Determines the table closest to player and then tells the Table to give the
     * player the nearest customers order
     */
    public void RequestOrder(int direction)
    {
        tables[FindNearestTable(direction)].GetComponent<Table>().ReceiveOrderRequest(playerCharacter.transform.position);
    }

    /*
     * Determines the table closest to player and then tells the Table to determine
     * closest customer to player and have that customer pay for their food
     */
    public void RequestPayment(int direction)
    {
        tables[FindNearestTable(direction)].GetComponent<Table>().ReceivePayRequest(playerCharacter.transform.position);
    }

    /*
     * Determines the table closest to player and then tells the table to give
     * the food to the customer nearest to the player
     * @param food GameObject A food prefab that the player is currently holding
     */
    public void DeliverFood(GameObject food, int direction)
    {
        tables[FindNearestTable(direction)].GetComponent<Table>().ReceiveFood(food, playerCharacter.transform.position);
    }

    /*
     * Tells player to hold the food
     * @param food GameObject A food prefab player is picking up
     */
    public void GivePlayerFood(GameObject food)
    {
        playerCharacter.GetComponent<PlayerActionAndMovement>().ReceiveFood(food);
    }

    /*
     * Tells the kitchen to take in the food orders
     * @param order FoodOrder adt that tells kitchen relevant info to "cook" a new food prefab
     */
    public void DeliverOrdersToKitchen(List<FoodOrder> orders)
    {
        kitchen.ReceiveOrders(orders);
    }

    public void OrdersDelivered()
    {
        playerCharacter.GetComponent<PlayerActionAndMovement>().OrdersReceived();
    }

    /*
     * Tells the player that they are able to pick up the order from the customer at position
     */
    public void CustomerReadyToOrder(Vector3 position)
    {
        playerCharacter.GetComponent<PlayerActionAndMovement>().CustomerReadyToOrder(position);
    }

    public void CustomerOrdered(Vector3 position)
    {
        playerCharacter.GetComponent<PlayerActionAndMovement>().CustomerOrdered(position);
    }

    /*
    * Tells the player that the customer at position is eating, allowing payment requests to occur
    */
    public void CustomerEating(Vector3 position)
    {
        playerCharacter.GetComponent<PlayerActionAndMovement>().CustomerEating(position);
    }

    /*
    * Tells the player that the customer at position is eating and cannot be interacting
    */
    public void CustomerPaid(float tip, Vector3 position)
    {
        playerCharacter.GetComponent<PlayerActionAndMovement>().CustomerPaid(position);
        tipCounter.AddTip(tip);
    }

    /*
     * Tells the player to carry order
     * @param order FoodOrder adt that holds info for "cooking" a new food prefab
     */
    public void GivePlayerOrder(FoodOrder order)
    {
        playerCharacter.GetComponent<PlayerActionAndMovement>().ReceiveOrder(order);
        //TODO
    }

    /*
     * Tells tiptracker the current multiplier based on the players combo
     * @param multiplier float
     */
    public void GiveComboMultiplier(float multiplier)
    {
        tipCounter.UpdateTipMultiplier(multiplier);
    }

    public void UpdateVisualiserCorrectness(int level)
    {
        visualizer.UpdateCorrectnessLevel(level);
    }

    /*
     * Tells the result tracker the total amount of tips the player made, so that it can determine
     * the result of the players performance
     * @param tipTotal float
     */
    public void GiveTipTotal(float tipTotal)
    {
        //endtracker
        //TODO
    }

    /*
     * Tells the CustomerQueueTracker the request for a customer
     */
    public void RequestCustomer(int tableNumber)
    {
        customerQueueTracker.ReceiveCustomerRequest(tableNumber);
    }

    /*
     * Tells the kitchen to give the player the food from the nearest tile to the player
     */
    public void RequestFood()
    {
        kitchen.ReceiveFoodRequest(new Vector3(playerCharacter.transform.position.x, 
            playerCharacter.transform.position.y + (float)0.264, playerCharacter.transform.position.z));
    }

    /*
     * Tells the table at index tableNumber to add the customer gameobject
     * to its array of customers
     * @param customer GameObject
     * @param tableNumber int
     */
    public void GiveCustomer(GameObject customer, int tableNumber)
    {
        tables[tableNumber].GetComponent<Table>().ReceiveCustomer(customer);
    }

    /*
     * Tells combotracker the correctness of the last input
     */
    public void GiveCorrectness(bool isCorrect)
    {
        beatSpawner.GetComponent<BeatVisualizerCorrectnessDisplay>().Input(isCorrect);
        comboTracker.ReceiveCorrectness(isCorrect);
        //TODO
    }

    /*
     * Informs all relevant objects when a beat has occured, namely all objects
     * with beat based timers
     */
    public void BeatOccured()
    {
        foreach(GameObject table in tables)
        {
            table.GetComponent<Table>().BeatOccurred();
        }
        kitchen.BeatOccured();
        customerQueueTracker.BeatOccured();
        laserManager.BeatOccured();
        //TODO
    }

    public void UpdateTilePosition(int[] playerPos)
    {
        playerPosition = new int[] { playerPos[0], playerPos[1] };
        laserManager.UpdatePosition(playerPos);
    }

    /*
     * Cleans the nearest table to the player
     */
     public void CleanNearestTable()
    {
        //TODO
    }

    public void RemoveCash(int x, int y, string orientation, float removalAmount)
    {
        print("called for remove");
        if ((orientation == "vertical" && playerPosition[0] == x) || (orientation == "horizontal" && playerPosition[1] == y))
        {
            tipCounter.RemoveCash(removalAmount);
        }
    }

    /*
     * Spawns a beat visualisation
     */
    public void SpawnBeatVisual()
    {
        beatSpawner.SpawnBeat();
    }
}

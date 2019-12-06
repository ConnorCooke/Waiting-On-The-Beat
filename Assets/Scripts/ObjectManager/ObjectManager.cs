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
    public UIManager uiManager;
    protected int[] playerPosition= {10, 5};

    // Start is called before the first frame update
    protected virtual void Start()
    {
        for(int index = 0; index < tables.Length; index++)
        {
            tables[index].GetComponent<Table>().SetTableID(index);
        }
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    protected virtual int FindNearestTable(int direction)
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
    public virtual void RequestOrder(int direction)
    {
        tables[FindNearestTable(direction)].GetComponent<Table>().ReceiveOrderRequest(playerCharacter.transform.position);
    }

    /*
     * Determines the table closest to player and then tells the Table to determine
     * closest customer to player and have that customer pay for their food
     */
    public virtual void RequestPayment(int direction)
    {
        tables[FindNearestTable(direction)].GetComponent<Table>().ReceivePayRequest(playerCharacter.transform.position);
    }

    /*
     * Determines the table closest to player and then tells the table to give
     * the food to the customer nearest to the player
     * @param food GameObject A food prefab that the player is currently holding
     */
    public virtual void DeliverFood(GameObject food, int direction)
    {
        tables[FindNearestTable(direction)].GetComponent<Table>().ReceiveFood(food, playerCharacter.transform.position);
    }

    /*
     * Tells player to hold the food
     * @param food GameObject A food prefab player is picking up
     */
    public virtual void GivePlayerFood(GameObject food)
    {
        playerCharacter.GetComponent<PlayerActionAndMovement>().ReceiveFood(food);
    }

    /*
     * Tells the kitchen to take in the food orders
     * @param order FoodOrder adt that tells kitchen relevant info to "cook" a new food prefab
     */
    public virtual void DeliverOrdersToKitchen(List<FoodOrder> orders)
    {
        kitchen.ReceiveOrders(orders);
    }

    public virtual void OrdersDelivered()
    {
        playerCharacter.GetComponent<PlayerActionAndMovement>().OrdersReceived();
    }

    /*
     * Tells the player that they are able to pick up the order from the customer at position
     */
    public virtual void CustomerReadyToOrder(Vector3 position)
    {
        playerCharacter.GetComponent<PlayerActionAndMovement>().CustomerReadyToOrder(position);
    }

    public virtual void CustomerOrdered(Vector3 position)
    {
        playerCharacter.GetComponent<PlayerActionAndMovement>().CustomerOrdered(position);
    }

    /*
    * Tells the player that the customer at position is eating, allowing payment requests to occur
    */
    public virtual void CustomerEating(Vector3 position)
    {
        playerCharacter.GetComponent<PlayerActionAndMovement>().CustomerEating(position);
    }

    /*
    * Tells the player that the customer at position is eating and cannot be interacting
    */
    public virtual void CustomerPaid(float tip, Vector3 position)
    {
        playerCharacter.GetComponent<PlayerActionAndMovement>().CustomerPaid(position);
        tipCounter.AddTip(tip);
    }

    /*
     * Tells the player to carry order
     * @param order FoodOrder adt that holds info for "cooking" a new food prefab
     */
    public virtual void GivePlayerOrder(FoodOrder order)
    {
        playerCharacter.GetComponent<PlayerActionAndMovement>().ReceiveOrder(order);
        //TODO
    }

    /*
     * Tells tiptracker the current multiplier based on the players combo
     * @param multiplier float
     */
    public virtual void GiveComboMultiplier(float multiplier)
    {
        tipCounter.UpdateTipMultiplier(multiplier);
    }

    public virtual void UpdateVisualiserCorrectness(int level)
    {
        visualizer.UpdateCorrectnessLevel(level);
    }

    public virtual void EndLevel()
    {
        tipCounter.receiveScoreRequest();
    }

    /*
     * Tells the result tracker the total amount of tips the player made, so that it can determine
     * the result of the players performance
     * @param tipTotal float
     */
    public virtual void GiveTipTotal(float tipTotal)
    {
        uiManager.EndOfLevel(tipTotal);
    }

    /*
     * Tells the CustomerQueueTracker the request for a customer
     */
    public virtual void RequestCustomer(int tableNumber)
    {
        customerQueueTracker.ReceiveCustomerRequest(tableNumber);
    }

    /*
     * Tells the kitchen to give the player the food from the nearest tile to the player
     */
    public virtual void RequestFood()
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
    public virtual void GiveCustomer(GameObject customer, int tableNumber)
    {
        tables[tableNumber].GetComponent<Table>().ReceiveCustomer(customer);
    }

    /*
     * Tells combotracker the correctness of the last input
     */
    public virtual void GiveCorrectness(bool isCorrect)
    {
        beatSpawner.GetComponent<BeatVisualizerCorrectnessDisplay>().Input(isCorrect);
        comboTracker.ReceiveCorrectness(isCorrect);
        //TODO
    }

    /*
     * Informs all relevant objects when a beat has occured, namely all objects
     * with beat based timers
     */
    public virtual void BeatOccured()
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

    public virtual void UpdateTilePosition(int[] playerPos)
    {
        playerPosition = new int[] { playerPos[0], playerPos[1] };
        laserManager.UpdatePosition(playerPos);
    }

    /*
     * Cleans the nearest table to the player
     */
    public virtual void CleanNearestTable()
    {
        //TODO
    }

    public virtual void RemoveCash(int position, string orientation, float removalAmount)
    {
        if ((orientation == "vertical" && playerPosition[0] == position) || (orientation == "horizontal" && playerPosition[1] == position))
        {
            tipCounter.RemoveCash(removalAmount);
        }
    }

    /*
     * Spawns a beat visualisation
     */
    public virtual void SpawnBeatVisual()
    {
        beatSpawner.SpawnBeat();
    }
}

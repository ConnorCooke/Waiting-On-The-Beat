using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Kitchen : MonoBehaviour
{
    
    public Queue<GameObject> cookedFood = new Queue<GameObject>();
    public Queue<FoodOrder> uncookedFood = new Queue<FoodOrder>();

    public static int kitchenSize = 4; 
    public GameObject[] counterTop;

    public ObjectManager objectManager;
    public int kitchenTimer = 0;
    public GameObject foodPrefab;
    public GameObject foodBeingCooked;

    public void ReceiveOrders(List<FoodOrder> orders)
    {
        foreach (FoodOrder order in orders)
        {
            uncookedFood.Enqueue(order);
        }
        objectManager.OrdersDelivered();
    }
    
    /*
     * If there is an empty countertop position then the cooking bot places a cooked piece of food there
     * setting its rendering position and its transform accordingly
     */
    private void toKitchenCounter()
    {
        void SetTransform(int x)
        {
            counterTop[x].transform.position = new Vector3((float)-8.5, (float)4.5-x, 0);
            counterTop[x].GetComponent<SpriteRenderer>().sortingOrder = 61;
        }

        void AttemptToPlaceFoodAtIndex(int index)
        {
            if (cookedFood.Count > 0 && counterTop[index] is null)
            {
                counterTop[index] = cookedFood.Dequeue().gameObject;
                SetTransform(index);
            }
        }

        for(int x = 0; x < counterTop.Length; x++)
        {
            AttemptToPlaceFoodAtIndex(x);
        }

    }
    
    /*
     * Receives a request from the player for the food at a specific countertop position
     * and sends the food to the player
     */
    public void ReceiveFoodRequest(Vector3 playerPosition)
    {
        void CheckIndex(int index)
        {
            if (playerPosition.y < counterTop[index].transform.position.y + .1 && playerPosition.y > counterTop[index].transform.position.y-.1)
            {
                objectManager.GivePlayerFood(counterTop[index]);
                counterTop[index] = null;
                return;
            }
        }

        for(int x = 0; x < kitchenSize; x++)
        {
            if(!(counterTop[x] is null))
            {
                CheckIndex(x);
            }
            
        }
    }
        
    void Start()
    {
        foodBeingCooked = null;
        for(int index = 0; index < counterTop.Length; index++)
        {
            counterTop[index] = null;
        }
        
    }

    /*
     * Decrements timer if there is food currently being cooked, if the food has finished cooking it is
     * put in the queue for placement on the counter
     */
    public void BeatOccured()
    {
        if(kitchenTimer > 0)
        {
            kitchenTimer--;
            if(kitchenTimer == 0)
            {
                cookedFood.Enqueue(foodBeingCooked);
                foodBeingCooked = null;
            }
        }

        toKitchenCounter();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(foodBeingCooked is null && uncookedFood.Count > 0)
        {
            foodBeingCooked = Instantiate(foodPrefab, new Vector3(-17, 0, 0), Quaternion.identity);
            FoodOrder order = uncookedFood.Dequeue();
            foodBeingCooked.GetComponent<Food>().CreateFood(order);
            kitchenTimer = order.GetCookingTime();
        }
    }
    
}

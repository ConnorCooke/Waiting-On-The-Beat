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
    
    private void toKitchenCounter()
    {
        void SetTransform(int x)
        {
            counterTop[x].transform.position = new Vector3((float)-8.5, (float)4.5-x, 0);
        }

        void AttemptToPlaceFoodAtIndex(int index)
        {
            if (cookedFood.Count > 0 && counterTop[index] is null)
            {
                counterTop[index] = cookedFood.Dequeue().gameObject;
                Instantiate(counterTop[index]);
                SetTransform(index);
            }
        }

        for(int x = 0; x < counterTop.Length; x++)
        {
            AttemptToPlaceFoodAtIndex(x);
        }

    }
    

    public void ReceiveFoodRequest(Transform playerPosition)
    {
        void CheckIndex(int index)
        {
            if (playerPosition.position.y < counterTop[index].transform.position.y + .1 && playerPosition.position.y > counterTop[index].transform.position.y-.1)
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

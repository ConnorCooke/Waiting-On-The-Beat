using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Food : FoodOrder
{


    //string Name { get; set; }


    Sprite sprite;
    
    
    public Food(string n, int t)
    {
        FoodName = n;
        CookTimer = t;
        sprite = Resources.Load("Assets/Sprites/Foods" + FoodName + ".png", typeof(Sprite)) as Sprite;

        SpriteRenderer spriteR = GetComponent<SpriteRenderer>();

        spriteR.sprite = sprite;//Resources.Load("Assets/Sprites/Foods" + Name + ".png", typeof(Sprite)) as Sprite;
    }
    
    void BeatOccured()
    {
        
    }

    public void Spawn(int spot)
    {
        Instantiate(gameObject, new Vector3(1.46f, (5.49f - spot), 6.58f), Quaternion.identity);
    }
    void Start()
    {

        //Food F = new Food(FoodName, CookTimer);
        
        //Debug.Log(F.FoodName);
        /*
        if (BeingCooked(FoodOrder.foodTimer) == false) //if done bing cooked
        {
            cookedFood.Enqueue(this.gameObject); //put that shit in the queue of cooked stuff
            for (int x = 0; x < Kitchen.kitchenSize; x++)
            {
                if (Kitchen.counterTop[x] == null) //check if couner space is full so food cooked can be immediatly got'd
                {
                    Kitchen.counterTop[x] = cookedFood.Dequeue(); //gottem
                    FoodAvailable(counterTop[x], x); //spawn the food onto counter
                    break; //stop looking
                }
            }


        }
        */

    }

   

    // Update is called once per frame
    void Update()
    {
        
    }

   

   
}

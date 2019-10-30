using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Kitchen : MonoBehaviour
{
    
    public static Queue<Food> cookedFood = new Queue<Food>();
    public static Queue<FoodOrder> uncookedFood = new Queue<FoodOrder>();
    public static int kitchenSize = 6; //kitchen size
    public static GameObject[] counterTop = new GameObject[kitchenSize]; //countertop where cooked food it placed
    public ObjectManager objectmanager;
    public FoodOrder BeingCooked; //thing that be cookin
    public int kitchenTimer = 0;
   

    bool GrillOccupied()
    {
        if(kitchenTimer > 0) //something is cooking, therefore grill occupied
        {
            return true;
        }
        else //something finished cooking and timer == 0
        {
            return false;
        }
    }
  

    public void ReceiveOrders(List<FoodOrder> order)
    {
        for (int x = 0; x < order.Count; x++){ //put order into the toBeCooked queue
            uncookedFood.Enqueue(order[x]);
            Debug.Log("order received: \n" + uncookedFood.ToString());
            
        }
    } 
    

    public static void FoodAvailable(FoodOrder f, int spot)
    {
        //NOT WORKING DONT USE


        //Sprite sprite = Resources.Load("Assets/Sprites/Foods" + f.name + ".png", typeof(Sprite)) as Sprite;
       // SpriteRenderer spriteR = f.GetComponent<SpriteRenderer>();
        Debug.Log("sprite assigned");
        //Instantiate(f, new Vector3(1.46f, (5.49f-spot) , 6.58f), Quaternion.identity); //1.46 is on the table, 5.49 is top, 1.0 represents each tile(x)down from the top
        f.Spawn(spot);
        //Instantiate(f, new Vector3(1.46f, (5.49f - spot), 6.58f), Quaternion.identity);
        Debug.Log("sprite rendered");
        //cooking.transform.position = new Vector3(2, spot, 0);
        Debug.Log("sprite moved");
        //when food is made spawn it and move it down to where it is on array and 2 tiles over
    }
    // Start is called before the first frame update
    
   private void toKitchenCounter()
    {
        for(int x = 0; x < kitchenSize; x++)
        {
            if (cookedFood.Count > 0 && counterTop[x] == null) //if there are food to put out and space to put food
            {
                counterTop[x] = cookedFood.Dequeue().gameObject;
                Instantiate(counterTop[x]);
            }
        }

    }
    

    public void ReveiveFoodRequest(Transform playerPosition)
    {
            //give player food from the counter
            //remove food from kitchen
            //counterTop[?] = null
        for(int x = 0; x < kitchenSize; x++)
        {
            if (playerPosition.position.x == counterTop[x].transform.position.x) //if player touching tile with food
            {
                objectmanager.GivePlayerFood(counterTop[x]); //give the player that piece of food
                counterTop[x] = null;
            }
    }
        }
        
    void Start()
    {

        Debug.Log("if this gets called youre fucked");
        if (!GrillOccupied()){ //nothing being cooked
            Debug.Log("nothing on grill");

            if (BeingCooked != null)//food is cooked and needs to be removed from grill
            {
                cookedFood.Enqueue(BeingCooked.FoodObject);
                BeingCooked = null; //food removed from grill
            }
            if (uncookedFood.Count > 0) //if there is food to be cooked
            {
                BeingCooked = uncookedFood.Dequeue();
                kitchenTimer = BeingCooked.CookTimer; //reset the timer
            }
           
        }
        toKitchenCounter(); //clean out the cooked queeu to the counter
    }

    void BeatOccured()
    {
     
        kitchenTimer -= 1; //decrement cook timer
        Debug.Log("beat occured");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    

    
   
    
}

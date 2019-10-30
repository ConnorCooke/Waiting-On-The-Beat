using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Kitchen : MonoBehaviour
{
    

   
    //public GameObject Food;
    public List<FoodOrder> TestOrder = new List<FoodOrder>();
    
    
    public static Queue<GameObject> cookedFood = new Queue<GameObject>();
    public static Queue<FoodOrder> uncookedFood = new Queue<FoodOrder>();
    public static int kitchenSize = 6; //kitchen size
    public static GameObject[] counterTop = new GameObject[kitchenSize]; //countertop where cooked food it placed

    //public GameObject foodBeingCooked;
    //Food onGrill = new Food("Oil", 10);
    //Food onGrill = gameObject.AddComponent<Food>();
  





    public void ReceiveOrders(List<FoodOrder> order)
    {
        for (int x = 0; x < order.Count; x++){ //put order into the toBeCooked queue
            uncookedFood.Enqueue(order[x]);
            Debug.Log("order received: \n" + uncookedFood.ToString());
            
        }
    } 
    
    /*public void cookFood(bool occupied, FoodOrder food)
    {
        if(!(occupied)) //if there is nothing on the grill
        {
            onGrill = uncookedFood.Dequeue(); //put earliest order on the grill
            sprite = Resources.Load("Assets/Sprites/Foods" + onGrill.foodName + ".png", typeof(Sprite)) as Sprite;
            spriteR = gameObject.GetComponent<SpriteRenderer>();
        }
    }*/

    public static void FoodAvailable(Food f, int spot)
    {
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
    
   
    /*
    void test(Food g)
    {
        
        FoodOrder testFood = gameObject.AddComponent<FoodOrder>();
        FoodOrder testFood2 = gameObject.AddComponent<FoodOrder>();
        List<FoodOrder> testL = new List<FoodOrder>
        {
            testFood, testFood2
        };
        ReceiveOrders(testL);
        g = gameObject.AddComponent<Food>();
        g = new Food(uncookedFood.Dequeue());
    }
    */
    void Start()
    {


        //testing//

        Food onGrill = gameObject.AddComponent<Food>();
        onGrill.FoodName = "oil";
        onGrill.CookTimer = 10;
        Debug.Log(onGrill.GetComponents<SpriteRenderer>());

        //end testing
        FoodAvailable(onGrill, 2);

        if (onGrill == null && uncookedFood.Count > 0){//nothing being cooked yet and there is stuff to be cooked
            Debug.Log("nothing on grill");

           //put food on the grill
            
            
            Debug.Log("ongrill now has " + onGrill.FoodName);
            //used for testing cuz beat occured never happens
            //Sprite sprite = onGrill.GetComponent.spriteR; //Resources.Load("Assets/Sprites/Foods" + onGrill.FoodName + ".png", typeof(Sprite)) as Sprite;
            //SpriteRenderer spriteR = GetComponent<SpriteRenderer>(); // = onGrill.gameObject.GetComponent<SpriteRenderer>();
            //spriteR.sprite = onGrill.sprite;
            Debug.Log("sprite assigned");

            // Food f = new Food() {FoodName = onGrill.FoodName };
            //Food ff = (Food) onGrill;
            // f.FoodName = onGrill.FoodName;
            //f.gameObject;
            FoodAvailable(onGrill, 2);
            
            //Instantiate(onGrill, new Vector3(0, 0, 0), Quaternion.identity);
                                    //GameObject cooking = Instantiate(onGrill.gameObject);
            //end of testing that block
            Debug.Log("sprite rendered");
        }
        else if (onGrill.CookTimer == 0) //if done bing cooked
        {
            cookedFood.Enqueue(onGrill.gameObject); //put that shit in the queue of cooked stuff
            Debug.Log("food enqueued");
            for (int x = 0; x < Kitchen.kitchenSize; x++)
            {
                if (Kitchen.counterTop[x] == null) //check if couner space is full so food cooked can be immediatly got'd
                {
                    Kitchen.counterTop[x] = cookedFood.Dequeue(); //gottem
                    //FoodAvailable(counterTop[x], x); //spawn the food onto counter
                    break; //stop looking
                }
            }


        }

    }

    void BeatOccured()
    {
        
        //onGrill.CookTimer -= 1; //decrement cook timer
        Debug.Log("beat occured");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    

    
   
    
}

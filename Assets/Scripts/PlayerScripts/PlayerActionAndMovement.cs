using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionAndMovement : MonoBehaviour
{
    protected float moveSpeed = 2f;
    protected float gridSize = 1f;
    protected enum Orientation
    {
        Horizontal,
        Vertical
    };
    protected Vector2 input;
    protected bool isMoving = false;
    protected bool wasMoving = false;
    protected Vector3 startPosition;
    protected Vector3 endPosition;
    protected float t;
    protected float factor;
    protected bool validMovement;
    public int[] playerPosition;
    protected int[,] tileContents;
    protected int width = 17;
    protected int height = 11;

    protected GameObject currentFood;
    public GameObject handThatHoldsFood;
    protected List<FoodOrder> currentOrders;

    public GameObject[] orderMachines;

    public ObjectManager objectManager;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        playerPosition = new int[] {10, 5};
        tileContents =new int[,] { { 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                   { 0, 4, 0, 0, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 0, 0 },
                                   { 0, 4, 0, 0, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 0, 0 },
                                   { 0, 4, 0, 0, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 0, 0 },
                                   { 0, 4, 0, 0, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 0, 0 },
                                   { 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                   { 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 0, 0 },
                                   { 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 0, 0 },
                                   { 3, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 0, 0 },
                                   { 3, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 0, 0 },
                                   { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }};
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        /**
         * When the player is not already moving input can be taken in, it then deals with ambiguities/player trying to input both directions
         * simultaneously, checks if there is an interaction or movement involved with the input, and then starts a coroutine for movement
         */
        if(wasMoving && !isMoving){
            input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
            {
                input.y = 0;
            }
            else
            {
                input.x = 0;
            }
            if(input.y == 0 && input.x == 0)
            {
                wasMoving = false;
            }
        }

        if (!isMoving && !wasMoving)
        {
            validMovement = false;
            input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
            {
                input.y = 0;
            }
            else
            {
                input.x = 0;
            }

            CheckMovementInteractions();

            if (input != Vector2.zero && validMovement)
            {
                wasMoving = true;
                StartCoroutine(move(transform));
            }
        }
    }

    protected virtual void RequestFood()
    {
        objectManager.RequestFood();
    }

    protected virtual void DeliverFood(int direction)
    {
        if(!(currentFood is null))
        {
            objectManager.DeliverFood(currentFood, direction);
        }
        
    }

    protected virtual void RequestOrder(int direction)
    {
        objectManager.RequestOrder(direction);
    }

    protected virtual void DeliverOrders()
    {
        if (!(currentOrders is null))
        {
            objectManager.DeliverOrdersToKitchen(currentOrders);
            foreach(GameObject orderMachine in orderMachines)
            {
                orderMachine.GetComponent<Animator>().SetTrigger("OrdersPlaced");
            }
            
        }
        
    }
    protected virtual IEnumerator ResetOrderMachines()
    {
        yield return new WaitForSeconds((float)0.25);
        foreach (GameObject orderMachine in orderMachines)
        {
            orderMachine.GetComponent<Animator>().ResetTrigger("OrdersPlaced");
        }
    }
    /**
     * Calls objectmanager which tells the nearest table ot the player to clean itself
     */
    protected virtual void CleanTable()
    {
        objectManager.CleanNearestTable();
    }

    protected virtual void RequestPayment(int direction)
    {
        objectManager.RequestPayment(direction);
    }

    protected virtual int DetermineBaseLayer()
    {
        if(playerPosition[1] < 1)
        {
            return 5;
        }
        else if (playerPosition[1] < 5)
        {
            return 35;
        }
        else
        {
            return 65;
        }
    }

    /**
     * Determines if any interactions occur due to the players input, including allowing movement,  picking up orders, placing orders
     * picking up food, dropping off food, and clean tables. It sends the respective messages if neccessary
     * @return void
     */
    protected virtual void CheckMovementInteractions()
    {
        void DetermineTileInteractivity(int tileValue)
        {
            int direction = 0;
            if(input.y < 0 && playerPosition[1] == 5 || playerPosition[1] > 5)
            {
                direction = 1;
            }
            isMoving = true;
            wasMoving = true;
            switch (tileValue)
            {
                case 2:
                    RequestOrder(direction);
                    break;
                case 3:
                    DeliverOrders();
                    break;
                case 4:
                    if(currentFood is null)
                    {
                        RequestFood();
                    }
                    break;
                case 5:
                    DeliverFood(direction);
                    break;
                case 6:
                    CleanTable();
                    break;
                case 7:
                    RequestPayment(direction);
                    break;
            }
            StartCoroutine(WaitWhileInteracting());
        }

        void CheckEastwardInteractions()
        {
            if (input.x > 0 && playerPosition[0] + 1 < width)
            {
                int tileValue = tileContents[playerPosition[1], playerPosition[0] + 1];
                GetComponent<Animator>().SetInteger("Direction", 1);
                GetComponent<PlayerSpriteManager>().faceEast(DetermineBaseLayer());
                if (tileValue==0)
                {
                    validMovement = true;
                    playerPosition[0] += 1;
                    GetComponent<Animator>().SetBool("Walking", true);
                }
                else
                {
                    DetermineTileInteractivity(tileValue);
                }
            }
        }

        void CheckWestwardInteractions()
        {
            if (input.x < 0 && playerPosition[0] > 0)
            {
                int tileValue = tileContents[playerPosition[1], playerPosition[0] - 1];
                GetComponent<Animator>().SetInteger("Direction", 3);
                GetComponent<PlayerSpriteManager>().faceWest(DetermineBaseLayer());
                if (tileValue == 0)
                {
                    validMovement = true;
                    playerPosition[0] -= 1;
                    
                    GetComponent<Animator>().SetBool("Walking", true);
                }
                else
                {
                    DetermineTileInteractivity(tileValue);
                }
            }
        }
        
        void CheckNorthwardInteractions()
        {
            if (input.y > 0 && playerPosition[1] > 0 )
            {
                int tileValue = tileContents[playerPosition[1] - 1, playerPosition[0]];
                GetComponent<Animator>().SetInteger("Direction", 2);
                GetComponent<PlayerSpriteManager>().faceNorth(DetermineBaseLayer());
                if (tileValue == 0)
                {
                    validMovement = true;
                    playerPosition[1] -= 1;
                    GetComponent<Animator>().SetBool("Walking", true);
                }
                else
                {
                    DetermineTileInteractivity(tileValue);
                }
            }
        }
        
        void CheckSouthwardInteractions()
        {
            if (input.y < 0 && playerPosition[1] + 1 < height)
            {
                int tileValue = tileContents[playerPosition[1] + 1, playerPosition[0]];
                GetComponent<Animator>().SetInteger("Direction", 2);
                if(tileValue == 0)
                {
                    validMovement = true;
                    playerPosition[1] += 1;
                    GetComponent<Animator>().SetBool("Walking", true);
                }
                else
                {
                    DetermineTileInteractivity(tileValue);
                }
            }
        }

        CheckEastwardInteractions();
        CheckWestwardInteractions();
        CheckNorthwardInteractions();
        CheckSouthwardInteractions();
    }

    public virtual IEnumerator WaitWhileInteracting()
    {
        yield return new WaitForSeconds((float)0.25);
        GetComponent<Animator>().SetInteger("Direction", -1);
        GetComponent<PlayerSpriteManager>().faceSouth(DetermineBaseLayer());
        isMoving = false;
    }

    /**
     * Given the position to move the player character sprite, gradually increment the player sprite position until it arrives
     * at the given position. IEnumerator act over a time period without interrupting other game processes
     * @param transform the position the player sprite is moving to
     * @return nothing
     */
    public virtual IEnumerator move(Transform transform)
    {
        isMoving = true;
        startPosition = transform.position;
        t = 0;
        objectManager.UpdateTilePosition(playerPosition);
        
        endPosition = new Vector3(startPosition.x + System.Math.Sign(input.x) * gridSize, startPosition.y + System.Math.Sign(input.y) * gridSize, startPosition.z);
        factor = 1f;
        while (t < 1f)
        {
            t += Time.deltaTime * (moveSpeed / gridSize) * factor;
            transform.position = Vector3.Lerp(startPosition, endPosition, t);
            yield return null;
        }
        isMoving = false;
        GetComponent<Animator>().SetInteger("Direction", -1);
        GetComponent<Animator>().SetBool("Walking", false);
        GetComponent<PlayerSpriteManager>().faceSouth(DetermineBaseLayer());
        yield return 0;
    }

    /**
     * Given the transform of a specific object determines the correct tile and sets its value to tileValue
     */
    protected virtual void SetTileAtTransform(Vector3 TilePosition, int tileValue)
    {
        int xPosition = (int)((TilePosition.x - 0.5) + (float)10.0);
        int yPosition = (int)(-(TilePosition.y - 0.5) + (float)5.0);
        tileContents[yPosition, xPosition] = tileValue;
    }

    protected virtual void UpdateCustomerTiles(Vector3 customerPosition, int tileValue)
    {
        SetTileAtTransform(new Vector3( customerPosition.x - (float)0.5, customerPosition.y, customerPosition.z), tileValue);
        SetTileAtTransform(new Vector3( customerPosition.x + (float)0.5, customerPosition.y, customerPosition.z), tileValue);
    }

    public virtual void CustomerPaid(Vector3 position)
    {
        UpdateCustomerTiles(position, 1);
    }

    public virtual void CustomerReadyToOrder(Vector3 position)
    {
        UpdateCustomerTiles(position, 2);
    }

    public virtual void CustomerOrdered(Vector3 position)
    {
        UpdateCustomerTiles(position, 5);
    }

    public virtual void CustomerEating(Vector3 position)
    {
        UpdateCustomerTiles(position, 7);
        currentFood = null;
    }

    public virtual void ReceiveFood(GameObject food)
    {
        currentFood = food;
        currentFood.transform.SetParent(handThatHoldsFood.transform);
        currentFood.transform.position = new Vector3(handThatHoldsFood.transform.position.x, handThatHoldsFood.transform.position.y, 0);
    }

    public virtual void OrdersReceived()
    {
        currentOrders = null;
    }

    public virtual void ReceiveOrder(FoodOrder order)
    {
        if(currentOrders is null)
        {
            currentOrders = new List<FoodOrder>();
        }

        currentOrders.Add(order);
    }
    

}

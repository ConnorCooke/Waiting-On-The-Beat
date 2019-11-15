using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionAndMovement : MonoBehaviour
{
    private float moveSpeed = 2f;
    private float gridSize = 1f;
    private enum Orientation
    {
        Horizontal,
        Vertical
    };
    private Orientation gridOrientation = Orientation.Horizontal;
    private Vector2 input;
    private bool isMoving = false;
    private bool wasMoving = false;
    private Vector3 startPosition;
    private Vector3 endPosition;
    private float t;
    private float factor;
    private bool validMovement;
    public int[] playerPosition;
    private int[,] tileContents;
    private int width = 17;
    private int height = 11;

    private GameObject currentFood;
    private List<FoodOrder> currentOrders;

    public ObjectManager objectManager;

    // Start is called before the first frame update
    void Start()
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
                                   { 1, 3, 0, 0, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 0, 0 },
                                   { 1, 3, 0, 0, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 0, 0 },
                                   { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }};
    }

    // Update is called once per frame
    void Update()
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
        if(!(currentFood is null))
        {
            currentFood.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - 1);
        }
    }

    private void RequestFood()
    {
        objectManager.RequestFood();
    }

    private void DeliverFood(int direction)
    {
        if(!(currentFood is null))
        {
            objectManager.DeliverFood(currentFood, direction);
        }
        
    }

    private void RequestOrder(int direction)
    {
        objectManager.RequestOrder(direction);
    }

    private void DeliverOrders()
    {
        if (!(currentOrders is null))
        {
            objectManager.DeliverOrdersToKitchen(currentOrders);
        }
        
    }

    /**
     * Calls objectmanager which tells the nearest table ot the player to clean itself
     */
    private void CleanTable()
    {
        objectManager.CleanNearestTable();
    }

    private void RequestPayment(int direction)
    {
        objectManager.RequestPayment(direction);
    }

    private int DetermineBaseLayer()
    {
        if(playerPosition[1] < 1)
        {
            return 15;
        }
        else if (playerPosition[1] < 5)
        {
            return 35;
        }
        else
        {
            return 55;
        }
    }

    /**
     * Determines if any interactions occur due to the players input, including allowing movement,  picking up orders, placing orders
     * picking up food, dropping off food, and clean tables. It sends the respective messages if neccessary
     * @return void
     */
    private void CheckMovementInteractions()
    {
        void DetermineTileInteractivity(int tileValue)
        {
            int direction = 0;
            if(input.y < 0 || playerPosition[1] > 5)
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
                    RequestFood();
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

    public IEnumerator WaitWhileInteracting()
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
    public IEnumerator move(Transform transform)
    {
        isMoving = true;
        startPosition = transform.position;
        t = 0;

        
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
    private void SetTileAtTransform(Vector3 TilePosition, int tileValue)
    {
        int xPosition = (int)((TilePosition.x - 0.5) + (float)10.0);
        int yPosition = (int)(-(TilePosition.y - 0.5) + (float)5.0);
        tileContents[yPosition, xPosition] = tileValue;
    }

    private void UpdateCustomerTiles(Vector3 customerPosition, int tileValue)
    {
        SetTileAtTransform(new Vector3( customerPosition.x - (float)0.5, customerPosition.y, customerPosition.z), tileValue);
        SetTileAtTransform(new Vector3( customerPosition.x + (float)0.5, customerPosition.y, customerPosition.z), tileValue);
    }

    public void CustomerPaid(Vector3 position)
    {
        UpdateCustomerTiles(position, 1);
    }

    public void CustomerReadyToOrder(Vector3 position)
    {
        UpdateCustomerTiles(position, 2);
    }

    public void CustomerOrdered(Vector3 position)
    {
        UpdateCustomerTiles(position, 5);
    }

    public void CustomerEating(Vector3 position)
    {
        UpdateCustomerTiles(position, 7);
        currentFood = null;
    }
    
    public void ReceiveFood(GameObject food)
    {
        currentFood = food;
    }
    
    public void OrdersReceived()
    {
        currentOrders = null;
    }
    
    public void ReceiveOrder(FoodOrder order)
    {
        if(currentOrders is null)
        {
            currentOrders = new List<FoodOrder>();
        }

        currentOrders.Add(order);
    }
    

}

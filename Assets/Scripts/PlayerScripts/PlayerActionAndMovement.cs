using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionAndMovement : MonoBehaviour
{
    private float moveSpeed = 3f;
    private float gridSize = 1f;
    private enum Orientation
    {
        Horizontal,
        Vertical
    };
    private Orientation gridOrientation = Orientation.Horizontal;
    private Vector2 input;
    private bool isMoving = false;
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
                                   { 0, 1, 0, 0, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 0, 0 },
                                   { 0, 1, 0, 0, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 0, 0 },
                                   { 0, 1, 0, 0, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 0, 0 },
                                   { 0, 1, 0, 0, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 0, 0 },
                                   { 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                   { 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 0, 0 },
                                   { 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 0, 0 },
                                   { 1, 1, 0, 0, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 0, 0 },
                                   { 1, 1, 0, 0, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 0, 0 },
                                   { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }};
    }

    // Update is called once per frame
    void Update()
    {
        /**
         * When the player is not already moving input can be taken in, it then deals with ambiguities/player trying to input both directions
         * simultaneously, checks if there is an interaction or movement involved with the input, and then starts a coroutine for movement
         */
        if (!isMoving)
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
                StartCoroutine(move(transform));
            }
        }
    }

    private void RequestFood()
    {
        objectManager.RequestFood();
    }

    private void DeliverFood()
    {
        objectManager.DeliverFood(currentFood);
    }

    private void RequestOrder()
    {
        objectManager.RequestOrder();
    }

    private void DeliverOrders()
    {
        objectManager.DeliverOrdersToKitchen(currentOrders);
    }

    /**
     * Calls objectmanager which tells the nearest table ot the player to clean itself
     */
    private void CleanTable()
    {
        objectManager.CleanNearestTable();
    }

    private void RequestPayment()
    {
        objectManager.RequestPayment();
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
            switch (tileValue)
            {
                case 2:
                    print("request order");
                    RequestOrder();
                    break;
                case 3:
                    print("deliver order");
                    DeliverOrders();
                    break;
                case 4:
                    print("request food");
                    RequestFood();
                    break;
                case 5:
                    print("deliver food");
                    DeliverFood();
                    break;
                case 6:
                    print("clean table");
                    CleanTable();
                    break;
                case 7:
                    print("request payment");
                    RequestPayment();
                    break;
            }
        }

        void CheckEastwardInteractions()
        {
            if (input.x > 0 && playerPosition[0] + 1 < width && tileContents[playerPosition[1], playerPosition[0] + 1] == 0)
            {
                int tileValue = tileContents[playerPosition[1], playerPosition[0] + 1];
                if (tileValue==0)
                {
                    validMovement = true;
                    playerPosition[0] += 1;
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
                if (tileValue == 0)
                {
                    validMovement = true;
                    playerPosition[0] -= 1;
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
                if (tileValue == 0)
                {
                    validMovement = true;
                    playerPosition[1] -= 1;
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
                if(tileValue == 0)
                {
                    validMovement = true;
                    playerPosition[1] += 1;
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
        yield return 0;
    }

    /**
     * Given the transform of a specific object determines the correct tile and sets its value to tileValue
     */
    private void SetTileAtTransform(Vector3 TilePosition, int tileValue)
    {
        int xPosition = (int)(TilePosition.x / 0.5 + 10);
        int yPosition = (int)(-(TilePosition.y / 0.5) + 6);
        tileContents[yPosition, xPosition] = tileValue;
    }

    private void UpdateCustomerTiles(Vector3 customerPosition, int tileValue)
    {
        SetTileAtTransform(customerPosition + new Vector3((float) -0.5, 0, 0), tileValue);
        SetTileAtTransform(customerPosition + new Vector3((float) 0.5, 0, 0), tileValue);
    }

    public void CustomerPaid(Vector3 position)
    {
        UpdateCustomerTiles(position, 1);
    }

    public void CustomerReadyToOrder(Vector3 position)
    {
        UpdateCustomerTiles(position, 2);
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
    

    
    public void ReceiveOrder(FoodOrder order)
    {
        currentOrders.Add(order);
    }
    

}

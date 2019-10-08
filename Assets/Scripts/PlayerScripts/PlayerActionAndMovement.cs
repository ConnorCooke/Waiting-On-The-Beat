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

    // Start is called before the first frame update
    void Start()
    {
        playerPosition = new int[] {10,5 };
        tileContents =new int[,] { { 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                   { 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                   { 0, 1, 0, 0, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 0, 0 },
                                   { 0, 1, 0, 0, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 0, 0 },
                                   { 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                   { 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                   { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                   { 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 0, 0 },
                                   { 1, 1, 0, 0, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 0, 0 },
                                   { 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
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

    /**
     * Determines if any interactions occur due to the players input, including allowing movement,  picking up orders, placing orders
     * picking up food, dropping off food, and clean tables. It sends the respective messages if neccessary
     * @return void
     */
    private void CheckMovementInteractions()
    {
        void CheckEastwardInteractions()
        {
            if (input.x > 0 && playerPosition[0] + 1 < width && tileContents[playerPosition[1], playerPosition[0] + 1] == 0)
            {
                switch(tileContents[playerPosition[1], playerPosition[0] + 1])
                {
                    case 0:
                        validMovement = true;
                        playerPosition[0] += 1;
                        break;
                    /*case 2:
                        //TODO request food from kitchen counter
                    case 3:
                        //TODO give order to kitchen
                    case 4:
                        //TODO request order from customer
                    case 5:
                        //TODO give food to customer
                    case 6:
                        //TODO clean table space*/
                }
            }
        }

        void CheckWestwardInteractions()
        {
            if (input.x < 0 && playerPosition[0] > 0)
            {
                switch (tileContents[playerPosition[1], playerPosition[0] - 1])
                {
                    case 0:
                        validMovement = true;
                        playerPosition[0] -= 1;
                        break;
                    /*case 2:
                    //TODO request food from kitchen counter
                    case 3:
                    //TODO give order to kitchen
                    case 4:
                    //TODO request order from customer
                    case 5:
                    //TODO give food to customer
                    case 6:
                        //TODO clean table space*/
                }
            }

        }
        
        void CheckNorthwardInteractions()
        {
            if (input.y > 0 && playerPosition[1] > 0 )
            {
                switch (tileContents[playerPosition[1] - 1, playerPosition[0]])
                {
                    case 0:
                        validMovement = true;
                        playerPosition[1] -= 1;
                        break;
                        /*
                    case 2:
                    //TODO request food from kitchen counter
                    case 3:
                    //TODO give order to kitchen
                    case 4:
                    //TODO request order from customer
                    case 5:
                    //TODO give food to customer
                    case 6:
                        //TODO clean table space*/
                }
            }
        }
        
        void CheckSouthwardInteractions()
        {
            if (input.y < 0 && playerPosition[1] + 1 < height)
            {
                switch (tileContents[playerPosition[1] + 1, playerPosition[0]])
                {
                    case 0:
                        validMovement = true;
                        playerPosition[1] += 1;
                        break;
                        /*
                    case 2:
                    //TODO request food from kitchen counter
                    case 3:
                    //TODO give order to kitchen
                    case 4:
                    //TODO request order from customer
                    case 5:
                    //TODO give food to customer
                    case 6:
                    //TODO clean table space*/
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
    private void SetTileAtTransform(Transform TilePosition, int tileValue)
    {
        int xPosition = (int)(TilePosition.position.x / 0.5 + 10);
        int yPosition = (int)(-(TilePosition.position.y / 0.5) + 6);
        tileContents[yPosition, xPosition] = tileValue;
    }

    public void CustomerEating(Transform customerPosition)
    {
        SetTileAtTransform(customerPosition, 1);
    }
}

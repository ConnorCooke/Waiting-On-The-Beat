using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PAMTutoriallvl : PlayerActionAndMovement
{
    // Start is called before the first frame update
	
	
	protected override void Start(){
		//0 -> Movable
		//1 -> Not-Movable
		//3 -> OrderPlace
		//4 -> DrinkSpawn
		playerPosition = new int[]{4,2};
		width = 12;
	    height = 6;
		tileContents = new int[,]{
		{0,4,0,0,0,0,0,0,0,0,0,0},
		{0,4,0,0,0,1,1,1,1,0,0,0},
		{1,1,0,0,0,1,1,1,1,0,0,0},
		{0,0,0,0,0,1,1,1,1,0,0,0},
		{0,0,0,0,0,1,1,1,1,0,0,3},
		{0,0,0,0,0,0,0,0,0,0,0,3}};

	}

    protected override void CheckMovementInteractions()
    {
        void DetermineTileInteractivity(int tileValue)
        {
            int direction = 0;
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
                    if (currentFood is null)
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
                if (tileValue == 0)
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
            if (input.y > 0 && playerPosition[1] > 0)
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
                if (tileValue == 0)
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

    protected override void SetTileAtTransform(Vector3 TilePosition, int tileValue)
    {
        int xPosition = (int)((TilePosition.x - 0.5) + (float)9.0);
        int yPosition = (int)(-(TilePosition.y - 0.5) + (float)2.0);
        tileContents[yPosition, xPosition] = tileValue;
    }

    protected override void UpdateCustomerTiles(Vector3 customerPosition, int tileValue)
    {
        SetTileAtTransform(new Vector3(customerPosition.x - (float)0.5, customerPosition.y, customerPosition.z), tileValue);
        SetTileAtTransform(new Vector3(customerPosition.x + (float)0.5, customerPosition.y, customerPosition.z), tileValue);
    }

}

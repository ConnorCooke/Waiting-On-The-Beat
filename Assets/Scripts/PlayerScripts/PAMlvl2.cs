using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PAMlvl2 : PlayerActionAndMovement
{
    // Start is called before the first frame update
	
	
	protected override void Start(){
		//0 -> Movable
		//1 -> Not-Movable
		//3 -> OrderPlace
		//4 -> DrinkSpawn
		playerPosition = new int[]{3,2};
		width = 14;
	    height = 10;
		tileContents = new int[,]{
		{0,0,0,3,3,4,4,4,4,3,3,0,0,0,0,0},
		{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
		{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
		{0,0,0,0,0,1,1,1,1,0,0,0,0,0,0,0},
		{0,0,0,0,0,1,1,1,1,0,0,0,0,0,0,0},
		{0,0,0,0,0,1,1,1,1,0,0,0,0,0,0,0},
		{0,0,0,0,0,1,1,1,1,0,0,0,0,0,0,0},
		{0,0,0,0,0,1,1,1,1,0,0,0,0,0,0,0},
		{0,0,0,0,0,1,1,1,1,0,0,0,0,0,0,0},
		{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
		{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}};

	}

	protected override void SetTileAtTransform(Vector3 TilePosition, int tileValue)
    {
        int xPosition = (int)((TilePosition.x - 0.5) + (float)8.0);
        int yPosition = (int)(-(TilePosition.y - 0.5) + (float)4.0);
        tileContents[yPosition, xPosition] = tileValue;
    }
	
	protected override void UpdateCustomerTiles(Vector3 customerPosition, int tileValue)
    {
        SetTileAtTransform(new Vector3( customerPosition.x, customerPosition.y - (float)0.5, customerPosition.z), tileValue);
        SetTileAtTransform(new Vector3( customerPosition.x, customerPosition.y + (float)0.5, customerPosition.z), tileValue);
    }

   
}

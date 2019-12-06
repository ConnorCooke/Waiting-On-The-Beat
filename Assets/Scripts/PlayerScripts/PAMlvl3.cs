using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PAMlvl3 : PlayerActionAndMovement
{
    // Start is called before the first frame update
	protected int width = 14;
	protected int height = 10;
	
	
	protected override void Start(){
		//0 -> Movable
		//1 -> Not-Movable
		//3 -> OrderPlace
		//4 -> DrinkSpawn
		playerPosition = new int[]{5,4};
		tileContents =new int[,]
        { { 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0 },
          { 0, 0, 1, 1, 4, 4, 4, 4, 4, 4, 1, 1, 0, 0 },
          { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
          { 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0 },
          { 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0 },
          { 0, 1, 1, 1, 0, 0, 3, 3, 0, 0, 1, 1, 1, 0 },
          { 0, 1, 1, 1, 0, 0, 3, 3, 0, 0, 1, 1, 1, 0 },
          { 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0 },
          { 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0 },
          { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }};

	}
	
	protected override void UpdateCustomerTiles(Vector3 customerPosition, int tileValue)
    {
        SetTileAtTransform(new Vector3( customerPosition.x, customerPosition.y - (float)0.5, customerPosition.z), tileValue);
        SetTileAtTransform(new Vector3( customerPosition.x, customerPosition.y + (float)0.5, customerPosition.z), tileValue);
    }

   
}
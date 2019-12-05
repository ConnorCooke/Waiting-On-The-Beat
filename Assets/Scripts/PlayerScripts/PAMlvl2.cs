using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PAMlvl2 : PlayerActionAndMovement
{
    // Start is called before the first frame update
	protected int width = 14;
	protected int height = 10;
	
	
	protected override void Start(){
		
		playerPosition = new int[]{3,2};
		tileContents = new int[,]{{0,0,0,3,3,4,4,4,4,3,3,0,0,0},{0,0,0,0,0,0,0,0,0,0,0,0,0,0},
		{0,0,0,0,0,1,1,1,1,0,0,0,0,0},
		{0,0,0,0,0,0,1,1,0,0,0,0,0,0},
		{0,0,0,0,0,1,1,1,1,0,0,0,0,0},
		{0,0,0,0,0,0,1,1,0,0,0,0,0,0},
		{0,0,0,0,0,1,1,1,1,0,0,0,0,0},
		{0,0,0,0,0,0,1,1,0,0,0,0,0,0},
		{0,0,0,0,0,1,1,1,1,0,0,0,0,0},
		{0,0,0,0,0,0,0,0,0,0,0,0,0,0}};

	}
	


   
}

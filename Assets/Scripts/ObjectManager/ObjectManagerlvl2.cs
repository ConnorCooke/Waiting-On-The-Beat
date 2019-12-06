using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManagerlvl2 : ObjectManager
{
    // Start is called before the first frame update
    
	protected GameObject[] tables;
	protected int[] playerPosition= {3, 2};

	protected override int FindNearestTable(int direction){
		return 0;
	}



}

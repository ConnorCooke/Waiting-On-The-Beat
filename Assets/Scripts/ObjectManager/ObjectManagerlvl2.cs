using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManagerlvl2 : ObjectManager
{
    // Start is called before the first frame update
    protected override void Start()
    {
        playerPosition = new int[]{3, 2};
        for (int index = 0; index < tables.Length; index++)
        {
            tables[index].GetComponent<Table>().SetTableID(index);
        }
    }

	protected override int FindNearestTable(int direction){
		return 0;
	}



}

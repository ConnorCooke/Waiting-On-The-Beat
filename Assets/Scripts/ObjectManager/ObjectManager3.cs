using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager3 : ObjectManager
{

	protected override void Start(){
        base.Start();
        playerPosition = new int[] {3, 4};
    }

    protected override int FindNearestTable(int direction)
    {
        return direction;
    }

}

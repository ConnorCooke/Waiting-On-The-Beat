using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManagerTutorial : ObjectManager
{
    public UIManagerTutorial uiManagerTutorial;
    protected int numberOfCalls = 0;

    protected override int FindNearestTable(int direction)
    {
        return 0;
    }

    public override void CustomerPaid(float tip, Vector3 position)
    {
        base.CustomerPaid(tip, position);
        uiManagerTutorial.NextTutorialSection();
        numberOfCalls++;
    }

    public override void CustomerReadyToOrder(Vector3 position)
    {
        base.CustomerReadyToOrder(position);
        uiManagerTutorial.NextTutorialSection();
    }

    public override void CustomerOrdered(Vector3 position)
    {
        base.CustomerOrdered(position);
        uiManagerTutorial.NextTutorialSection();
    }

    public override void RemoveCash(int position, string orientation, float removalAmount)
    {
        base.RemoveCash(position, orientation, removalAmount);
        uiManagerTutorial.NextTutorialSection();
    }
}

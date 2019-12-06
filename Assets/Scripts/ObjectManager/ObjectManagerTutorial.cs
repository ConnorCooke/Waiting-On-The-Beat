using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManagerTutorial : ObjectManager
{
    public UIManagerTutorial uiManagerTutorial;

    protected override int FindNearestTable(int direction)
    {
        return 0;
    }

    public override void CustomerPaid(float tip, Vector3 position)
    {
        base.CustomerPaid(tip, position);
        uiManagerTutorial.NextTutorialSection();
    }

    public override void CustomerReadyToOrder(Vector3 position)
    {
        base.CustomerReadyToOrder(position);
        uiManagerTutorial.NextTutorialSection();
    }

    public virtual void CustomerOrdered(Vector3 position)
    {
        base.CustomerOrdered(position);
        uiManagerTutorial.NextTutorialSection();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManagerTutorial : ObjectManager
{
    public UIManagerTutorial uiManagerTutorial;

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
}

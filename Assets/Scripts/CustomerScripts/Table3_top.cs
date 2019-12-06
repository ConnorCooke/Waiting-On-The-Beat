using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table3_top : Table
{
    protected override void Start()
    {
        drinkSprites = Resources.LoadAll<Sprite>("Sprites/SetDressing/drinksnoanimation");
        customers = new GameObject[4];
        customersFood = new GameObject[4];
    }

    protected override void Update()
    {
        if (!requestedCustomer)
        {
            int index = 0;
            while (index < 4)
            {
                if (customers[index] is null)
                {
                    RequestCustomer();
                    index = 4;
                }
                index++;
            }
        }
    }
    protected override void CustomerReceived(int index)
    {
        Vector3 pos = customers[index].transform.position;

        objectManager.CustomerReadyToOrder(new Vector3(pos.x, pos.y, pos.z));

    }

    // Start is called before the first frame update
    protected override void SetCustomerTransform(int idx, GameObject customer)
    {
        void SetTransform(float x, float y)
        {
            float tablex = middleX;
            float tabley = middleY;
            float tablez = this.transform.position.z;
            customers[idx].transform.position = new Vector3((tablex + x), (tabley + y), tablez);
            customers[idx].GetComponent<CustomerObject>().SetTable(this);
        }
        if (idx == 0)
        {
            SetTransform(-3.5f, -1f);
            customer.GetComponent<CustomerSpriteManager>().faceNorth(upperBaseLayer);
        }
        else if (idx == 1)
        {
            SetTransform(-1.5f, -1f);
            customer.GetComponent<CustomerSpriteManager>().faceNorth(upperBaseLayer);
        }
        else if (idx == 2)
        {
            SetTransform(0.5f, -1f);
            customer.GetComponent<CustomerSpriteManager>().faceNorth(lowerBaseLayer);
        }
        else if (idx == 3)
        {
            SetTransform(2.5f, -1f);
            customer.GetComponent<CustomerSpriteManager>().faceNorth(lowerBaseLayer);
        }
	}
        
}

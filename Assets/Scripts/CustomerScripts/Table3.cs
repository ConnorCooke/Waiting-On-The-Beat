using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table3 : Table
{
    protected override void Start()
    {
        drinkSprites = Resources.LoadAll<Sprite>("Sprites/SetDressing/drinksnoanimation");
        customers = new GameObject[6];
        customersFood = new GameObject[6];
    }

    protected override void Update()
    {
        if (!requestedCustomer)
        {
            int index = 0;
            while (index < 8)
            {
                if (customers[index] is null)
                {
                    RequestCustomer();
                    index = 8;
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
            SetTransform(-3.5f, 3.5f);
            customer.GetComponent<CustomerSpriteManager>().faceNorth(upperBaseLayer);
        }
        else if (idx == 1)
        {
            SetTransform(-1.5f, 3.5f);
            customer.GetComponent<CustomerSpriteManager>().faceNorth(upperBaseLayer);
        }
        else if (idx == 2)
        {
            SetTransform(0.5f, 3.5f);
            customer.GetComponent<CustomerSpriteManager>().faceNorth(lowerBaseLayer);
        }
        else if (idx == 3)
        {
            SetTransform(2.5f, 3.5f);
            customer.GetComponent<CustomerSpriteManager>().faceNorth(lowerBaseLayer);
        }
        else if (idx == 4)
        {
            SetTransform(-3.5f, -3.5f);
            customer.GetComponent<CustomerSpriteManager>().faceSouth(lowerBaseLayer);
        }
        else if(idx ==5)
        {
            SetTransform(-1.5f, (float)-3.5f);
            customer.GetComponent<CustomerSpriteManager>().faceSouth(lowerBaseLayer);
        }
        else if(idx == 6)
        {
            SetTransform(0.5f, (float)-3.5f);
            customer.GetComponent<CustomerSpriteManager>().faceSouth(lowerBaseLayer);
        }
        else
        {
            SetTransform(2.5f, (float)-3.5f);
            customer.GetComponent<CustomerSpriteManager>().faceSouth(lowerBaseLayer);
        }
    }
}

using UnityEngine;

public class Table2 : Table
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
            while(index < 6)
            {
                if(customers[index] is null)
                {
                    RequestCustomer();
                    index = 6;
                }
                index++;
            }
        }
    }

	protected override void CustomerReceived(int index)
    {
        Vector3 pos = customers[index].transform.position;
        
        objectManager.CustomerReadyToOrder(new Vector3(pos.x,pos.y,pos.z));
        
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
            SetTransform(-2, (float)2);
            customer.GetComponent<CustomerSpriteManager>().faceEast(upperBaseLayer);
        }
        else if (idx == 1)
        {
            SetTransform(-2, (float)0);
            customer.GetComponent<CustomerSpriteManager>().faceEast(upperBaseLayer);
        }
        else if (idx == 2)
        {
            SetTransform(-2, (float)-2);
            customer.GetComponent<CustomerSpriteManager>().faceEast(lowerBaseLayer);
        }
        else if (idx == 3)
        {
            SetTransform(1, (float)2);
            customer.GetComponent<CustomerSpriteManager>().faceEast(lowerBaseLayer);
        }
        else if (idx == 4)
        {
            SetTransform(1, (float)0);
            customer.GetComponent<CustomerSpriteManager>().faceEast(lowerBaseLayer);
        }
        else
        {
            SetTransform(1, (float)-2);
            customer.GetComponent<CustomerSpriteManager>().faceWest(lowerBaseLayer);
        }
    }
}

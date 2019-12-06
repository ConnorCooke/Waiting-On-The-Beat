using UnityEngine;

public class Table2 : Table
{
    new protected GameObject[] customers = new GameObject[6];
    new protected GameObject[] customersFood = new GameObject[6];

    // Start is called before the first frame update


    override protected void Update()
    {
        if (!requestedCustomer)
        {
            int index = 0;
            while (index < 10)
            {
                if (customers[index] is null)
                {
                    RequestCustomer();
                    index = 10;
                }
                index++;
            }
        }
    }
    override protected void SetCustomerTransform(int idx, GameObject customer)
    {
        Debug.Log("herwohwrhioweh");
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
            SetTransform(-2, (float)1);
            customer.GetComponent<CustomerSpriteManager>().faceEast(upperBaseLayer);
        }
        else if (idx == 1)
        {
            SetTransform(-2, (float)0);
            customer.GetComponent<CustomerSpriteManager>().faceEast(upperBaseLayer);
        }
        else if (idx == 2)
        {
            SetTransform(-2, (float)-1);
            customer.GetComponent<CustomerSpriteManager>().faceEast(lowerBaseLayer);
        }
        else if (idx == 3)
        {
            SetTransform(1, (float)1);
            customer.GetComponent<CustomerSpriteManager>().faceEast(lowerBaseLayer);
        }
        else if (idx == 4)
        {
            SetTransform(1, (float)0);
            customer.GetComponent<CustomerSpriteManager>().faceEast(lowerBaseLayer);
        }
        else
        {
            SetTransform(1, (float)-1);
            customer.GetComponent<CustomerSpriteManager>().faceWest(lowerBaseLayer);
        }
    }
}

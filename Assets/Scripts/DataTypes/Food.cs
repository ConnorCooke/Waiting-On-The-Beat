using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : FoodOrder
{
    // Start is called before the first frame update
    string Name { get => FoodName; set => Name = FoodName; }

    SpriteRenderer Sprite;// = Resources.Load("Assets/Sprites/foods" + name + ".png") as Sprite;
    
    public Food()
    {

        void Awake()
        {
        Sprite.sprite = Resources.Load("Assets/Sprites/foods" + Name + ".png") as Sprite;
        }
    }

    public void BeatOccured()
    {
        CookTimer -= 1;
    }
}

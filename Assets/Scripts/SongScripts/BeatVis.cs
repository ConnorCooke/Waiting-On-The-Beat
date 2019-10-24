using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatVis : MonoBehaviour
{
    //This will be determined by bpm
    [SerializeField]
    float speed;
    [SerializeField]
    GameObject visualizer;


    [SerializeField]
    Color changedColor;

    SpriteRenderer sprite;
    float timer;

    
    
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        timer = Time.deltaTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position.x > visualizer.transform.position.x)
        {
            transform.position = new Vector2(transform.position.x - speed, transform.position.y);
        }
        else
        {
            transform.position = new Vector2(transform.position.x + speed, transform.position.y);

        }
        
        sprite.color = new Color(sprite.color.r,sprite.color.g,sprite.color.b,sprite.color.a+0.005f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DestructiveCollider"))
        {
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Plate"))
        {
            sprite.color = changedColor;
        }
    }

    
}

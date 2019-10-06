using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Great By mysz
Date20191005
*/
///<summary>
///
///</summary>
public class Thron : MonoBehaviour
{
    public ThronDirection direction = ThronDirection.Up;
    public float speed = 0.01f;
    Vector2 position1;
    Vector2 position2;
    Vector2 dir;
    Vector2 dest;

    public bool isMovingThron = false;//是否是飞行的尖刺
    private void Start()
    {
        if (direction == ThronDirection.Up)
            dir = new Vector2(0, 1);
        else if (direction == ThronDirection.Down)
            dir = new Vector2(0, -1);
        else if (direction == ThronDirection.Left)
            dir = new Vector2(-1, 0);
        else if (direction == ThronDirection.Right)
            dir = new Vector2(1,0);
        position1 = this.transform.position;
        position2 = position1 + dir;
        dest = position2;
            
    }
    private void FixedUpdate()
    {
        Vector2 temp = Vector2.MoveTowards(transform.position, dest, speed);
        GetComponent<Rigidbody2D>().MovePosition(temp);
        if(isMovingThron)
        {
            dest += dir;
        }
        else
        {
            if ((Vector2)transform.position == position2)
                dest = position1;
            if ((Vector2)transform.position == position1)
                dest = position2;
        }
        
            
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "SnakeBody")
        {
            Destroy(collision.gameObject);
        }
        if(isMovingThron&&collision.tag == "Wall")
        {
            transform.position = position1;
        }
    }
}

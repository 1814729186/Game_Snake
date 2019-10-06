using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Great By mysz
Date:20191005
*/
///<summary>
///
///</summary>
public class Monster : MonoBehaviour
{
    public ThronDirection direction = ThronDirection.Up;//确定Monster运动的初始方向
    public float speed = 0.01f;
    Vector2 dir;
    Vector2 dest;
    public GameObject snake;
    private bool triggerIsWall = false;//用于记录是否到达墙的位置
    private void Start()
    {
        if (direction == ThronDirection.Up)
            dir = new Vector2(0, 1);
        else if (direction == ThronDirection.Down)
            dir = new Vector2(0, -1);
        else if (direction == ThronDirection.Left)
            dir = new Vector2(-1, 0);
        else if (direction == ThronDirection.Right)
            dir = new Vector2(1, 0);
        
        dest = (Vector2)this.transform.position + dir;
    }
    private void FixedUpdate()
    {
        Vector2 temp = Vector2.MoveTowards(transform.position, dest, speed);
        GetComponent<Rigidbody2D>().MovePosition(temp);
        if((Vector2)transform.position == dest||triggerIsWall)
        {
            triggerIsWall = false;
            dest = (Vector2)transform.position + dir;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "SnakeBody"||collision.tag == "SnakeHead")
        {
            snake.GetComponent<PalyerSnake02>().GameOver();
        }
        else if(collision.tag == "Wall")
        {
            triggerIsWall = true;
            
            dir = new Vector2(-dir.x,-dir.y);
        }
    }
}

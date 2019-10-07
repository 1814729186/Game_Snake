using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
Great By mysz
Date:20191006
*/
///<summary>
///
///</summary>
public class SnakePlayer4 : MonoBehaviour
{
    public GameObject camera;
    Vector3 cameraToSnake = new Vector3(0, 0, -10);//创建一个向量，用来定义camera与Snake之间的距离，放在这里避免Update过程中的重复创建
    float angle;
    public float speed = 20.0f;
    Vector3 pos;
    Vector3 chaPos;
    public float distanceCanMove = 2.0f;//鼠标与snakeHead之间达到一定距离才可以修改移动的方向
    public List<Transform> bodyList = new List<Transform>();//身体
    public List<Vector3> bodyListLastTransform = new List<Vector3>();
    public GameObject snakeBody;
    public Sprite[] snakeHeadSprites = new Sprite[4];
    public Sprite[] snakeBodySprites = new Sprite[4];
    public float xSpeed = 12f;//设置横向的速度

    private  int snakeSkin = 0;//设置蛇的皮肤(默认初始为red)
    public Text scoreText;
    private int score;
    public Text gameOverText;

    private void ChangeSkin(int num)//
    {
        //设置snake的皮肤
        this.GetComponent<SpriteRenderer>().sprite = snakeHeadSprites[num];
        snakeBody.GetComponent<SpriteRenderer>().sprite = snakeBodySprites[num];
        for(int i = 0;i < bodyList.Count;i++)//设置已经生成的蛇身的皮肤
        {
            bodyList[i].GetComponent<SpriteRenderer>().sprite = snakeBodySprites[num];
        }
    }
    private void Start()
    {
        //设置snake的皮肤
        this.GetComponent<SpriteRenderer>().sprite = snakeHeadSprites[snakeSkin];
        snakeBody.GetComponent<SpriteRenderer>().sprite = snakeBodySprites[snakeSkin];
        chaPos = new Vector3(0, 0, 0);
        InvokeRepeating("Move", 0, 0.01f);
        for (int i = 0; i < 50; i++)//设置初始长度为50
            Grow();
    }
    private void Grow()
    {
        Transform body = Instantiate(snakeBody).transform;//生成一节snakebody,将其位置放到最末尾
        if (bodyList.Count == 0)
            body.GetComponent<Transform>().position = transform.position;
        else
            body.GetComponent<Transform>().position = bodyList[bodyList.Count - 1].position;
        //body.SetParent(this.transform,true);
        bodyList.Add(body);//将生成的body添加到List当中
    }
    private void FixedUpdate()
    {
        score = (int)transform.position.x / 10;
        scoreText.GetComponent<Text>().text = score.ToString();

        pos = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
        if ((pos - transform.position).magnitude >= distanceCanMove)//鼠标与snakehead之间的距离达到一定值才可以更改运动的方向
        {
            chaPos = pos - transform.position;
        }
        transform.position = transform.position + new Vector3(xSpeed, chaPos.y).normalized * speed * Time.deltaTime;
        camera.GetComponent<Transform>().position = new Vector3(transform.position.x + 10, 0, 0) + cameraToSnake;

    }

    private void Move()
    {
        //通过速度设置进行移动
        if (bodyList.Count > 0)
            for (int i = 0; i < bodyListLastTransform.Count; i++)
            {
                Vector2 temp = (bodyListLastTransform[i] - bodyList[i].position).normalized;
                bodyList[i].GetComponent<Rigidbody2D>().velocity = temp * speed;
            }
        //为下一次移动做准备
        bodyListLastTransform = new List<Vector3>();//重新生成一个列表，记录位置信息
        //记录snakeHead和body的位置信息,把位置信息填入列表中
        Vector3 headVec = new Vector3(0, 0, 0);
        headVec.x = transform.position.x;
        headVec.y = transform.position.y;
        bodyListLastTransform.Add(headVec);
        for (int i = 0; i < bodyList.Count - 1; i++)
        {
            Vector3 vector3 = new Vector3(0, 0, 0);
            vector3.x = bodyList[i].position.x;
            vector3.y = bodyList[i].position.y;
            bodyListLastTransform.Add(vector3);
        }
    }


    //以下是游戏逻辑实现
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Line")
        {
            snakeSkin = (int)collision.GetComponent<Line>().curColor;
            ChangeSkin(snakeSkin);
        }
        else if(collision.tag == "LineBox")
        {
            if (collision.GetComponent<LineBox>().curColor != snakeSkin)
                GameOver();
        }
        else if(collision.tag == "RotateBox")
        {
            if (collision.GetComponent<RotateBox>().curColor != snakeSkin)
                GameOver();
        }
    }
    public void ResetTimeScal()
    {
        Time.timeScale = 1;
    }
    public void GameOver()
    {
        Destroy(this);
        gameOverText.GetComponent<Text>().text = score.ToString();
        GameObject.Find("UIinformation").GetComponent<Canvas>().enabled = false;
        GameObject.Find("GameOverUI").GetComponent<Canvas>().enabled = true;
        Time.timeScale = 0;
    }
    private void Pass()
    {
        Time.timeScale = 0;
        GameObject.Find("PassUI").GetComponent<Canvas>().enabled = true;
    }
}

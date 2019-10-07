using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PalyerSnake02 : MonoBehaviour
{
    public GameObject camera;
    Vector3 cameraToSnake = new Vector3(0, 0, -10);//创建一个向量，用来定义camera与Snake之间的距离，放在这里避免Update过程中的重复创建
    float angle;
    public float speed = 10.0f;
    Vector3 pos;
    Vector3 chaPos;
    public float distanceCanMove = 2.0f;//鼠标与snakeHead之间达到一定距离才可以修改移动的方向
    public List<Transform> bodyList = new List<Transform>();//身体
    public List<Vector3> bodyListLastTransform = new List<Vector3>();
    public GameObject snakeBody;
    public Sprite[] snakeHeadSprites = new Sprite[4];
    public Sprite[] snakeBodySprites = new Sprite[4];

    public Text scoreText;
    public Text bodyLengthText;
    public Text GameOverText;

    private static int snakeSkin = 0;//设置蛇的皮肤，static类型便于直接进行修改
    public static void SnakeSkin(int skin)//定义静态方法，提供操作入口
    {
        snakeSkin = skin;
    }
    public int score = 0;//记录当前的分数
    private bool alwaysFolloeMouse = true;
    public void ChangeFollow()
    {
        alwaysFolloeMouse = !alwaysFolloeMouse;
    }
    private void Start()
    {
        //设置snake的皮肤
        this.GetComponent<SpriteRenderer>().sprite = snakeHeadSprites[snakeSkin];
        snakeBody.GetComponent<SpriteRenderer>().sprite = snakeBodySprites[snakeSkin];
        chaPos = new Vector3(0, 0, 0);
        //初始化lastPos
        InvokeRepeating("Move", 0, 0.04f);
        for (int i = 0; i < 4; i++)//设置初始长度为4
            Grow();
    }
    private void FixedUpdate()
    {
        scoreText.GetComponent<Text>().text = score.ToString();
        bodyLengthText.GetComponent<Text>().text = (bodyList.Count + 1).ToString();
        pos = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
        if ((alwaysFolloeMouse == true && (pos - transform.position).magnitude >= distanceCanMove) || (alwaysFolloeMouse == false && Input.GetMouseButtonDown(0)))//鼠标与snakehead之间的距离达到一定值才可以更改运动的方向
        {
            angle = Vector3.Angle(pos, Vector3.down);
            chaPos = pos - transform.position;
            //设置snake的旋转
            float fireangle;//发射角度
            //计算鼠标位置与对象位置之间的角度
            Vector2 targetDir = pos - transform.position;
            fireangle = Vector2.Angle(targetDir, Vector3.up);
            if (pos.x > transform.position.x)
            {
                fireangle = -fireangle;
            }
            this.transform.eulerAngles = new Vector3(0, 0, fireangle);
        }
        transform.position += chaPos.normalized * speed * Time.deltaTime;
        camera.GetComponent<Transform>().position = transform.position + cameraToSnake;

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
    /// <summary>
    /// 摧毁最后一个列表中的最后一个元素
    /// </summary>
    public void DistoryBody()
    {
        if (bodyList.Count > 0)
        {
            if(bodyList[bodyList.Count - 1]!=null)
            {
                GameObject obj = bodyList[bodyList.Count - 1].gameObject;
                GameObject.Destroy(obj);
            }
            
            bodyList.RemoveAt(bodyList.Count - 1);
            if(bodyListLastTransform.Count > 0)
                bodyListLastTransform.RemoveAt(bodyListLastTransform.Count - 1);
        }

    }

    private void Move()
    {
        //通过速度设置进行移动
        if (bodyList.Count > 0)
            for (int i = 0; i < bodyListLastTransform.Count; i++)
            {
                //检测到前方的body已经被distroy，重置列表
                if(bodyList[i] == null)
                {
                    for(int j = i; j < bodyList.Count;j ++ )
                        DistoryBody();
                }
                else
                {
                    Vector2 temp = (bodyListLastTransform[i] - bodyList[i].position).normalized;
                    bodyList[i].GetComponent<Rigidbody2D>().velocity = temp * speed;
                }
                
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
            if (bodyList[i] == null)
            {
                for (int j = i; j < bodyList.Count; j++)
                    DistoryBody();
            }
            else
            {
                Vector3 vector3 = new Vector3(0, 0, 0);
                vector3.x = bodyList[i].position.x;
                vector3.y = bodyList[i].position.y;
                bodyListLastTransform.Add(vector3);
            }
            
        }
    }

    public AudioSource audio;
    //以下是游戏逻辑实现
    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Food")
        {
            audio.Play();
            Destroy(collision.gameObject);
            score += 100;
            Grow();
        }
        else if (collision.name == "key")
        {
            audio.Play();
            Destroy(collision.gameObject);
            Destroy(GameObject.Find("lockedDoor"));
        }
        else if (collision.name == "exit")
        {
            Pass();
        }
        else if(collision.tag == "Thron")
        {
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
        GameObject.Find("UIinformation").GetComponent<Canvas>().enabled = false;
        GameObject.Find("GameOverUI").GetComponent<Canvas>().enabled = true;
        GameOverText.GetComponent<Text>().text = score.ToString();
        Time.timeScale = 0;
    }
    private void Pass()
    {
        Time.timeScale = 0;
        GameObject.Find("PassUI").GetComponent<Canvas>().enabled = true;
    }
}

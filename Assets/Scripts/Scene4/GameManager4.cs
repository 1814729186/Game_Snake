using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Great By mysz
Date:20191007
*/
///<summary>
///模式4的manager
///</summary>
public class GameManager4 : MonoBehaviour
{
    static System.Random random = new System.Random();//随机数生成
    public Sprite[] color = new Sprite[4];//得到游戏中存在的四种颜色

    public GameObject line;//得到游戏场景中需要生成的变色线
    public GameColor lineCol = GameColor.Blue;
    private GameObject curLine;//当前位置的line

    public GameObject lineBox;//四个整齐排列的方格

    public GameObject RotateBox;//旋转的方格

    public GameObject wall;//得到游戏中的墙壁

    public GameObject snakeHead;//得到snake对象

    /// <summary>
    /// 生成游戏的墙壁边界，参数为横坐标，纵坐标为10.5和-10.5
    /// </summary>
    /// <param name="x"></param>
    private void CreatWall(float x)
    {
        //生成游戏中的两堵墙
        Instantiate(wall,new Vector3(x,10.5f,0),new Quaternion(0,0,0,0));
        Instantiate(wall,new Vector3(x,-10.5f,0),new Quaternion(0,0,0,0));
    }
    private bool isCreatWall = false;//当前位置是否已经生成墙壁

    /// <summary>
    /// 生成游戏中出现的细线,参数为生成位置的横坐标
    /// </summary>
    private void CreatLine(float x)
    {
        curLine = Instantiate(line,new Vector3(x,0,0),new Quaternion(0,0,0,0));
        int num = random.Next(0,4);
        curLine.GetComponent<SpriteRenderer>().sprite = color[num];
    }
    /// <summary>
    /// 按时间改变line的颜色(使用挂载于对象上的脚本来改变颜色)
    /// </summary>
    private void ChangeLineColor()
    {
        curLine.GetComponent<SpriteRenderer>().sprite = color[((int)lineCol)];
        lineCol = lineCol + 1;
        if ((int)lineCol == 4)
            lineCol = 0;//重置颜色的改变
    }

    public float timeScale = 1f;
    /// <summary>
    /// 生成四个纵向排列的方块,参数为生成位置的横坐标,纵坐标7.5，2.5，-2.5，-7.5
    /// </summary>
    private void CreatBox(float x,bool isRandom)
    {
        GameObject obj;
        if(isRandom == false)
        {
            int num = random.Next(0, 4);
            obj = Instantiate(lineBox, new Vector3(x, 7.5f), new Quaternion());
            obj.GetComponent<SpriteRenderer>().sprite = color[num % 4];
            obj.GetComponent<LineBox>().curColor = num % 4;
            num++;
            obj = Instantiate(lineBox, new Vector3(x, 2.5f), new Quaternion());
            obj.GetComponent<SpriteRenderer>().sprite = color[num % 4];
            obj.GetComponent<LineBox>().curColor = num % 4;

            num++;
            obj = Instantiate(lineBox, new Vector3(x, -2.5f), new Quaternion());
            obj.GetComponent<SpriteRenderer>().sprite = color[num % 4];
            obj.GetComponent<LineBox>().curColor = num % 4;

            num++;
            obj = Instantiate(lineBox, new Vector3(x, -7.5f), new Quaternion());
            obj.GetComponent<SpriteRenderer>().sprite = color[num % 4];
            obj.GetComponent<LineBox>().curColor = num % 4;

            num++;
        }
        else//在一定区域范围内自动生成
        {
            int boxNum = random.Next(0,4);
            for(int i = 0;i < boxNum;i ++)
            {
                int xRan = random.Next(-10,10);
                int yRan = random.Next(-10,10);//取得范围区域的任意点
                obj = Instantiate(lineBox,new Vector3(x + xRan,yRan),new Quaternion());
                int co = random.Next(0,4);//取得随机的颜色
                obj.GetComponent<SpriteRenderer>().sprite = color[co];
                obj.GetComponent<LineBox>().curColor = co;

            }
        }
        
    }

    /// <summary>
    /// 生成旋转的物体方块，参数为生成位置的横坐标，生成位置的纵坐标,四个物体为一组,在物体挂载的代码中设置转动的速度
    /// </summary>
    private void CreatRotateBox(float x,bool isRandom)
    {
        GameObject obj;
        if (isRandom == false)
        {
            int num = random.Next(0, 4);
            obj = Instantiate(RotateBox, new Vector3(x - 5, -5), new Quaternion());
            obj.GetComponent<SpriteRenderer>().sprite = color[num % 4];
            obj.GetComponent<RotateBox>().curColor = num % 4;
            num++;
            obj = Instantiate(RotateBox, new Vector3(x - 5, 5), new Quaternion());
            obj.GetComponent<SpriteRenderer>().sprite = color[num % 4];
            obj.GetComponent<RotateBox>().curColor = num % 4;

            num++;
            obj = Instantiate(RotateBox, new Vector3(x + 5, -5), new Quaternion());
            obj.GetComponent<SpriteRenderer>().sprite = color[num % 4];
            obj.GetComponent<RotateBox>().curColor = num % 4;

            num++;
            obj = Instantiate(RotateBox, new Vector3(x + 5, 5), new Quaternion());
            obj.GetComponent<SpriteRenderer>().sprite = color[num % 4];
            obj.GetComponent<RotateBox>().curColor = num % 4;

            num++;
        }
        else
        {
            int boxNum = random.Next(0, 4);
            for (int i = 0; i < boxNum; i++)
            {
                int xRan = random.Next(-10, 10);
                int yRan = random.Next(-10, 10);//取得范围区域的任意点
                obj = Instantiate(RotateBox, new Vector3(x + xRan, yRan), new Quaternion());
                int co = random.Next(0, 4);//取得随机的颜色
                obj.GetComponent<SpriteRenderer>().sprite = color[co];
                obj.GetComponent<RotateBox>().curColor = co;
            }
               
        }

        
    }

    private bool isPause = false;
    private float x;//记录当前扫描位置的横坐标
    private void Start()
    {
        x = snakeHead.transform.position.x + 100f;//获得当前位置的横坐标
    }

    int intX;//记录扫描位置地横坐标
    private bool isCreatBox = false;
    private void FixedUpdate()
    {
        Time.timeScale = timeScale;
        x = snakeHead.transform.position.x + 100.0f;//获得当前位置的横坐标

        if (intX != (int)x)//位置改变
        {
            isCreatWall = false;
            isCreatBox = false;
            intX = (int)x;
        }
        
        
        if (intX % 9 == 0&&isCreatWall == false)
        {
            CreatWall(intX);
            isCreatWall = true;
        }
            
        x = snakeHead.transform.position.x + 30.0f;
        if(intX <= 330)//有规律地生成
        {
            if((intX-30)%100 == 20&&isCreatBox==false )
            {
                CreatLine(x);
                isCreatBox = true;
            }
            if((intX - 30)%100 == 50 && isCreatBox == false)
            {
                isCreatBox = true;
                CreatBox(x,false);
            }
            if((intX - 30)%100 == 80 && isCreatBox == false)
            {
                isCreatBox = true;
                CreatRotateBox(x,false);
            }
        }
        else//无规律生成
        {
            if((intX-330)%20==0&&isCreatBox == false)
            {
                int num = random.Next(0,3);
                if (num == 0)
                    CreatLine(intX);
                else if (num == 1)
                    CreatBox(intX,random.Next(0,2) == 0?true:false);
                else CreatRotateBox(intX, random.Next(0, 2) == 0 ? true : false);
                isCreatBox = true;
            }
        }
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (isPause == false)
            {
                Pause();
                isPause = true;
            }
            else if (isPause == true)
            {
                UnPause();
                isPause = false;
            }
        }
    }
    public void Pause()
    {
        GameObject.Find("PauseUI").GetComponent<Canvas>().enabled = true;
        Time.timeScale = 0;
    }
    public void UnPause()
    {
        GameObject.Find("PauseUI").GetComponent<Canvas>().enabled = false;
        Time.timeScale = timeScale;
    }

    public AudioSource audio;//按键音效
    public void  PlayButtonVolumn()
    {
        audio.Play();
    }
    public void  LoadScene(int sceneNum)
    {
        Application.LoadLevel(sceneNum);
    }


}

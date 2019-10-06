using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Great By mysz
Date:20191006
*/
///<summary>
///挂载于障碍方块的物体
///</summary>
public class Diamond : MonoBehaviour
{
    private int score = 1;
    private static System.Random random = new System.Random();
    public GameObject text;
    public int Score//属性，数据读取入口
    {
        get
        {
            return score;
        }
    }

    private void Start()
    {
        score = random.Next(1, 50);
        text.GetComponent<TextMesh>().text = score.ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Great By mysz
Date:
*/
///<summary>
///
///</summary>
public class Line : MonoBehaviour
{
    public static System.Random random = new System.Random();
    public Sprite[] color = new Sprite[4];
    public GameColor curColor;

    private void Start()
    {
        InvokeRepeating("ChangeColor",0,0.2f);
    }

    private void ChangeColor()
    {
        curColor  = (GameColor)random.Next(0,4);
        this.GetComponent<SpriteRenderer>().sprite = color[(int)curColor];
    }


}

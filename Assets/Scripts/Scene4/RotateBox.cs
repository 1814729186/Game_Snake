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
public class RotateBox : MonoBehaviour
{
    static System.Random random = new System.Random();
    public float angularV = 10;
    public int curColor;
    private void Start()
    {
        angularV = random.Next(2,5);
    }
    private void FixedUpdate()
    {
        this.transform.Rotate(Vector3.back*angularV);
    }
}

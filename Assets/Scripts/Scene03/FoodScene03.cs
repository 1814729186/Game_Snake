using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
Great By mysz
Date:
*/
///<summary>
///
///</summary>
public class FoodScene03 : MonoBehaviour
{
    private static System.Random random = new System.Random();
    public GameObject score;
    public int foodScore = 1;
    public int FoodScore {
        get
        {
            return foodScore;
        }
        set
        {
            foodScore = value;
        }
    }
    private void Start()
    {
        FoodScore = random.Next(0,50);
        score.GetComponent<TextMesh>().text = FoodScore.ToString();
    }
}

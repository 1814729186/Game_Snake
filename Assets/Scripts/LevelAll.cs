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
public class LevelAll : MonoBehaviour
{
    public Toggle level1;
    public Toggle level2;
    public Toggle level3;

    public float timeScale = 1f;
    public GameObject obj;
    public void GetTheVal()
    {
        if (level1.GetComponent<Toggle>().isOn == true)
            timeScale = 0.5f;
        else if (level2.GetComponent<Toggle>().isOn == true)
            timeScale = 1.0f;
        else if (level3.GetComponent<Toggle>().isOn == true)
            timeScale = 1.5f;

    }

}

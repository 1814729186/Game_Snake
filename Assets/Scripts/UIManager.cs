using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public void ShowCurUI()
    {
        transform.GetComponent<Canvas>().enabled = true;
    }
    public void CloseCurUI()
    {
        transform.GetComponent<Canvas>().enabled = false;
    }
}

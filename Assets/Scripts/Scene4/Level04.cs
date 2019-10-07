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
public class Level04 : LevelAll
{
    private void Update()
    {
        obj.GetComponent<GameManager4>().timeScale = timeScale;
    }
}

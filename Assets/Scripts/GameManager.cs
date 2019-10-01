using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject wallPrefab;//得到墙的预制体
    void Start()
    {
        WallBoxMap wallMap = new WallBoxMap();
        wallMap.CreatMapOfScene02();
        bool[,] map = wallMap.Map;
        for(int i = 0;i < map.GetLength(0); i ++)
        {
            for(int j = 0;j < map.GetLength(0); j++)
            {
                if (map[i, j] == false)
                    Instantiate(wallPrefab, new Vector3(i, j, 0), new Quaternion());
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

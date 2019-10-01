public class WallBoxMap
{ 
    private bool [,] map;//游戏地图
    public bool[,] Map {
        get
        {
            return map;
        }
    }//设置属性，通过属性访问字段
    private const int DEFAULT = 20;//默认的游戏地图边长
    private int removeTimes;//记录RemoveBox方法实际执行的次数
    private const int TIMES = 80;//设置一个Remove的最大执行次数
    /// <summary>
    /// 无参构造，默认创建一个边长为DEFAULT的二维数组，作为游戏地图
    /// </summary>
    public WallBoxMap() {
        this.map = new bool[DEFAULT, DEFAULT]; 
    }
    public WallBoxMap(int length)
    {
        this.map = new bool[length,length];
    }
    private static System.Random random = new System.Random();
    /// <summary>
    /// 实例方法，创建一个具有通路的迷宫
    /// </summary>
    public void CreatMapOfScene02()
    {
        int[] curPoint = new int[] {1,1 };//设置一个扫描指针，指向数组中的代码块，初始值设置为为迷宫入口
        while(removeTimes <= TIMES)
        {
            int direction = random.Next(0, 4);
            int length = random.Next(0, map.GetLength(0));
            RemoveBox(curPoint, direction, length);
        }
        //最后再朝着一个方向移动直至到达迷宫的边界，并将边界设置为出口
        int L_direction = random.Next(0, 4);
        RemoveBox(curPoint, L_direction);
    }

    /// <summary>
    /// 实例方法，检测扫描指针是否到达边界，如果是，则返回true,否则返回false
    /// </summary>
    /// <param name="curPoint"></param>
    /// <returns></returns>
    private bool IsReachBorder(int[] curPoint)
    {
        if (curPoint[0] == 0 || curPoint[0] == map.GetLength(0)-1 || curPoint[1] == 0 || curPoint[1] == map.GetLength(0)-1)
            return true;
        else
            return false;
    }
    /// <summary>
    /// 扫描指针从某个起点开始，向一个随机的方向，移动一个随机的距离，经过的路径上，将二维数组的false设置为true，为true的位置为通路
    /// </summary>
    /// <param name="direction"></param>
    /// <param name="lenght"></param>
    private void RemoveBox(int [] curPoint,int direction,int length)
    {
        //向上移动
        if (direction == 0)
        {
            for (int i = 0; i < length; i++)
            {
                curPoint[0]++;
                if (IsReachBorder(curPoint))//如果到达了边界
                {
                    curPoint[0]--;//撤销指针的修改
                    if (i != 0)//到达边界之前发生了移动
                        removeTimes++;
                    return;
                }
                map[curPoint[0], curPoint[1]] = true;
            }
        }
        //向左移动
        else if (direction == 1)
        {
            for (int i = 0; i < length; i++)
            {
                curPoint[1]--;
                if (IsReachBorder(curPoint))//如果到达了边界
                {
                    curPoint[1]++;//撤销指针的修改
                    if (i != 0)//到达边界之前发生了移动
                        removeTimes++;
                    return;
                }
                map[curPoint[0], curPoint[1]] = true;
            }
        }
        //向右移动
        else if (direction == 2)
        {
            for (int i = 0; i < length; i++)
            {
                curPoint[1]++;
                if (IsReachBorder(curPoint))//如果到达了边界
                {
                    curPoint[1]--;//撤销指针的修改
                    if (i != 0)//到达边界之前发生了移动
                        removeTimes++;
                    return;
                }
                map[curPoint[0], curPoint[1]] = true;
            }
        }
        //向下移动
        else if(direction == 3)
        {
            for (int i = 0; i < length; i++)
            {
                curPoint[0]--;
                if (IsReachBorder(curPoint))//如果到达了边界
                {
                    curPoint[0]++;//撤销指针的修改
                    if (i != 0)//到达边界之前发生了移动
                        removeTimes++;
                    return;
                }
                map[curPoint[0], curPoint[1]] = true;
            }
        }
        removeTimes++;
    }
    //重载remove方法，向一个方向移动直至边界
    private void RemoveBox(int[] curPoint, int direction)
    {
        //向上移动
        if (direction == 0)
        {
            while(true)
            {
                curPoint[0]++;
                map[curPoint[0], curPoint[1]] = true;
                if (IsReachBorder(curPoint))
                    return;
            }
        }
        //向左移动
        else if (direction == 1)
        {
            while (true)
            {
                curPoint[1]--;
                map[curPoint[0], curPoint[1]] = true;
                if (IsReachBorder(curPoint))
                    return;
            }
        }
        //向右移动
        else if (direction == 2)
        {
            while (true)
            {
                curPoint[1]++;
                map[curPoint[0], curPoint[1]] = true;
                if (IsReachBorder(curPoint))
                    return;
            }
        }
        //向下移动
        else if (direction == 3)
        {
            while (true)
            {
                curPoint[0]--;
                map[curPoint[0], curPoint[1]] = true;
                if (IsReachBorder(curPoint))
                    return;
            }
        }
        removeTimes++;
    }
}

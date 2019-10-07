using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManagerStart : MonoBehaviour
{
    public GameObject camera;
    public GameObject slider1;
    public GameObject slider2;
    public GameObject toggle1;
    public GameObject toggle2;
    public GameObject skinToogle0;
    public GameObject skinToogle1;
    public GameObject skinToogle2;
    public GameObject skinToogle3;



    public GameObject gameEvent;

    private void Start()
    {
        Time.timeScale = 1;
    }
    private void Update()
    {
        //设置音量和音效
        if(toggle1.GetComponent<Toggle>().isOn == true)
        {
            gameEvent.GetComponent<AudioSource>().volume = slider2.GetComponent<Slider>().value;
        }
        else
        {
            gameEvent.GetComponent<AudioSource>().volume = 0;
        }
        if (toggle2.GetComponent<Toggle>().isOn == true)
        {
            camera.GetComponent<AudioSource>().volume = slider1.GetComponent<Slider>().value;
        }
        else
        {
            camera.GetComponent<AudioSource>().volume = 0;
        }
        //设置皮肤



    }
    public void ChangeScene(int sceneNum)
    {
            Application.LoadLevel(sceneNum);
    }
    public void LoginOut()
    {
        Application.Quit();//退出游戏
    }
    public void ChangeSkin(int skin)
    {
        PlayerSnake.SnakeSkin(skin);
    }
}

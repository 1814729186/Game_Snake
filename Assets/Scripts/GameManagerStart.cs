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

    public GameObject gameEvent;
    private void Update()
    {
        
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
    }
    public void ChangeScene(string sceneName)
    {
        Application.LoadLevel(sceneName);
    }
    public void ChangeSkin(int skin)
    {
        PlayerSnake.SnakeSkin(skin);
    }
}

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
public class AudioManager01 : MonoBehaviour
{
    public GameObject bgmToggle;
    public GameObject volumnToggle;
    public GameObject bgmSlider;
    public GameObject volumnSlider;

    public GameObject snake;
    public GameObject volumn;
    public GameObject camera;

    public static float bgmVolumnVal = 1;
    public static float volumnVal = 1;
    private void Start()
    {
        bgmVolumnVal = bgmSlider.GetComponent<Slider>().value;
        volumnVal = volumnSlider.GetComponent<Slider>().value;

    }
    private void Update()
    {
        if (bgmToggle.GetComponent<Toggle>().isOn == false)
            bgmVolumnVal = 0;
        bgmSlider.GetComponent<Slider>().value = bgmVolumnVal;
        if (volumnToggle.GetComponent<Toggle>().isOn = false)
            volumnVal = 0;
        volumnSlider.GetComponent<Slider>().value = 0;
        camera.GetComponent<AudioSource>().volume = volumnVal;
        volumn.GetComponent<AudioSource>().volume = bgmVolumnVal;
        snake.GetComponent<PlayerSnake>().volumnVal = volumnVal;
    }

}

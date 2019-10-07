using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
Great By mysz
Date:
*/
///<summary>
///用于控制音效
///</summary>
public class AudioManagerAll : MonoBehaviour
{
    public Toggle bgmToggle;
    public Slider bgmSlider;
    public Toggle volumnToggle;
    public Slider volumnSlider;

    public List<GameObject> obj = new List<GameObject>();//新建一个链表，用于储存含有音效的组件 
    public List<AudioSource> aud = new List<AudioSource>();//创建Audio链表
    public List<GameObject> objBgm = new List<GameObject>();//储存含有BGM的对象
    public List<AudioSource> audBgm = new List<AudioSource>();//储存含有BGM的对组件

    private void Update()
    {
        float bgmVal = bgmSlider.GetComponent<Slider>().value;
        if (bgmToggle.GetComponent<Toggle>().isOn == false)
            bgmVal = 0;
        float volumnVal = volumnSlider.GetComponent<Slider>().value;
        if (volumnToggle.GetComponent<Toggle>().isOn == false)
            volumnVal = 0;


        for (int i = 0; i < objBgm.Count; i++)
        {
            objBgm[i].GetComponent<AudioSource>().volume = bgmVal;
        }
        for (int i = 0; i < audBgm.Count; i++)
        {
            audBgm[i].GetComponent<AudioSource>().volume = bgmVal;
        }
        for(int i = 0;i < obj.Count;i ++)
        {
            obj[i].GetComponent<AudioSource>().volume = volumnVal;
        }
        for (int i = 0; i < aud.Count; i++)
        {
            aud[i].GetComponent<AudioSource>().volume = volumnVal;
        }
    }
}

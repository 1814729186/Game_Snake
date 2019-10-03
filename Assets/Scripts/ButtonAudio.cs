using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///控制button的声音
///</summary>
public class ButtonAudio : MonoBehaviour
{
    public AudioSource AudioSource;
    public void PlayButtonAudio()
    {
        AudioSource.Play();
    }
}

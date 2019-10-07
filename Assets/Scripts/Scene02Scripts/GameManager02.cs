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
public class GameManager02 : MonoBehaviour
{
    public AudioSource audio;
    private bool isPause = false;
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (isPause == false)
                Pause();
            else
                UnPause();
        }
            
    }
    private void Pause()
    {
        Time.timeScale = 0;
        isPause = true;
        GameObject.Find("PauseUI").GetComponent<Canvas>().enabled = true;
    }
    private void UnPause()
    {
        Time.timeScale = 1;
        isPause = false;
        GameObject.Find("PauseUI").GetComponent<Canvas>().enabled = false;

    }
    public void LoadScene(int sceneNum)
    {
        Application.LoadLevel(sceneNum);
    }
    public void PlayButtonVolumn()
    {
        audio.Play();
    }
}

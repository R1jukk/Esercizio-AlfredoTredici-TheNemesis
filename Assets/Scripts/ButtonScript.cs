using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    public void PlayButton()
    {
        SceneManager.LoadScene("LoadingScene");
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}

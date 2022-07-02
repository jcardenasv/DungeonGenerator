using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioSource _buttonAudio;
    public void PlayGame ()
    {
        _buttonAudio.Play();
        GameController.SetDefaultParameters();
        Debug.Log(GameController.Seed);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        _buttonAudio.Play();
        Application.Quit();
    }
}

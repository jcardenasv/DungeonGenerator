using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class EndGameMenu : MonoBehaviour
{
    public static bool IsGameEnded = false;
    public static bool IsGameWinned = false;

    [SerializeField] private GameObject EndGameMenuUI;
    [SerializeField] private TextMeshProUGUI EndGameText;

    // Update is called once per frame
    void Update()
    {

        if (IsGameEnded)
        {
            OpenEndGameMenu(IsGameWinned);
        } 

    }

    public static void EndGame(bool win = false){
        IsGameEnded = true;
        IsGameWinned = win;
    }

    public void OpenEndGameMenu(bool win = false)
    {
        if (win) {
            EndGameText.text = "You win";
        } else {
            EndGameText.text = "You lose :c";
        }
        
        IsGameEnded = true;
        EndGameMenuUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        IsGameEnded = false;
        EndGameMenuUI.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        
    }

    public void RestartGame()
    {
        IsGameEnded = false;
        EndGameMenuUI.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

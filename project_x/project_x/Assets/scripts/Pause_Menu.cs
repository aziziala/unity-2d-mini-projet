using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause_Menu : MonoBehaviour {

    public GameObject pause_menu;

    public void PauseGame()
    {
        Time.timeScale = 0f;
        pause_menu.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pause_menu.SetActive(false);
    }


    public void RestartGame()
    {
        Time.timeScale = 1f;
        pause_menu.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }
}

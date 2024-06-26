using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [SerializeField] private bool clickOnce = false;
    [SerializeField] private GameObject pause;
    private void Update()
    {
        SetPause();
    }
    public void LoadLevel()
    {
        SceneManager.LoadScene("MainLevel");
        Time.timeScale = 1;
    }

    private void SetPause()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if (!clickOnce)
            {
                pause.SetActive(true);
                Time.timeScale = 0f;
                clickOnce = true;
            }
            else
            {
                pause.SetActive(false);
                Time.timeScale = 1f;
                clickOnce = false;
            }
        }
    }

    public void Retry()
    {
        SceneManager.LoadScene("MainLevel");
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Victory()
    {
        SceneManager.LoadScene("Victory");
    }

    public void Defeat()
    {
        SceneManager.LoadScene("Defeat");
    }

    public void Exit()
    {
        Debug.Log("Cerrando el juego...");
        Application.Quit();
    }
}

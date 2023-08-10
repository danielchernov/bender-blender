using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    GameObject mainMenu;

    [SerializeField]
    GameObject creditsMenu;

    [SerializeField]
    GameObject pauseMenu;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    private void Update()
    {
        if (pauseMenu != null && Input.GetButtonDown("Cancel"))
        {
            pauseMenu.SetActive(!pauseMenu.activeSelf);
        }
    }

    public void NewGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowCredits()
    {
        mainMenu.SetActive(false);
        creditsMenu.SetActive(true);
    }

    public void BackToMenu()
    {
        mainMenu.SetActive(true);
        creditsMenu.SetActive(false);
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene(1);
    }

    public void QuitToMenu()
    {
        // Time.timeScale = 1;
        // Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(0);
    }
}

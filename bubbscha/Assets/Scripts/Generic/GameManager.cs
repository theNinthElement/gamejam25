using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private RikschaControll rikschaActions;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject continueButton;
    [SerializeField] RikschaController player;
    [SerializeField] InputActionAsset actions;

    private void Awake()
    {
        rikschaActions = new RikschaControll();
        rikschaActions.InGame.PauseGame.performed += PauseGame;
        rikschaActions.InGame.Enable();
    }

    private void PauseGame(InputAction.CallbackContext context)
    {
        pauseMenu.SetActive(true);
        rikschaActions.InGame.PauseGame.performed -= PauseGame;
        rikschaActions.InGame.PauseGame.performed += ContinueGame;
        Time.timeScale = 0.0f;
    }

    public void PlayerCollision(float force)
    {
        player.AddExternalPushForce(force);
    }
    public void ContinueGame()
    {
        pauseMenu.SetActive(false);
        rikschaActions.InGame.PauseGame.performed += PauseGame;
        rikschaActions.InGame.PauseGame.performed -= ContinueGame;
        Time.timeScale = 1.0f;
    }
    private void ContinueGame(InputAction.CallbackContext context)
    {
        pauseMenu.SetActive(false);
        rikschaActions.InGame.PauseGame.performed += PauseGame;
        rikschaActions.InGame.PauseGame.performed -= ContinueGame;
        Time.timeScale = 1.0f;
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void GameOver()
    {
        //TODO: Stop Game, Show Scoreboard, Player can add his Score
        Time.timeScale = 0.0f;
        pauseMenu.SetActive(true);
        continueButton.SetActive(false);
    }
    public void EndGame()
    {
        Application.Quit();
    }
}

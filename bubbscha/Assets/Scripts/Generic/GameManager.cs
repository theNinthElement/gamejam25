using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private RikschaControll rikschaActions;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject enterScore;
    [SerializeField] GameObject continueButton;
    public RikschaController player;
    [SerializeField] InputActionAsset actions;

    public bool isRunning;
    //[SerializeField] int scoreThreshold=1000;

    //private int thresholdCount=0;
    private void Awake()
    {
        instance = this;
        rikschaActions = new RikschaControll();
        rikschaActions.InGame.PauseGame.performed += PauseGame;
        rikschaActions.InGame.Enable();
        PauseGame();
    }

    private void PauseGame(InputAction.CallbackContext context)
    {
        PauseGame();
    }

    private void PauseGame()
    {
        pauseMenu.SetActive(true);
        isRunning = false;
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
        isRunning = true;
        rikschaActions.InGame.PauseGame.performed += PauseGame;
        rikschaActions.InGame.PauseGame.performed -= ContinueGame;
        Time.timeScale = 1.0f;
    }
    private void ContinueGame(InputAction.CallbackContext context)
    {
        ContinueGame();
    }
    public void RestartGame()
    {
        Time.timeScale = 1.0f;
        isRunning = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void GameOver()
    {
        //TODO: Stop Game, Show Scoreboard, Player can add his Score
        isRunning = false;
        Time.timeScale = 0.0f;
        enterScore.SetActive(true);
        pauseMenu.SetActive(true);
        continueButton.SetActive(false);
    }
    public void EndGame()
    {
        Debug.Log("Application closed");
        Application.Quit();
    }
}

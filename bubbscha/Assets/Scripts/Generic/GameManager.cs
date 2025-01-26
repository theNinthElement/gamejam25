using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

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

    [FormerlySerializedAs("_onGameStart")] [SerializeField] private UnityEvent _onContinue;
    [SerializeField] private UnityEvent _onPause;
    //[SerializeField] int scoreThreshold=1000;

    //private int thresholdCount=0;
    private void Awake()
    {
        instance = this;
        GetRikschawInputActions().InGame.PauseGame.performed += PauseGame;
        GetRikschawInputActions().InGame.Enable();
        PauseGame();
    }

    private void PauseGame(InputAction.CallbackContext context)
    {
        PauseGame();
    }

    private void PauseGame()
    {
        pauseMenu.SetActive(true);
        _onPause.Invoke();
        isRunning = false;
        GetRikschawInputActions().InGame.PauseGame.performed -= PauseGame;
        GetRikschawInputActions().InGame.PauseGame.performed += ContinueGame;
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
        GetRikschawInputActions().InGame.PauseGame.performed += PauseGame;
        GetRikschawInputActions().InGame.PauseGame.performed -= ContinueGame;
        _onContinue.Invoke();
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

    public RikschaControll GetRikschawInputActions()
    {
        if (rikschaActions == null)
        {
            rikschaActions = new RikschaControll();
        }
        return rikschaActions;
    }
}

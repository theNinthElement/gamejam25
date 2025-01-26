using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private RikschaControll rikschaActions;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject enterScore;
    [SerializeField] GameObject continueButton;
    [SerializeField] Button restartButton;
    public RikschaController player;
    [SerializeField] InputActionAsset actions;
    public bool isRunning;
    [SerializeField] PlayableDirector introCutscenePlayable;
    private bool isCutscenePlaying = false;

    [FormerlySerializedAs("_onGameStart")] [SerializeField] private UnityEvent _onContinue;
    [SerializeField] private UnityEvent _onPause;
    //[SerializeField] int scoreThreshold=1000;

    //private int thresholdCount=0;
    private void Awake()
    {
        instance = this;
        GetRikschawInputActions().InGame.PauseGame.performed += PauseGame;
        GetRikschawInputActions().PauseMenu.UnPause.performed += ContinueGame;
        GetRikschawInputActions().PauseMenu.Select.performed += Select_performed;
        GetRikschawInputActions().PauseMenu.Navigate.started += Navigate_started;
        GetRikschawInputActions().IntroCutscene.Skip.canceled += Skip_Performed;
        PauseGame();
        if (introCutscenePlayable != null)
        {
            StartCutscene(); 
        }
    }

    private void OnDestroy()
    {
        rikschaActions.Dispose();
    }

    private void Navigate_started(InputAction.CallbackContext obj)
    {
        float direction = obj.ReadValue<float>();
        Selectable nextSelected = null;
        Selectable currentSelected = null;
        if (EventSystem.current.currentSelectedGameObject != null)
        {
            currentSelected = EventSystem.current.currentSelectedGameObject.GetComponent<Selectable>();
        }
        if (currentSelected == null)
        {
            currentSelected = restartButton;
            nextSelected = restartButton;
        }
        if (direction < 0)
        {
            nextSelected = currentSelected.navigation.selectOnLeft;
        }
        else if(direction > 0)
        {
            nextSelected = currentSelected.navigation.selectOnRight;
        }
        if (nextSelected != null)
        {
            nextSelected.Select();
        }
    }

    private void Select_performed(InputAction.CallbackContext obj)
    {
        ExecuteEvents.Execute<IPointerClickHandler>(EventSystem.current.currentSelectedGameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
    }

    private void StartCutscene()
    {
        isCutscenePlaying = true;
        pauseMenu.SetActive(false);
        GetRikschawInputActions().IntroCutscene.Enable();
        GetRikschawInputActions().InGame.Disable();
        GetRikschawInputActions().Menu.Disable();
        GetRikschawInputActions().PauseMenu.Disable();
        introCutscenePlayable.Play();
        introCutscenePlayable.stopped += IntroCutscenePlayable_stopped;
    }

    private void IntroCutscenePlayable_stopped(PlayableDirector obj)
    {
        SkipCutscene();
    }

    private void Skip_Performed(InputAction.CallbackContext obj)
    {
        SkipCutscene();
    }

    private void SkipCutscene()
    {
        isCutscenePlaying = false;
        GetRikschawInputActions().IntroCutscene.Disable();
        introCutscenePlayable.stopped -= IntroCutscenePlayable_stopped;
        introCutscenePlayable.time = introCutscenePlayable.duration;
        introCutscenePlayable.Stop();
        PauseGame();
    }

    private void PauseGame(InputAction.CallbackContext context)
    {
        PauseGame();
    }

    private void PauseGame()
    {
        pauseMenu.SetActive(true);
        restartButton.Select();
        _onPause.Invoke();
        isRunning = false;
        GetRikschawInputActions().InGame.Disable();
        GetRikschawInputActions().PauseMenu.Enable();
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
        GetRikschawInputActions().InGame.Enable();
        GetRikschawInputActions().Menu.Disable();
        GetRikschawInputActions().PauseMenu.Disable();
        _onContinue.Invoke();
        Time.timeScale = 1.0f;
    }
    private void ContinueGame(InputAction.CallbackContext context)
    {
        ContinueGame();
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void GameOver()
    {
        isRunning = false;
        Time.timeScale = 0.0f;
        //if (Scoreboard.highscoreList[Scoreboard.scoreboardSize-1].score <= GameStats.instance.GetScore())
        //{
            enterScore.SetActive(true);
        //}        
        pauseMenu.SetActive(true);
        continueButton.SetActive(false);
        GetRikschawInputActions().InGame.Disable();
        GetRikschawInputActions().PauseMenu.Disable();
        GetRikschawInputActions().Menu.Enable();

    }
    public void EndGame()
    {
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

using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class EnterScore : MonoBehaviour
{
    private RikschaControll rikschaActions;
    public UnityEvent<string> submitScore;

    private void Awake()
    {
        GameManager.instance.GetRikschawInputActions().Menu.SubmitScore.performed += SubmitScore;
    }

    private void SubmitScore(InputAction.CallbackContext context)
    {
        submitScore.Invoke(gameObject.GetComponent<TMP_InputField>().text);
        gameObject.SetActive(false);
    }
}

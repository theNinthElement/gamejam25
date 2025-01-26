using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class EnterScore : MonoBehaviour
{
    public UnityEvent<string> submitScore;

    private void Awake()
    {
        GameManager.instance.GetRikschawInputActions().Menu.SubmitScore.performed += SubmitScore;
        GameManager.instance.GetRikschawInputActions().Menu.Enable();
    }

    private void SubmitScore(InputAction.CallbackContext context)
    {
        SubmitScore();
    }

    public void SubmitScore()
    {
        submitScore.Invoke(gameObject.GetComponent<TMP_InputField>().text);
        GameManager.instance.GetRikschawInputActions().Menu.SubmitScore.performed -= SubmitScore;
        transform.parent.gameObject.SetActive(false);
        //gameObject.SetActive(false);
    }
}

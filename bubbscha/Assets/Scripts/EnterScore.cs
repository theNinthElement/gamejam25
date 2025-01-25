using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class EnterScore : MonoBehaviour
{
    private RikschaControll rikschaActions;
    public UnityEvent<string> submitScore;

    private void Awake()
    {
        rikschaActions = new RikschaControll();
        rikschaActions.Menu.SubmitScore.performed += SubmitScore;
        rikschaActions.Menu.Enable();
    }

    private void SubmitScore(InputAction.CallbackContext context)
    {
        submitScore.Invoke(gameObject.GetComponent<TMP_InputField>().text);
        rikschaActions.Menu.Disable();
    }
}

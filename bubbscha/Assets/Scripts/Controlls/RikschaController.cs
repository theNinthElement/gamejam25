using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RikschaController : MonoBehaviour
{
    private RikschaControll rikschaActions;
    public Transform rikschaTransform;
    public Transform bowlAnchorPointTransform;
    public Transform bowlTransform;
    public Vector2 roadBounds;
    public Vector2 tiltingBounds;
    [Space]
    public float moveSpeed = 5;
    public float moveKeepupSpeed = 0.5f;
    [Space]
    public float tiltSpeed = 5;
    public float tiltKeepupSpeed = 0.5f;
    public float tiltTurboMultiplier = 2;
    public float mouseSensitivity = 1;
    public float controllerSensitivity = 1;
    [Space]
    public float externalPushRecoverSpeed = 2;

    private Rigidbody bowlRigi;
    [SerializeField] private Animator rikschaAnimator;

    private bool tiltTurboActive = false;

    private float inputMoveValue = 0;
    private float currentMoveValue = 0;
    private Vector3 currentPosition = new Vector3();

    private float inputControllerTiltValue = 0;
    private float inputMouseTiltValue = 0;
    private float currentTiltValue = 0;
    private Vector3 currentTiltAngle = new Vector3();

    public float externalPushForce;

    public Vector3 startBasePosition;

    // Start is called before the first frame update
    void Start()
    {
        startBasePosition = rikschaTransform.position;
        bowlRigi = bowlTransform.GetComponent<Rigidbody>();
        GameManager.instance.GetRikschawInputActions().InGame.RikschaMoveHorizontal.performed += RikschaMoveHorizontal_performed;
        GameManager.instance.GetRikschawInputActions().InGame.RikschaMoveHorizontal.canceled += RikschaMoveHorizontal_performed;
        GameManager.instance.GetRikschawInputActions().InGame.BowlRotationX.performed += BowlRotationX_performed;
        GameManager.instance.GetRikschawInputActions().InGame.BowlRotationX.canceled += BowlRotationX_performed;
        GameManager.instance.GetRikschawInputActions().InGame.BowlRotationXMouse.performed += BowlRotationXMouse_performed;
        GameManager.instance.GetRikschawInputActions().InGame.BowlRotationXMouse.canceled += BowlRotationXMouse_performed;
        GameManager.instance.GetRikschawInputActions().InGame.TurboRotation.started += TurboRotation_started;
        GameManager.instance.GetRikschawInputActions().InGame.TurboRotation.canceled += TurboRotation_canceled;

        GameManager.instance.player = this;
    }

    private void RikschaMoveHorizontal_performed(InputAction.CallbackContext obj)
    {
        inputMoveValue = obj.ReadValue<float>();
    }

    private void BowlRotationX_performed(InputAction.CallbackContext obj)
    {
        inputControllerTiltValue = obj.ReadValue<float>();
    }

    private void BowlRotationXMouse_performed(InputAction.CallbackContext obj)
    {
        inputMouseTiltValue = obj.ReadValue<float>();
    }

    private void TurboRotation_started(InputAction.CallbackContext obj)
    {
        //AddExternalPushForce(Random.Range(-20, 20));
        tiltTurboActive = true;
    }

    private void TurboRotation_canceled(InputAction.CallbackContext obj)
    {
        tiltTurboActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        currentMoveValue = Mathf.Lerp(currentMoveValue, inputMoveValue, moveKeepupSpeed * Time.deltaTime);
        currentPosition.x += (externalPushForce + (currentMoveValue * moveSpeed)) * Time.deltaTime;
        currentPosition.x = Mathf.Clamp(currentPosition.x, roadBounds.x, roadBounds.y);
        rikschaTransform.position = startBasePosition + currentPosition;
        rikschaAnimator.SetFloat("Blend", currentMoveValue);

        externalPushForce = Mathf.Lerp(externalPushForce, 0, externalPushRecoverSpeed * Time.deltaTime);

        if (inputMouseTiltValue != 0 && Time.deltaTime > 0)
        {
            inputMouseTiltValue /= Time.deltaTime;
            inputMouseTiltValue /= 200;
        }
        inputMouseTiltValue *= mouseSensitivity;
        inputControllerTiltValue *= controllerSensitivity;
        currentTiltValue = Mathf.Lerp(currentTiltValue, inputControllerTiltValue + inputMouseTiltValue, tiltKeepupSpeed * Time.deltaTime);
        currentTiltAngle.z -= currentTiltValue * tiltSpeed * Time.deltaTime * (tiltTurboActive ? tiltTurboMultiplier : 1);
        currentTiltAngle.z = Mathf.Clamp(currentTiltAngle.z, tiltingBounds.x, tiltingBounds.y);
        bowlRigi.MovePosition(bowlAnchorPointTransform.position);
        bowlRigi.MoveRotation(Quaternion.Euler(currentTiltAngle));
    }

    public void AddExternalPushForce(float force)
    {
        externalPushForce = force;
    }

    public float GetBowlAngle()
    {
        return currentTiltAngle.z;
    }

    public Vector3 GetBowlPosition()
    {
        return bowlTransform.position;
    }
}

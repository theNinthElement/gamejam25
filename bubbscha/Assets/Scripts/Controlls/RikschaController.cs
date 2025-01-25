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
    public float moveSpeed = 5;
    public float moveKeepupSpeed = 0.5f;
    public float tiltSpeed = 5;
    public float tiltKeepupSpeed = 0.5f;
    public float tiltTurboMultiplier = 2;
    [Space]
    public float externalPushRecoverSpeed = 2;

    private Rigidbody bowlRigi;

    private bool tiltTurboActive = false;

    private float inputMoveValue = 0;
    private float currentMoveValue = 0;
    private Vector3 currentPosition = new Vector3();

    private float inputTiltValue = 0;
    private float currentTiltValue = 0;
    private Vector3 currentTiltAngle = new Vector3();

    public float externalPushForce;

    public Vector3 startBasePosition;

    // Start is called before the first frame update
    void Start()
    {
        startBasePosition = rikschaTransform.position;
        bowlRigi = bowlTransform.GetComponent<Rigidbody>();
        rikschaActions = new RikschaControll();
        rikschaActions.InGame.RikschaMoveHorizontal.performed += RikschaMoveHorizontal_performed;
        rikschaActions.InGame.RikschaMoveHorizontal.canceled += RikschaMoveHorizontal_performed;
        rikschaActions.InGame.BowlRotationX.performed += BowlRotationX_performed;
        rikschaActions.InGame.BowlRotationX.canceled += BowlRotationX_performed;
        rikschaActions.InGame.TurboRotation.started += TurboRotation_started;
        rikschaActions.InGame.TurboRotation.canceled += TurboRotation_canceled; 
        rikschaActions.InGame.Enable();

        GameManager.instance.player = this;
    }

    private void RikschaMoveHorizontal_performed(InputAction.CallbackContext obj)
    {
        inputMoveValue = obj.ReadValue<float>();
    }

    private void BowlRotationX_performed(InputAction.CallbackContext obj)
    {
        inputTiltValue = obj.ReadValue<float>();
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
        //Debug.Log("rikscha: " + currentMoveValue);
        //Vector3 newPos = rikschaTransform.position;
        currentPosition.x += (externalPushForce + (currentMoveValue * moveSpeed)) * Time.deltaTime;
        currentPosition.x = Mathf.Clamp(currentPosition.x, roadBounds.x, roadBounds.y);
        rikschaTransform.position = startBasePosition + currentPosition;
        //rikschaTransform.GetComponent<Rigidbody>().MovePosition(startBasePosition + currentPosition);

        externalPushForce = Mathf.Lerp(externalPushForce, 0, externalPushRecoverSpeed * Time.deltaTime);

        currentTiltValue = Mathf.Lerp(currentTiltValue, inputTiltValue, tiltKeepupSpeed * Time.deltaTime);
        //Debug.Log("Bowl: " + currentTiltValue);
        //Vector3 newRotation = bowlTransform.rotation.eulerAngles;
        currentTiltAngle.z -= currentTiltValue * tiltSpeed * Time.deltaTime * (tiltTurboActive ? tiltTurboMultiplier : 1);
        currentTiltAngle.z = Mathf.Clamp(currentTiltAngle.z, tiltingBounds.x, tiltingBounds.y);
        //bowlTransform.rotation = Quaternion.Euler(currentTiltAngle);
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

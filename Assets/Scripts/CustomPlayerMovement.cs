using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CustomPlayerMovement : MonoBehaviour
{

    public bool paused;

    // Look
    [SerializeField] private Transform playerCam, playerOrientation;
    [SerializeField] private float mouseSensitivity;
    [SerializeField, Range(-90, 90)] private float cameraPitchMin = -90;
    [SerializeField, Range(-90, 90)] private float cameraPitchMax = 90;
    private float cameraPitch, cameraYaw;

    [Header("Tweakables")]
    [SerializeField] float walkSpeed = 6f;
    [SerializeField][Range(0f, 0.5f)] float moveSmoothTime = 0.3f;
    [SerializeField] float gravity = -5;
    [SerializeField] float jumpPower = 20f;

    // Dash
    [SerializeField] float dashLerpDuration = 1f;
    [SerializeField] float dashStartValue = 2f;
    [SerializeField] float dashEndValue = 0.6f;
    [SerializeField] float DashingTimout = 2f;
    [SerializeField] private GameObject dashEffects;
    [SerializeField] private AnimationCurve dashPPCurve;
    [SerializeField] private Volume dashPP;
    private Vignette v;


    private float timeOfLastDash;
    private float DashMult = 1;

    // Movement
    public AudioSource dash;
    float velocityY = 0f;
    Vector2 currentDir = Vector2.zero;
    Vector2 currentDirVelocity = Vector2.zero;
    CharacterController controller = null;

    // Multipliers
    bool isJumping = false;
    bool isDashing = false;

    // Inputs 
    Vector2 i_MoveInput = Vector2.zero;
    Vector2 i_RawMouse = Vector2.zero;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        dashPP.profile.TryGet(out v);
    }

    private void Update() {
        GrabInputs();
        Look();
    }

    private void FixedUpdate()
    {
        UpdateMovement();
    }

    void GrabInputs()
    {
        i_MoveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        i_RawMouse = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        if (Input.GetKeyDown(KeyCode.Space)) { isJumping = true; }
        if (Input.GetKeyDown(KeyCode.LeftShift) && Time.time - timeOfLastDash > DashingTimout) { isDashing = true; timeOfLastDash = Time.time; Debug.Log("DASH"); }
    }

    void UpdateMovement()
    {
        Vector2 targetDirNormalised = i_MoveInput.normalized; // Makes moving diagnally speed the same
        currentDir = Vector2.SmoothDamp(currentDir, i_MoveInput, ref currentDirVelocity, moveSmoothTime);

        if (controller.isGrounded)
        {
            if (isJumping) { velocityY = jumpPower; isJumping = false; Debug.Log("Jump"); }
            else { }
        }
        else
        {
            if (isJumping) { isJumping = false; }
            if (velocityY < 1)
                velocityY += (gravity / 2) * Time.deltaTime;
            else
                velocityY += gravity * Time.deltaTime;

        }

        if (isDashing) { StartCoroutine(Dash()); }

        Vector3 velocity = (playerOrientation.transform.forward * currentDir.y + playerOrientation.transform.right * currentDir.x) * DashMult * walkSpeed + Vector3.up * velocityY;
        controller.Move(velocity * Time.deltaTime);
    }

    private void Look()
    {
        if (paused) { return; }

        //Find current look rotation
        Vector3 currentRot = playerCam.transform.localRotation.eulerAngles;

        // How much change is the player asking for?
        float changeInPitch = i_RawMouse.y * mouseSensitivity;
        float changeInYaw = i_RawMouse.x * mouseSensitivity;

        // Perform the change
        cameraPitch -= changeInPitch;
        cameraYaw += changeInYaw;

        // Do we need to clamp these changes?
        cameraPitch = Mathf.Clamp(cameraPitch, cameraPitchMin, cameraPitchMax);

        // Do the rotations
        playerCam.transform.localRotation = Quaternion.Euler(cameraPitch, cameraYaw, 0);
        playerOrientation.transform.localRotation = Quaternion.Euler(0, cameraYaw, 0);
    }

    IEnumerator Dash()
    {
        if (dash != null)
            dash.Play();

        dashEffects.SetActive(true);
        // Distort screen

        isDashing = false;
        float timeElapsed = 0;

        while (timeElapsed < dashLerpDuration)
        {
            DashMult = Mathf.Lerp(dashStartValue, dashEndValue, timeElapsed / dashLerpDuration);
            v.intensity.value = dashPPCurve.Evaluate(Mathf.Lerp(0,1, timeElapsed / dashLerpDuration));
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        dashEffects.SetActive(false);
    }
}
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWalk : MonoBehaviour
{
    public InputActionAsset InputActions;
    public Transform orientation;
    private InputAction m_moveAction;
    private InputAction m_lookAction;
    private InputAction m_jumpAction;
    private InputAction m_sprintAction;
    private Vector2 m_moveAmt;
    private Vector2 m_lookAmt;
    private Rigidbody m_rigidbody;
    public float WalkSpeed = 5;
    public float RotateSpeed = 5;
    public float JumpSpeed = 5;
    public float SprintSpeed = 20;
    private float currentSpeed;
    public float Acceleration = 10f;
    public bool isFrozen = false;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip walkingClip;
    public AudioClip sprintingClip;

    private void OnEnable()
    {
        InputActions.FindActionMap("Player").Enable();
    }
    private void OnDisable()
    {
        InputActions.FindActionMap("Player").Disable();
    }
    private void Awake()
    {
        m_moveAction = InputSystem.actions.FindAction("move");
        m_lookAction = InputSystem.actions.FindAction("look");
        m_jumpAction = InputSystem.actions.FindAction("jump");
        m_sprintAction = InputSystem.actions.FindAction("sprint");
        m_rigidbody = GetComponent<Rigidbody>();

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        audioSource.loop = true;
    }
    public void SetFreeze(bool frozen)
    {
        isFrozen = frozen;
    }
    private void Update()
    {
        if (isFrozen == false)
        {
            m_moveAmt = m_moveAction.ReadValue<Vector2>();
            m_lookAmt = m_lookAction.ReadValue<Vector2>();
            if (m_jumpAction.WasPressedThisFrame())
            {
                Jump();
            }
        }
        HandleFootstepAudio();
    }
    public void Jump()
    {
        m_rigidbody.AddForce(Vector3.up * JumpSpeed, ForceMode.Impulse);
    }
    private void FixedUpdate()
    {
        float targetSpeed = m_sprintAction.IsPressed() ? SprintSpeed : WalkSpeed;
        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, Acceleration * Time.fixedDeltaTime);
        Walking();
    }
    private void Walking()
    {
        Vector3 moveDir =
        orientation.forward * m_moveAmt.y +
        orientation.right * m_moveAmt.x;
        float currentSpeed = m_sprintAction.IsPressed() ? SprintSpeed : WalkSpeed;
        m_rigidbody.MovePosition(m_rigidbody.position + moveDir.normalized * currentSpeed * Time.fixedDeltaTime);
    }
    private void HandleFootstepAudio()
    {
        bool isMoving = m_moveAmt.magnitude > 0.1f && !isFrozen;
        bool isSprinting = isMoving && m_sprintAction.IsPressed();

        if (!isMoving)
        {
            // Player is still — stop all footstep audio
            if (audioSource.isPlaying)
                audioSource.Stop();
            return;
        }

        AudioClip targetClip = isSprinting ? sprintingClip : walkingClip;

        // Only swap clip if it has changed, to avoid restarting the loop
        if (audioSource.clip != targetClip)
        {
            audioSource.clip = targetClip;
            audioSource.Play();
        }
        else if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
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

    [Header("Movement Settings")]
    public float WalkSpeed = 5;
    public float RotateSpeed = 5;
    public float SprintSpeed = 20;

    [Header("Jump Settings")]
    public float JumpSpeed = 5;
    public float jumpCooldown = 0.5f;
    private float lastJumpTime = -Mathf.Infinity;
    
    private float currentSpeed;
    public float Acceleration = 10f;

    public bool isFrozen = false;

    private Rigidbody rb;


    //bool to help with player movement sounds
    private bool wasMovingLastFrame = false;


    private void OnEnable()
    {
        // Find and enable input actions
        InputActions.FindActionMap("Player").Enable();
    }

    private void OnDisable()
    {
        // Find and disable input actions
        InputActions.FindActionMap("Player").Disable();
    }

    private void Awake()
    {
        // Assign input actions for Unity input manager
        m_moveAction = InputSystem.actions.FindAction("move");
        m_lookAction = InputSystem.actions.FindAction("look");
        m_jumpAction = InputSystem.actions.FindAction("jump");
        m_sprintAction = InputSystem.actions.FindAction("sprint");

        m_rigidbody = GetComponent<Rigidbody>();

        rb = GetComponent<Rigidbody>();
    }

    // Freezing functionality (SEE DIALOGUE AND KEYPAD MANAGER)
    public void SetFreeze(bool frozen)
    {
        isFrozen = frozen;
        if (frozen)
        {
            m_moveAmt = Vector2.zero;
            m_rigidbody.linearVelocity = Vector3.zero;
        }
    }

    private void Update()
    {
        // Freezing functionality (SEE DIALOGUE AND KEYPAD MANAGER)
        if (isFrozen == false)
        {
            m_moveAmt = m_moveAction.ReadValue<Vector2>();
            m_lookAmt = m_lookAction.ReadValue<Vector2>();

            if (m_jumpAction.WasPressedThisFrame() && Time.time >= lastJumpTime + jumpCooldown)
            {
                Jump();
            }
        }

    }

    public void Jump()
    {
        m_rigidbody.AddForce(Vector3.up * JumpSpeed, ForceMode.Impulse);
        lastJumpTime = Time.time;
    }

    // Speed and checks
    private void FixedUpdate()
    {
        // Checks for sprint action, adjusts speed if necessary
        float targetSpeed = m_sprintAction.IsPressed() ? SprintSpeed : WalkSpeed;
        // Current speed and acceleration by time
        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, Acceleration * Time.fixedDeltaTime);

        Walking();
        CheckFootsteps();
    }

    private void Walking()
    {
        Vector3 moveDir =
        orientation.forward * m_moveAmt.y +
        orientation.right * m_moveAmt.x;

        // Check if sprint is being held
        float currentSpeed = m_sprintAction.IsPressed() ? SprintSpeed : WalkSpeed;

        m_rigidbody.MovePosition(m_rigidbody.position + moveDir.normalized * currentSpeed * Time.fixedDeltaTime);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.contacts.Length > 0)
        {
            Vector3 normal = collision.contacts[0].normal;

            if (normal.y < 0.4f)
            {
                // Prevent downward velocity
                rb.linearVelocity = new Vector3(
                    rb.linearVelocity.x,
                    Mathf.Max(rb.linearVelocity.y),
                    rb.linearVelocity.z
                );
                Vector3 oppositeDir = -transform.forward;
                // Ignore vertical so backward movement stays on the ground
                oppositeDir.y = 0f;
                oppositeDir.Normalize();
                // Move the player backward
                rb.linearVelocity = new Vector3(
                    oppositeDir.x, 
                    oppositeDir.y, 
                    oppositeDir.z
                );
            }
        }
  
    }

    private void CheckFootsteps()
    {
        if (isFrozen)
        {
            PlayerAudio.Instance.StopFootsteps();
            return;
        }

        bool isMoving = m_moveAmt.magnitude > 0.1f;

        if (isMoving && !wasMovingLastFrame)
        {
            PlayerAudio.Instance.StartFootsteps();
        }
        else if (!isMoving && wasMovingLastFrame)
        {
            PlayerAudio.Instance.StopFootsteps();
        }

        wasMovingLastFrame = isMoving;
    }
}
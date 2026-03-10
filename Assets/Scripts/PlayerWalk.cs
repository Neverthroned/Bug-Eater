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

        // Check if sprint is being held
        float currentSpeed = m_sprintAction.IsPressed() ? SprintSpeed : WalkSpeed;

        m_rigidbody.MovePosition(m_rigidbody.position + moveDir.normalized * currentSpeed * Time.fixedDeltaTime);
    }
}

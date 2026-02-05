using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWalk : MonoBehaviour
{
    public InputActionAsset InputActions;
    public Transform orientation;

    private InputAction m_moveAction;
    private InputAction m_lookAction;
    private InputAction m_jumpAction;

    private Vector2 m_moveAmt;
    private Vector2 m_lookAmt;
    private Rigidbody m_rigidbody;

    public float WalkSpeed = 5;
    public float RotateSpeed = 5;
    public float JumpSpeed = 5;


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

        m_rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        m_moveAmt = m_moveAction.ReadValue<Vector2>();
        m_lookAmt = m_lookAction.ReadValue<Vector2>();

        if(m_jumpAction.WasPressedThisFrame())
        {
            Jump();
        }
    }

    public void Jump()
    {
        m_rigidbody.AddForceAtPosition(new Vector3(0, 5f, 0), Vector3.up, ForceMode.Impulse);
    }

    private void FixedUpdate()
    {
        Walking();
    }

    private void Walking()
    {
        Vector3 moveDir =
        orientation.forward * m_moveAmt.y +
        orientation.right * m_moveAmt.x;

        m_rigidbody.MovePosition(m_rigidbody.position + moveDir.normalized * WalkSpeed * Time.fixedDeltaTime);
    }
}

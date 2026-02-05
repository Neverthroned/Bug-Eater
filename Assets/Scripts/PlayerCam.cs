using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCam : MonoBehaviour
{
    public float sensX = 200f;
    public float sensY = 200f;

    public Transform orientation;
    public InputActionAsset inputActions;

    private InputAction lookAction;

    float yaw;
    float pitch;

    void Awake()
    {
        lookAction = inputActions.FindActionMap("Player").FindAction("look");
    }

    void OnEnable()
    {
        lookAction.Enable();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void OnDisable()
    {
        lookAction.Disable();
    }

    void Update()
    {
        Vector2 look = lookAction.ReadValue<Vector2>();

        yaw += look.x * sensX * Time.deltaTime;
        pitch -= look.y * sensY * Time.deltaTime;
        pitch = Mathf.Clamp(pitch, -89f, 89f);

        // Camera pitch + yaw
        transform.rotation = Quaternion.Euler(pitch, yaw, 0f);

        // Player yaw only
        orientation.rotation = Quaternion.Euler(0f, yaw, 0f);
    }
}
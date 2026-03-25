using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Interaction Settings")]
    public float interactDistance = 3f;
    public LayerMask interactLayer;

    [Header("UI")]
    public GameObject interactPromptUI;

    [Header("Input")]
    
    public InputActionAsset inputActions;
    private InputAction interactAction;

    private Camera cam;
    private Interactable currentInteractable;

    void Awake()
    {
        
        var map = inputActions.FindActionMap("Player", true);
        interactAction = map.FindAction("Interact", true);
    }

    // Interact button necessities
    void OnEnable()
    {
        interactAction.Enable();
        interactAction.started += OnInteract;
    }

    void OnDisable()
    {
        interactAction.started -= OnInteract;
        interactAction.Disable();
    }

    void Start()
    {
        cam = Camera.main;
        interactPromptUI.SetActive(false);
    }

    // Constant check for interactable (by association constant raycast)
    void Update()
    {
        CheckForInteractable();
    }

    // Racast function and interact UI
    void CheckForInteractable()
    {
        // Raycast
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance, interactLayer))
        {
            Interactable interactable = hit.collider.GetComponentInParent<Interactable>();
            if (interactable != null)
            {
                currentInteractable = interactable;
                interactPromptUI.SetActive(true);
                return;
            }
        }

        // If not in range (no raycast hit) set to off
        currentInteractable = null;
        interactPromptUI.SetActive(false);
    }

    private void OnInteract(InputAction.CallbackContext ctx)
    {
        // Input manager implementation (e to interact button functionality)
        Debug.Log("Interact input fired");
        currentInteractable?.Interact();
    }
}
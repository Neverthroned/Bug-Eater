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
    public TMP_Text promptText;

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

        Debug.Log(interactAction);
    }

    void Update()
    {
        CheckForInteractable();
    }

    void CheckForInteractable()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance, interactLayer))
        {
            Interactable interactable = hit.collider.GetComponentInParent<Interactable>();
            if (interactable != null)
            {
                currentInteractable = interactable;
                interactPromptUI.SetActive(true);
                promptText.text = interactable.GetPrompt();
                return;
            }
        }

        currentInteractable = null;
        interactPromptUI.SetActive(false);
    }

    private void OnInteract(InputAction.CallbackContext ctx)
    {
        Debug.Log("Interact input fired");
        currentInteractable?.Interact();
    }
}
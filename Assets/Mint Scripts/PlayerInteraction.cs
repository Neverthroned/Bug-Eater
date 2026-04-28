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

    //bool for determining when an inyteraction is happening
    private bool isInteracting = false;

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
    bool IsPlayerBusy()
    {
        DialogueManager dialogue = FindFirstObjectByType<DialogueManager>();
        KeypadManager keypad = FindFirstObjectByType<KeypadManager>();
        NarrativeManager narrative = FindFirstObjectByType<NarrativeManager>();

        if (dialogue != null && dialogue.IsOpen()) return true;
        if (keypad != null && keypad.IsOpen()) return true;
        if (narrative != null && narrative.IsOpen()) return true;

        return false;
    }

    // Racast function and interact UI
    void CheckForInteractable()
    {

        // Do not show prompt while interacting
        if (isInteracting || IsPlayerBusy())
        {
            interactPromptUI.SetActive(false);
            return;
        }

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

        if (currentInteractable == null)
        {
            EndInteraction();
            return;
        }
        // If not in range (no raycast hit) set to off
        currentInteractable = null;
        interactPromptUI.SetActive(false);
    }

    private void OnInteract(InputAction.CallbackContext ctx)
    {
        if (currentInteractable == null)
            return;

        isInteracting = true;
        interactPromptUI.SetActive(false);

        currentInteractable.Interact();

    }

    public void EndInteraction()
    {
        isInteracting = false;
        currentInteractable = null;
        interactPromptUI.SetActive(false);
    }

    

}
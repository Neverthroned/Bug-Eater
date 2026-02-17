using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    public float interactDistance = 3f;
    public LayerMask interactLayer;
    public GameObject interactPromptUI;
    public TMP_Text promptText; // or TMP_Text if using TextMeshPro

    private Camera cam;
    private IInteractable currentInteractable;

    void Start()
    {
        cam = Camera.main;
        interactPromptUI.SetActive(false);
    }

    void Update()
    {
        CheckForInteractable();

        if (Input.GetKeyDown(KeyCode.E) && currentInteractable != null)
        {
            currentInteractable.Interact();
        }
    }

    void CheckForInteractable()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactDistance, interactLayer))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

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
}

using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractableDetector : MonoBehaviour
{

    [SerializeField] private Camera m_Camera;

    private const float k_InteractableDetectionDistance = 4;
    [SerializeField] private float m_InteractableDetectionDistance = k_InteractableDetectionDistance;
    [SerializeField] private LayerMask m_InteractableDetectionLayer;
    private const float k_InteractableRadius = 2;
    [SerializeField] private float m_InteractableRadius = k_InteractableRadius;
    private const float k_InteractableDistance = 1;
    [SerializeField] private float m_InteractableDistance = k_InteractableDistance;


    [Header("Input Actions")]
    [SerializeField] private InputActionReference m_InteractAction;

    [SerializeField] private InputAction test;
    private Inventory m_Inventory;
    private IInteractable m_HitInteractable;
    private void Start()
    {
        m_InteractAction.action.performed += OnInteractPerformed;
        m_Inventory = GetComponent<Inventory>();
    }

    private void OnInteractPerformed(InputAction.CallbackContext obj)
    {
        if (m_HitInteractable != null)
        {
            m_HitInteractable.Interact(m_Inventory);
            if(m_HitInteractable.CanInteract())
                InteractionPrompt.Instance.UpdatePrompt("Press F to interact with " + m_HitInteractable.GetPromptName());
        }
    }

    private void Update()
    {
        var ray = new Ray(m_Camera.transform.position, m_Camera.transform.forward);
        if (Physics.SphereCast(ray, m_InteractableRadius, out RaycastHit hit, m_InteractableDetectionDistance, m_InteractableDetectionLayer))
        {
            Debug.Log(hit.collider.name);
            if (hit.distance > m_InteractableDistance)
            {
                InteractionPrompt.Instance.UpdatePrompt("Too far to interact");
                m_HitInteractable = null;
                return;
            }


            var interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != m_HitInteractable)
            {
                if (!interactable.CanInteract())
                {
                    InteractionPrompt.Instance.UpdatePrompt(interactable.GetPromptName() + " is locked.");
                }
                else
                {
                    InteractionPrompt.Instance.UpdatePrompt("Press F to interact with " + interactable.GetPromptName());
                }

                m_HitInteractable = interactable;
            }
        }
        else
        {
            InteractionPrompt.Instance.HidePrompt();
            m_HitInteractable = null;
        }
        Debug.DrawRay(ray.origin, ray.direction * m_InteractableDetectionDistance, Color.red);
    }
}

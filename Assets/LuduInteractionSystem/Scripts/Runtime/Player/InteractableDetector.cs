using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractableDetector : MonoBehaviour
{

    [SerializeField] private Camera m_Camera;
    [SerializeField] private float m_InteractableDetectionDistance = 4;
    [SerializeField] private LayerMask m_InteractableDetectionLayer;
    [SerializeField] private float m_InteractableRadius = 2;
    [SerializeField] private float m_InteractableDistance = 1;


    [Header("Input Actions")]
    [SerializeField] private InputActionReference m_InteractAction;

    private Inventory m_Inventory;
    private IInteractable m_HitInteractable;
    private string m_KeyName;

    private void Start()
    {
        m_Inventory = GetComponent<Inventory>();

    }

    private void OnEnable()
    {
        if (m_InteractAction != null && m_InteractAction.action != null)
        {
            m_InteractAction.action.Enable();
            m_InteractAction.action.performed += OnInteractPerformed;
            m_InteractAction.action.canceled += OnInteractCanceled;

            m_KeyName = m_InteractAction.action.GetBindingDisplayString();
        }
    }

    private void Update()
    {
        var ray = new Ray(m_Camera.transform.position, m_Camera.transform.forward);
        if (Physics.SphereCast(ray, m_InteractableRadius, out RaycastHit hit, m_InteractableDetectionDistance, m_InteractableDetectionLayer))
        {
            var interactable = hit.collider.GetComponent<IInteractable>();
            if (!interactable.CanInteract())
            {
                InteractionPrompt.Instance.HidePrompt();
                return;
            }

            Debug.Log(hit.collider.name);
            if (hit.distance > m_InteractableDistance)
            {
                InteractionPrompt.Instance.UpdatePrompt("Too far to interact");
                m_HitInteractable = null;
                return;
            }

            if (interactable != m_HitInteractable)
            {
                if (interactable.IsLocked())
                {
                    InteractionPrompt.Instance.UpdatePrompt(interactable.GetLockedPrompt());
                }
                else
                {
                    InteractionPrompt.Instance.UpdatePrompt(interactable.GetPrompt(m_KeyName));
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

    private void OnDisable()
    {
        if (m_InteractAction != null && m_InteractAction.action != null)
        {
            m_InteractAction.action.performed -= OnInteractPerformed;
            m_InteractAction.action.canceled -= OnInteractCanceled;
        }
    }

    private void OnInteractCanceled(InputAction.CallbackContext obj)
    {
        Debug.Log("InteractCanceled");
        if (m_HitInteractable != null)
        {
            m_HitInteractable.CancelInteract();
        }
        else
        {
            Debug.LogWarning("No interactable to cancel interaction with.", this);
        }
    }

    private void OnInteractPerformed(InputAction.CallbackContext obj)
    {
        if (m_HitInteractable != null)
        {
            m_HitInteractable.Interact(m_Inventory);
            if(!m_HitInteractable.IsLocked())
                InteractionPrompt.Instance.UpdatePrompt(m_HitInteractable.GetPrompt(m_KeyName));
        }
        else
        {
            Debug.LogWarning("No interactable to perform interaction with.", this);
        }
    }


}

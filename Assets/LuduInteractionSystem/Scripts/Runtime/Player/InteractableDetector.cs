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

    [Header("Input Actions")]
    [SerializeField] private InputActionReference m_InteractAction;

    private Inventory m_Inventory;

    private void Start()
    {
        m_InteractAction.action.performed += OnInteractPerformed;
        m_Inventory = GetComponent<Inventory>();
    }

    private void OnInteractPerformed(InputAction.CallbackContext obj)
    {
        var ray = new Ray(m_Camera.transform.position, m_Camera.transform.forward);
        if (Physics.SphereCast(ray, m_InteractableRadius, out RaycastHit hit, m_InteractableDetectionDistance, m_InteractableDetectionLayer))
        {
            Debug.Log("Interacted with: " + hit.collider.name);
            var interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactable.Interact(m_Inventory);
            }
        }
    }


    private void Update()
    {
        var ray = new Ray(m_Camera.transform.position, m_Camera.transform.forward);
        if (Physics.SphereCast(ray, m_InteractableRadius, out RaycastHit hit, m_InteractableDetectionDistance, m_InteractableDetectionLayer))
        {
            Debug.Log(hit.collider.name);
        }
        Debug.DrawRay(ray.origin, ray.direction * m_InteractableDetectionDistance, Color.red);
    }
}

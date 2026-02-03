using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractableDetector : MonoBehaviour
{

    [SerializeField] private Camera m_Camera;

    private const float k_InteractableDetectionDistance = 4;
    [SerializeField] private float m_InteractableDetectionDistance = k_InteractableDetectionDistance;
    [SerializeField] private LayerMask m_InteractableDetectionLayer;

    [Header("Input Actions")]
    [SerializeField] private InputActionReference m_InteractAction;

    private void Start()
    {
        m_InteractAction.action.performed += OnInteractPerformed;
    }

    private void OnInteractPerformed(InputAction.CallbackContext obj)
    {
        var ray = new Ray(m_Camera.transform.position, m_Camera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, m_InteractableDetectionDistance, m_InteractableDetectionLayer))
        {
            Debug.Log("Interacted with: " + hit.collider.name);
            var interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactable.Interact();
            }
        }
    }


    private void Update()
    {
        var ray = new Ray(m_Camera.transform.position, m_Camera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, k_InteractableDetectionDistance, m_InteractableDetectionLayer))
        {
            Debug.Log(hit.collider.name);
        }
        Debug.DrawRay(ray.origin, ray.direction * m_InteractableDetectionDistance, Color.red);
    }

}

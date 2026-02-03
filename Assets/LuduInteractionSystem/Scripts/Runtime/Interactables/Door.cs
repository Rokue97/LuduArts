using System;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform m_DoorPivotTransform;
    [SerializeField] private string m_ObjectName;
    [SerializeField] private bool m_IsLocked;
    [SerializeField] private float m_OpenAngle;
    [SerializeField] private float m_AnimationSpeed;
    [SerializeField] private bool m_IsInteractable;

    [Header("Locked Settings")]
    [SerializeField] private KeyData m_KeyData;

    private bool m_IsOpen;

    private void Start()
    {
        m_IsOpen = false;

        if(m_IsLocked && m_KeyData == null)
            Debug.LogWarning("Door is locked but no KeyData is assigned.", this);
    }

    private void Update()
    {
        m_DoorPivotTransform.localRotation = Quaternion.Euler(0, m_IsOpen ? Mathf.LerpAngle(m_DoorPivotTransform.localRotation.eulerAngles.y, m_OpenAngle, Time.deltaTime * m_AnimationSpeed) :
            Mathf.LerpAngle(m_DoorPivotTransform.localRotation.eulerAngles.y, 0, Time.deltaTime * m_AnimationSpeed), 0);
    }

    public void Interact(Inventory inventory)
    {
        Debug.Log("Interacted");

        if (m_IsLocked)
        {
            TryUnlockDoor(inventory);
            return;
        }

        if(m_IsOpen)
            CloseDoor();
        else
            OpenDoor();
    }

    public string GetPromptName()
    {
        return m_ObjectName;
    }

    public bool CanInteract()
    {
        return m_IsInteractable;
    }
    public bool IsLocked()
    {
        return m_IsLocked;
    }

    private void OpenDoor()
    {
        m_IsOpen = true;
    }

    private void CloseDoor()
    {
        m_IsOpen = false;
    }

    private void TryUnlockDoor(Inventory inventory)
    {
        if (inventory.HasKey(m_KeyData))
        {
            m_IsLocked = false;
        }
    }

}

using System;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform m_DoorPivotTransform;
    [SerializeField] private string m_ObjectName;
    [SerializeField] private bool m_IsLocked;

    [Header("Locked Settings")]
    [SerializeField] private KeyData m_KeyData;

    private bool m_IsOpen;

    private void Start()
    {
        if(m_IsLocked && m_KeyData == null)
            Debug.LogError("Door is locked but no KeyData is assigned.", this);
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
        return !m_IsLocked;
    }

    private void TryUnlockDoor(Inventory inventory)
    {
        if (inventory.HasKey(m_KeyData))
        {
            m_IsLocked = false;
        }
    }

    private void OpenDoor()
    {
        m_IsOpen = true;
        m_DoorPivotTransform.localRotation = Quaternion.Euler(0, 90, 0);
    }

    private void CloseDoor()
    {
        m_IsOpen = false;
        m_DoorPivotTransform.localRotation = Quaternion.Euler(0, 0, 0);
    }


}

using System;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{

    [SerializeField] private Transform m_DoorPivotTransform;
    [SerializeField] private KeyData m_KeyData;

    private bool m_IsOpen;

    public void Interact(Inventory inventory)
    {
        Debug.Log("Interacted");

        if(m_IsOpen)
            CloseDoor(inventory);
        else
            OpenDoor(inventory);
    }


    private void OpenDoor(Inventory inventory)
    {
        if (m_KeyData == null)
        {
            m_IsOpen = true;
            m_DoorPivotTransform.localRotation = Quaternion.Euler(0, 90, 0);
        }
        else
        {
            Debug.Log("Key required to open the door.");
            if (inventory.HasKey(m_KeyData))
            {
                m_IsOpen = true;
                m_DoorPivotTransform.localRotation = Quaternion.Euler(0, 90, 0);
            }
        }

    }

    private void CloseDoor(Inventory inventory)
    {
        m_IsOpen = false;
        m_DoorPivotTransform.localRotation = Quaternion.Euler(0, 0, 0);
    }


}

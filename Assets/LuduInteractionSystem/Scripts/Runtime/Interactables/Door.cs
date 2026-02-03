using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{

    [SerializeField] private Transform m_DoorPivotTransform;

    private bool m_IsOpen;
    public void Interact()
    {
        Debug.Log("Interacted");

        if(m_IsOpen)
            CloseDoor();
        else
            OpenDoor();
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

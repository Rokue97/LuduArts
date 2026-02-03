using UnityEngine;

public class Key : MonoBehaviour, IInteractable
{
    [SerializeField] private KeyData m_KeyData;

    public void Interact(Inventory inventory)
    {
        if (inventory != null)
        {
            inventory.AddKey(m_KeyData);
            Destroy(gameObject);
        }

        Debug.Log("Interacted with key: " + m_KeyData.KeyName);
    }
}

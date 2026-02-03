using UnityEngine;

public class Key : MonoBehaviour, IInteractable
{
    [SerializeField] private KeyData m_KeyData;
    [SerializeField] private string m_ObjectName;
    [SerializeField] private AudioClip m_PickUpSfx;
    public void Interact(Inventory inventory)
    {
        if (m_KeyData == null)
        {
            Debug.LogError($"{gameObject.name} Missing Key Data!");
            return;
        }

        if (inventory != null)
        {
            AudioManager.Instance.PlaySfx(m_PickUpSfx);
            inventory.AddKey(m_KeyData);
            Destroy(gameObject);
        }

        Debug.Log("Interacted with key: " + m_KeyData.KeyName);
    }

    public string GetPrompt(string keyName)
    {
        return $"Press {keyName} to pick up {m_ObjectName}";
    }
}

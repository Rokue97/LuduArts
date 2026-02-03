using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<KeyData> m_OwnedKeys = new List<KeyData>();

    public void AddKey(KeyData keyData)
    {
        m_OwnedKeys.Add(keyData);
        InventoryUI.Instance.AddItem(keyData);
    }

    public bool HasKey(KeyData keyData)
    {
        return m_OwnedKeys.Contains(keyData);
    }
}

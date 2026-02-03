using TMPro;
using UnityEditor;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance;

    [SerializeField] private GameObject m_InventoryParent;
    [SerializeField] private GameObject m_KeyInfoPrefab;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void AddItem(KeyData keyData)
    {
        var item = Instantiate(m_KeyInfoPrefab, m_InventoryParent.transform);
        item.GetComponent<TextMeshProUGUI>().text = keyData.KeyName;
    }
}

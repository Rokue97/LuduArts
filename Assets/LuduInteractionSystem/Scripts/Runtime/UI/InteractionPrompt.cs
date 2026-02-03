using TMPro;
using UnityEngine;

public class InteractionPrompt : MonoBehaviour
{
    public static InteractionPrompt Instance;

    [SerializeField] private TextMeshProUGUI m_PromptText;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void UpdatePrompt(string prompt)
    {
        m_PromptText.text = prompt;
    }

    public void HidePrompt()
    {
        m_PromptText.text = string.Empty;
    }
}

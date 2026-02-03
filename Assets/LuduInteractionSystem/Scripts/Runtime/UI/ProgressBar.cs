using System;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public static ProgressBar Instance;

    [SerializeField] private Slider m_ProgressSlider;

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

    public void SetProgress(float progress)
    {
        m_ProgressSlider.value = progress;
    }

    public void SetProgressActive(bool isActive)
    {
        m_ProgressSlider.gameObject.SetActive(isActive);
    }
}

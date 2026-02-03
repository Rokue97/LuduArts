using System;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform m_ChestPivotTransform;
    [SerializeField] private string m_ObjectName;
    [SerializeField] private float m_OpenAngle;
    [SerializeField] private float m_AnimationSpeed;
    [SerializeField] private float m_HoldDuration;

    private float m_CurrentTimer;
    private bool m_Holding;
    private bool m_IsOpen;

    private void Start()
    {
        m_CurrentTimer = 0;
        m_Holding = false;
        m_IsOpen = false;
    }

    private void Update()
    {
        if (m_Holding && !m_IsOpen)
        {
            m_CurrentTimer += Time.deltaTime;
            ProgressBar.Instance.SetProgress(m_CurrentTimer / m_HoldDuration);
            if (m_CurrentTimer >= m_HoldDuration)
            {
                OpenChest();
            }
        }

        if (m_IsOpen)
        {
            m_ChestPivotTransform.localRotation = Quaternion.Euler(Mathf.LerpAngle(m_ChestPivotTransform.localRotation.eulerAngles.x, m_OpenAngle, Time.deltaTime * m_AnimationSpeed), 0, 0);
        }
    }

    public void Interact(Inventory inventory)
    {
        m_Holding = true;
        ProgressBar.Instance.SetProgressActive(true);
    }

    public void CancelInteract()
    {
        m_Holding = false;
        m_CurrentTimer = 0;
        ProgressBar.Instance.SetProgressActive(false);
    }

    public string GetPromptName()
    {
        return m_ObjectName;
    }

    public bool CanInteract()
    {
        return !m_IsOpen;
    }

    private void OpenChest()
    {
        m_IsOpen = true;
        ProgressBar.Instance.SetProgressActive(false);
    }
}

using System;
using UnityEngine;
using UnityEngine.Events;

public class Switch : MonoBehaviour, IInteractable
{
    public UnityEvent OnSwitchOn;
    public UnityEvent OnSwitchOff;
    [SerializeField] private Transform m_SwitchPivotTransform;
    [SerializeField] private string m_ObjectName;
    [SerializeField] private float m_ActiveAngle;
    [SerializeField] private float m_AnimationSpeed;

    private bool m_IsOn;

    private void Start()
    {
        m_IsOn = false;
    }

    private void Update()
    {
        m_SwitchPivotTransform.localRotation = Quaternion.Euler(m_IsOn ? Mathf.LerpAngle(m_SwitchPivotTransform.localRotation.eulerAngles.x, m_ActiveAngle, Time.deltaTime * m_AnimationSpeed) :
            Mathf.LerpAngle(m_SwitchPivotTransform.localRotation.eulerAngles.x, -m_ActiveAngle, Time.deltaTime * m_AnimationSpeed), 0, 0);
    }

    public void Interact(Inventory inventory)
    {
        m_IsOn = !m_IsOn;

        if(m_IsOn)
            OnSwitchOn.Invoke();
        else
            OnSwitchOff.Invoke();
    }

    public string GetPrompt(string keyName)
    {
        if (m_IsOn)
        {
            return $"Press {keyName} to deactivate {m_ObjectName}";
        }
        return $"Press {keyName} to activate {m_ObjectName}";
    }

}

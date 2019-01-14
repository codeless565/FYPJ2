using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillNoiseProp : QPropertiesBase{
    public static string m_propertyname;
    private bool m_isCompleted;
    private bool m_isActive;
    private float m_currentValue;
    private float m_completeValue;

    public KillNoiseProp(bool _active, float _completevalue)
    {
        m_propertyname = "KillNoise";
        m_isCompleted = false;
        m_isActive = _active;
        m_currentValue = 0.0f;
        m_completeValue = _completevalue;
    }

    public string PropertyName
    {
        get
        {
            return m_propertyname;
        }
    }

    public bool IsCompleted { get { return m_isCompleted; } set { m_isCompleted = value; } }
    public bool IsActive { get { return m_isActive; } set { m_isActive = value; } }
    public float CurrentValue { get { return m_currentValue; } set { m_currentValue = value; } }
    public float CompleteValue { get { return m_completeValue; } set { m_completeValue = value; } }

    public void AddCurrentValue(float _amount)
    {
        CurrentValue += _amount;
    }
    public void SetCurrentValue(float _amount)
    {
        CurrentValue = _amount;
    }

    public void Update()
    {
        Debug.Log("KillNoiseProp: " + m_currentValue + "/" + m_completeValue);
        if (m_currentValue >= m_completeValue)
            m_isCompleted = true;
    }
}

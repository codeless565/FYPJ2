using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EntityHealthBar : MonoBehaviour
{
    [SerializeField]
    Slider HealthSlider;

    IEntity m_Owner;

    private void Start()
    {
        m_Owner = GetComponent<IEntity>();
        HealthSlider.value = m_Owner.GetStats().HP / m_Owner.GetStats().MaxHP;
    }

    private void Update()
    {
        HealthSlider.value = m_Owner.GetStats().HP / m_Owner.GetStats().MaxHP;
    }

}

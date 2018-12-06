using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debuff_Slow : IBuff
{
    //Default stuff
    bool m_active;
    Sprite m_buffSprite;
    GameObject m_Target;

    //Buff members
    float m_timer;
    float m_deletedMoveSpeed;

    public void Init(GameObject _target)
    {
        m_Target = _target;
        m_active = true;
        //m_buffSprite = (Sprite)Resources.Load("Buffs/Slow");

        m_timer = 5.0f;
        if (m_Target.GetComponent<CStats>() != null)
        {
            m_deletedMoveSpeed = m_Target.GetComponent<CStats>().MoveSpeed * 0.3f;
            m_Target.GetComponent<CStats>().MoveSpeed -= m_deletedMoveSpeed;
        }
    }

    public void UpdateBuff()
    {
        if (!m_active)
            return;

        m_timer -= Time.deltaTime;
        if (m_timer <= 0)
        {
            DestoryBuff();
            m_active = false;
        }
    }

    public void DestoryBuff()
    {
        m_Target.GetComponent<CStats>().MoveSpeed += m_deletedMoveSpeed;
    }

    public void ResetBuff()
    {
        m_timer = 5.0f;
    }

    public bool Active
    { get { return m_active; } }

    public bool isDebuff
    { get { return true; } }

    public string BuffName
    { get { return "Slow Motion"; } }

    public Sprite BuffSprite
    { get { return m_buffSprite; } }

    public string BuffDescription
    { get { return "Slows target's movement speed by 30% for 5 seconds."; } }

}

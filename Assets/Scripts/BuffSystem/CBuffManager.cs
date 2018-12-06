using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBuffManager
{
    GameObject m_master;
    Dictionary<string, IBuff> m_buffList;

    public void Init(GameObject _master)
    {
        m_master = _master;
        m_buffList = new Dictionary<string, IBuff>();
    }

    public void Update()
    {
        foreach (IBuff buff in m_buffList.Values)
        {
            buff.UpdateBuff();

            if (!buff.Active)
                m_buffList.Remove(buff.BuffName);
        }
    }

    public void AddBuff(IBuff _newbuff)
    {
        // if buff alrdy exist in list
        if (m_buffList.ContainsKey(_newbuff.BuffName))
        {
            m_buffList[_newbuff.BuffName].ResetBuff();
            return;
        }

        // if buff is not in the list
        m_buffList.Add(_newbuff.BuffName, _newbuff);
    }

    //Clear only positive buffs
    public void ClearAllBuffs()
    {
        foreach (IBuff buff in m_buffList.Values)
        {
            if (!buff.isDebuff)
                m_buffList.Remove(buff.BuffName);
        }
    }

    //Clear only negative buffs
    public void ClearAllDebuffs()
    {
        foreach (IBuff buff in m_buffList.Values)
        {
            if (buff.isDebuff)
                m_buffList.Remove(buff.BuffName);
        }
    }

    //Clear all types of buffs
    public void ClearEveryBuff()
    {
        m_buffList.Clear();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPPotion : MonoBehaviour, IItem
{
    Sprite m_ItemSprite;

    void Awake()
    {
        m_ItemSprite = GetComponent<SpriteRenderer>().sprite;
    }

    public bool UseItem(ref CStats _playerStats)
    {
        //if (_playerStats.HP > _PlayerStats.MaxHP)
        //  return false;
        _playerStats.HP += 10;
        return true;
    }

    public string ItemKey
    {
        get
        {
            return "HPPOT";
        }
    }

    public string ItemName
    {
        get
        {
            return "Health Potion";
        }
    }

    public string Description
    {
        get
        {
            return "Recovers 10 HP upon use.";
        }
    }

    public Sprite ItemSprite
    {
        get
        {
            return m_ItemSprite;
        }
    }
}

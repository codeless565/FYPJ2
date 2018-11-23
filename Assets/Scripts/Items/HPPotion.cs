﻿using System.Collections;
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
        if (_playerStats.HP >= _playerStats.MaxHP)
            return false;

        _playerStats.HP += _playerStats.MaxHP * 0.5f;

        if (_playerStats.HP > _playerStats.MaxHP)
            _playerStats.HP = _playerStats.MaxHP;

        Debug.Log("Player hp: " + _playerStats.HP + " Max: " + _playerStats.MaxHP);
        return true;
    }

    public string ItemKey
    {
        get
        {
            return "HPPOTION";
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
            return "Recovers 50% of your max health upon use.";
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

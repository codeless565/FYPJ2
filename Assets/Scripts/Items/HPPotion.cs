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

    public bool UseItem(CPlayer _player)
    {
        if (_player.GetStats().HP >= _player.GetStats().MaxHP)
            return false;

        _player.UIScript.AddHealth(_player.GetStats().HP, _player.GetStats().MaxHP * 0.5f);

        if (_player.GetStats().HP > _player.GetStats().MaxHP)
            _player.GetStats().HP = _player.GetStats().MaxHP;
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

    public ItemType ItemType
    {
        get { return ItemType.Use; }
    }

    public Sprite ItemSprite
    {
        get
        {
            if (m_ItemSprite == null)
                m_ItemSprite = GetComponent<SpriteRenderer>().sprite;
            return m_ItemSprite;
        }
    }

    private void OnTriggerEnter2D(Collider2D _other)
    {
        CPlayer player = _other.GetComponent<CPlayer>();
        if (player != null)
        {
            if (player.m_InventorySystem.AddItem(this))
                Destroy(gameObject);
        }
    }
}

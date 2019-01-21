using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPElixir : MonoBehaviour, IItem
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

        _playerStats.HP = _playerStats.MaxHP;

        Debug.Log("Player hp: " + _playerStats.HP + " Max: " + _playerStats.MaxHP);
        return true;
    }

    public string ItemKey
    {
        get
        {
            return "HPELIXIR";
        }
    }

    public string ItemName
    {
        get
        {
            return "Health Elixir";
        }
    }

    public string Description
    {
        get
        {
            return "Fully recovers your health upon use.";
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

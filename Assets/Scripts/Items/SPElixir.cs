using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPElixir : MonoBehaviour, IItem
{
    Sprite m_ItemSprite;

    void Awake()
    {
        m_ItemSprite = GetComponent<SpriteRenderer>().sprite;
    }

    public bool UseItem(CPlayer _player)
    {
        if (_player.GetStats().SP >= _player.GetStats().MaxSP)
            return false;

        _player.UIScript.AddSP(_player.GetStats().SP, _player.GetStats().MaxSP);

        if (_player.GetStats().SP > _player.GetStats().MaxSP)
            _player.GetStats().SP = _player.GetStats().MaxSP;
        return true;
    }

    public string ItemKey
    {
        get
        {
            return "SPELIXIR";
        }
    }

    public string ItemName
    {
        get
        {
            return "Spirit Elixir";
        }
    }

    public string Description
    {
        get
        {
            return "Fully recover your spirit upon use.";
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

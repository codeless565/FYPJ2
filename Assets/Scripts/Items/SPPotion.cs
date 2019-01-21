using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPPotion : MonoBehaviour, IItem
{
    Sprite m_ItemSprite;

    void Awake()
    {
        m_ItemSprite = GetComponent<SpriteRenderer>().sprite;
    }

    public bool UseItem(ref CStats _playerStats)
    {
        if (_playerStats.SP >= _playerStats.MaxSP)
            return false;

        _playerStats.SP += _playerStats.MaxSP * 0.5f;

        if (_playerStats.SP > _playerStats.MaxSP)
            _playerStats.SP = _playerStats.MaxSP;

        Debug.Log("Player sp: " + _playerStats.SP + " Max: " + _playerStats.MaxSP);
        return true;
    }

    public string ItemKey
    {
        get
        {
            return "SPPOTION";
        }
    }

    public string ItemName
    {
        get
        {
            return "Spirit Potion";
        }
    }

    public string Description
    {
        get
        {
            return "Recovers 50% of your max spirit upon use.";
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

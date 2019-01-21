using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReviveTicket : MonoBehaviour, IItem
{
    Sprite m_ItemSprite;

    void Awake()
    {
        m_ItemSprite = GetComponent<SpriteRenderer>().sprite;
    }

    public bool UseItem(ref CStats _playerStats)
    {
        Debug.Log("Revive item used!");
        return true;
    }

    public string ItemKey
    {
        get
        {
            return "REVIVETIX";
        }
    }

    public string ItemName
    {
        get
        {
            return "Revival Amulet";
        }
    }

    public string Description
    {
        get
        {
            return "Allow you to revive upon taking fatal damage.";
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

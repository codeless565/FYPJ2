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

    public bool UseItem(ref CStats _playerStats)
    {
        if (_playerStats.SP >= _playerStats.MaxSP)
            return false;

        _playerStats.SP = _playerStats.MaxSP;

        Debug.Log("Player sp: " + _playerStats.SP + " Max: " + _playerStats.MaxSP);
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

    public Sprite ItemSprite
    {
        get
        {
            if (m_ItemSprite == null)
                m_ItemSprite = GetComponent<SpriteRenderer>().sprite;
            return m_ItemSprite;
        }
    }
}

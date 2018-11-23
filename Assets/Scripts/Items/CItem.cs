using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CItem
{
    string m_ItemName;
    Sprite m_ItemSprite;

    bool m_IsStackable;
    bool m_isBossDrop;
    bool m_InShop;
    int m_Quantity;


    public CItem(string _itemname, bool _isstackable, bool _isbossdrop, bool _isinshop)
    {
        m_ItemName = _itemname;
        m_IsStackable = _isstackable;
        m_isBossDrop = _isbossdrop;
        m_InShop = _isinshop;
        m_Quantity = 0;
    }


    public string ItemName
    {
        get { return m_ItemName; }
        set { m_ItemName = value; }
    }

    public Sprite ItemSprite
    {
        get { return m_ItemSprite; }
        set { m_ItemSprite = value; }
    }
    public bool IsStackable
    {
        get { return m_IsStackable; }
        set { m_IsStackable = value; }
    }
    public bool IsBossDrop
    {
        get { return m_isBossDrop; }
        set { m_isBossDrop = value; }
    }
    public bool IsInShop
    {
        get { return m_InShop; }
        set { m_InShop = value; }
    }
    public int Quantity
    {
        get { return m_Quantity; }
        set { m_Quantity = value; }
    }

    //public void PlayerUseItem(GameObject _player, string _itemname)
    //{
    //    if (!_player.GetComponent<CPlayer>().m_ItemDictonary.ContainsKey(_itemname))
    //    {
    //        //Debug.Log("NO ITEM");
    //        return; // TODO user does not have item
    //    }
    //    switch (_itemname)
    //    {
    //        case "HP_POTION":
    //            _player.GetComponent<CPlayer>().GetStats().HP += 100000;
    //            UpdateItemQuantity(_player, _itemname);
    //            break;
    //            // TODO the other items :')
    //    }
    //}

    //public void UpdateItemQuantity(GameObject _player, string _itemname)
    //{
    //    if (_player.GetComponent<CPlayer>().m_ItemDictonary.ContainsKey(_itemname))

    //        if (_player.GetComponent<CPlayer>().m_ItemDictonary[_itemname].Quantity >= 1)
    //        {
    //            _player.GetComponent<CPlayer>().m_ItemDictonary[_itemname].Quantity -= 1;
    //            if (_player.GetComponent<CPlayer>().m_ItemDictonary[_itemname].Quantity == 0)
    //                _player.GetComponent<CPlayer>().m_ItemDictonary.Remove(_itemname);
    //        }
    //}
}

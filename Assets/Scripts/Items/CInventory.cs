using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CInventory
{
    private int m_Notes;    //Regular Currency
    private int m_Gems;     //Premium Currency
    private List<CItemSlot> m_Items;

    public CInventory()
    {
        m_Notes = 0;
        m_Gems = 0;
        m_Items = new List<CItemSlot>();
    }

    public void AddItem(IItem _newItem, int _quantity = 1)
    {
        foreach (CItemSlot slot in m_Items)
        {
            if (slot.itemInfo.ItemKey == _newItem.ItemKey)
            {
                slot.quantity += _quantity;
                Debug.Log(_newItem.ItemKey + "'s quantity is " + slot.quantity);
                return;
            }
        }

        CItemSlot newItem = new CItemSlot(_newItem, _quantity);
        m_Items.Add(newItem);
        Debug.Log(_newItem.ItemKey + " has been added to inventory, quantity is " + m_Items[m_Items.Count - 1].quantity);
    }

    public bool RemoveItem(string _itemKey, int _quantity = 1)
    {
        foreach (CItemSlot slot in m_Items)
        {
            if (slot.itemInfo.ItemKey == _itemKey)
            {
                if (slot.quantity - _quantity < 0)
                {
                    Debug.Log(_itemKey + "'s quantity is < 0");
                    return false;
                }

                slot.quantity -= _quantity;
                Debug.Log(_itemKey + "'s quantity is now " + slot.quantity);
                if (slot.quantity <= 0)
                {
                    m_Items.Remove(slot);
                    Debug.Log(_itemKey + "'s quantity is removed from inventory");
                }
                return true;
            }
        }

        Debug.Log(_itemKey + "does not exist in inventory");
        return false;
    }

    public IItem GetItem(string _itemKey)
    {
        foreach (CItemSlot slot in m_Items)
        {
            if (slot.itemInfo.ItemKey == _itemKey)
            {
                return slot.itemInfo;
            }
        }

        return null;
    }

    //Set _value to negative if subtracting value
    public void AddNotes(int _value)
    {
        if (m_Notes + _value > int.MaxValue)
        {
            m_Notes = int.MaxValue;
            return;
        }
        if (m_Notes + _value < 0)
        {
            m_Notes = 0;
            return;
        }

        m_Notes += _value;
    }

    public int Notes
    {
        get { return m_Notes; }
    }

    //Set _value to negative if subtracting value
    public void AddGems(int _value)
    {
        if (m_Gems + _value > int.MaxValue)
        {
            m_Gems = int.MaxValue;
            return;
        }
        if (m_Gems + _value < 0)
        {
            m_Gems = 0;
            return;
        }

        m_Gems += _value;
    }

    public int Gems
    {
        get { return m_Gems; }
    }

    /*
     * HELPER FUNCTION
     */
    public void DebugLogAll()
    {
        int i = 0;
        foreach (var items in m_Items)
        {
            i++;
            Debug.Log("Slot " + i + " : Key - " + items.itemInfo.ItemKey + ", ItemName - " + items.itemInfo.ItemName + ", Quantity - " + items.quantity);
        }

        Debug.Log("Currencies: Notes - " + m_Notes + " Gems - " + m_Gems);
    }
}

public class CItemSlot
{
    public int quantity;
    public IItem itemInfo;

    public CItemSlot()
    {
        quantity = 0;
        itemInfo = null;
    }

    public CItemSlot(IItem _item, int _quantity)
    {
        quantity = _quantity;
        itemInfo = _item;
    }
}

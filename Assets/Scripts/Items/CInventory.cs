using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CInventory
{
    private Dictionary<string, IItem> m_Items;
    private Dictionary<string, int> m_ItemQuantity;

    public CInventory()
    {
        m_Items = new Dictionary<string, IItem>();
        m_ItemQuantity = new Dictionary<string, int>();
    }

    public void AddItem(IItem _newItem, int _quantity = 1)
    {
        if (m_Items.ContainsValue(_newItem))
        {
            m_ItemQuantity[_newItem.ItemKey] += _quantity;
            Debug.Log(_newItem.ItemKey + "'s quantity is " + m_ItemQuantity[_newItem.ItemKey]);
        }
        else
        {
            m_Items.Add(_newItem.ItemKey, _newItem);
            m_ItemQuantity.Add(_newItem.ItemKey, _quantity);
            Debug.Log(_newItem.ItemKey + " has been added to inventory, quantity is " + m_ItemQuantity[_newItem.ItemKey]);
        }
    }

    public bool RemoveItem(string _itemKey, int _quantity = 1)
    {
        if (m_ItemQuantity.ContainsKey(_itemKey))
        {
            if (m_ItemQuantity[_itemKey] - _quantity < 0)
            {
                Debug.Log(_itemKey + "'s quantity is < 0");
                return false;
            }

            m_ItemQuantity[_itemKey] -= _quantity;
            Debug.Log(_itemKey + "'s quantity is now " + m_ItemQuantity[_itemKey]);
            if (m_ItemQuantity[_itemKey] <= 0)
            {
                m_ItemQuantity.Remove(_itemKey);
                m_Items.Remove(_itemKey);
                Debug.Log(_itemKey + "'s quantity is removed from inventory");
            }
            return true;
        }

        Debug.Log(_itemKey + "does not exist in inventory");
        return false;
    }

    public IItem GetItem(string _itemKey)
    {
        if (m_Items.ContainsKey(_itemKey))
            return m_Items[_itemKey];
        else
            return null;
    }
}

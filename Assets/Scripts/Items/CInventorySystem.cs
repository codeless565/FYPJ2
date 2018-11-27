using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CInventorySystem
{
    private int m_Notes;    //Regular Currency
    private int m_Gems;     //Premium Currency
    private List<CItemSlot> m_Items;
    private CInventorySlots m_HotBar;
    private CInventory m_Inventory;

    public CInventorySystem()
    {
        m_Notes = 0;
        m_Gems = 0;
        m_Items = new List<CItemSlot>();
        m_HotBar = null;
        m_Inventory = null;
    }

    public CInventorySystem(CInventorySlots _HotbarScript, CInventory _InventoryScript)
    {
        m_Notes = 0;
        m_Gems = 0;
        m_Items = new List<CItemSlot>();
        m_HotBar = _HotbarScript;
        m_HotBar.Init();
        m_Inventory = _InventoryScript;
        m_Inventory.Init();
    }

    //returning false means bag for that itemType is full
    public bool AddItem(IItem _newItem, int _quantity = 1)
    {
        CItemSlot newItem = new CItemSlot(_newItem, _quantity);
        
        //Bag is full / Item exists in slot
        switch (_newItem.ItemType)
        {
            case ItemType.Use:
                if (m_Inventory.UseTabFull(newItem))
                    return false;
                break;
            case ItemType.Equip:
                if (m_Inventory.EquipTabFull(newItem))
                    return false;
                break;
        }

        //New item can be added
        foreach (CItemSlot slot in m_Items)
        {
            if (slot.itemInfo.ItemKey == _newItem.ItemKey)
            {
                slot.quantity += _quantity;
                AddItem2Itembar(slot);
                Debug.Log(_newItem.ItemKey + "'s quantity is " + slot.quantity);
                return true;
            }
        }

        //Create new ItemSlot with info to add to inventory
        AddItem2Inventory(newItem);

        if (_newItem.ItemType == ItemType.Use)
            AddItem2Itembar(newItem);

        //Add to universal List
        m_Items.Add(newItem);
        Debug.Log(_newItem.ItemKey + " has been added to inventory, quantity is " + m_Items[m_Items.Count - 1].quantity);

        return true;
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

    private void AddItem2Itembar(CItemSlot _newItem)
    {
        m_HotBar.AddItem(_newItem);
    }

    private bool AddItem2Inventory(CItemSlot _newItem)
    {
        return m_Inventory.AddItem(_newItem);
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

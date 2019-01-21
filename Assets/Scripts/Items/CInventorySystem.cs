using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CInventorySystem
{
    private int m_Notes;    //Regular Currency
    private int m_Gems;     //Premium Currency
    private Dictionary<string, CItemSlot> m_Items;
    private CInventorySlots m_HotBar;
    private CInventory m_Inventory;

    public CInventorySystem()
    {
        m_Notes = 0;
        m_Gems = 0;
        m_Items = new Dictionary<string, CItemSlot>();
        m_HotBar = null;
        m_Inventory = null;
    }

    public CInventorySystem(CInventorySlots _HotbarScript, CInventory _InventoryScript)
    {
        m_Notes = 0;
        m_Gems = 0;
        m_Items = new Dictionary<string, CItemSlot>();
        m_HotBar = _HotbarScript;
        m_Inventory = _InventoryScript;
    }

    public void Init(int _notes, int _gems, int _HPRations, int _HPPot, int _HPElix, int _SPPot, int _SPElix, int _ReviveTix)
    {
        m_Inventory.UpdateNoteText(m_Notes = _notes);
        m_Inventory.UpdateGemText(m_Gems = _gems);

        m_Items.Add(CItemDatabase.Instance.HPRation.GetComponent<IItem>().ItemKey, new CItemSlot(CItemDatabase.Instance.HPRation.GetComponent<IItem>(), _HPRations));
        m_Items.Add(CItemDatabase.Instance.HPPotion.GetComponent<IItem>().ItemKey, new CItemSlot(CItemDatabase.Instance.HPPotion.GetComponent<IItem>(), _HPPot));
        m_Items.Add(CItemDatabase.Instance.HPElixir.GetComponent<IItem>().ItemKey, new CItemSlot(CItemDatabase.Instance.HPElixir.GetComponent<IItem>(), _HPElix));
        m_Items.Add(CItemDatabase.Instance.SPPotion.GetComponent<IItem>().ItemKey, new CItemSlot(CItemDatabase.Instance.SPPotion.GetComponent<IItem>(), _SPPot));
        m_Items.Add(CItemDatabase.Instance.SPElixir.GetComponent<IItem>().ItemKey, new CItemSlot(CItemDatabase.Instance.SPElixir.GetComponent<IItem>(), _SPElix));
        m_Items.Add(CItemDatabase.Instance.ReviveTix.GetComponent<IItem>().ItemKey, new CItemSlot(CItemDatabase.Instance.ReviveTix.GetComponent<IItem>(), _ReviveTix));

        m_HotBar.Init(m_Items);
        m_Inventory.Init(m_Items);
    }

    //returning false means bag for that itemType is full
    public bool AddItem(IItem _newItem, int _quantity = 1)
    {
        if (m_Items.ContainsKey(_newItem.ItemKey))
            m_Items[_newItem.ItemKey].quantity += _quantity;
        else
            return false;

        return true;
    }

    public bool RemoveItem(string _itemKey, int _quantity = 1)
    {
        if (m_Items.ContainsKey(_itemKey))
            m_Items[_itemKey].quantity -= _quantity;
        else
            return false;

        return true;
    }

    public IItem GetItem(string _itemKey)
    {
        if (m_Items.ContainsKey(_itemKey))
        {
            if (m_Items[_itemKey].quantity > 0)
                return m_Items[_itemKey].itemInfo;
        }
        return null;
    }

    //Set _value to negative if subtracting value
    public void AddNotes(int _value)
    {
        if (m_Notes + _value > int.MaxValue - 1)
            m_Notes = int.MaxValue - 1;
        else if (m_Notes + _value < 0)
            m_Notes = 0;
        else
            m_Notes += _value;

        m_Inventory.UpdateNoteText(m_Notes);
    }

    public int Notes
    {
        get { return m_Notes; }
    }

    //Set _value to negative if subtracting value
    public void AddGems(int _value)
    {
        if (m_Gems + _value > int.MaxValue - 1)
            m_Gems = int.MaxValue - 1;
        else if (m_Gems + _value < 0)
            m_Gems = 0;
        else
            m_Gems += _value;

        m_Inventory.UpdateGemText(m_Gems);
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
        foreach (var items in m_Items.Values)
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

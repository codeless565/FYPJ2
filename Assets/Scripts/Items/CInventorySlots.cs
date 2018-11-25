using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CInventorySlots : MonoBehaviour {

    public int InventorySlots = 3;
    GameObject[] itemslots;
    
    public void Start()
    {
        itemslots = new GameObject[InventorySlots];
        for (int i = 0; i < InventorySlots; i++)
        {
            itemslots[i] = gameObject.transform.GetChild(i).transform.GetChild(0).gameObject;
            Debug.Log(itemslots[i]);
        }
    }

    public bool AddItem(CItemSlot _newItem)
    {
        bool skipSecondCheck = false;
        int i = 0;
        CSlotInfo emptySlot = null;

        foreach (GameObject slot in itemslots)
        {
            i++;
            CSlotInfo currSlot = slot.transform.GetComponent<CSlotInfo>();
            if (currSlot.isEmpty())
            {
                emptySlot = currSlot;
                break;
            }
            if (currSlot.isSameItem(_newItem))
                return false;
            if (i >= itemslots.Length)
                skipSecondCheck = true;
        }

        if (!skipSecondCheck)
            foreach (GameObject slot in itemslots)
            {
                CSlotInfo currSlot = slot.GetComponent<CSlotInfo>();
                if (currSlot.isEmpty())
                    continue;
                if (currSlot.isSameItem(_newItem))
                    return false;
            }

        if (emptySlot != null)
        {
            emptySlot.SetItemSlot(_newItem);
            Debug.Log(_newItem.itemInfo.ItemName + " added to Hotbar.");
            return true;
        }

        Debug.Log("Hotbar full");
        return false;
    }
}

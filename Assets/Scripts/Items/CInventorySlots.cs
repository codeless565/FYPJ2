using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CInventorySlots : MonoBehaviour {

    public int InventorySlots = 3;

    GameObject[] m_Itemslots;

    public void Init()
    {
        m_Itemslots = new GameObject[InventorySlots];
        for (int i = 0; i < InventorySlots; i++)
        {
            m_Itemslots[i] = gameObject.transform.GetChild(i).transform.GetChild(0).gameObject;
            Debug.Log(m_Itemslots[i]);
        }
    }

    public bool AddItem(CItemSlot _newItem)
    {
        bool skipSecondCheck = false;
        int i = 0;
        CSlotInfo emptySlot = null;

        // Find an empty slot in the inventory/hotbar
        foreach (GameObject slot in m_Itemslots)
        {
            i++;
            CSlotInfo currSlot = slot.transform.GetComponent<CSlotInfo>();
            if (currSlot.isEmpty())
            {
                emptySlot = currSlot;
                break;
            }
            if (currSlot.isSameItem(_newItem))
            {
                Debug.Log("CInventorySlots - Item Exists!");
                return false;
            }
            if (i >= m_Itemslots.Length)
                skipSecondCheck = true;
        }

        // Check another time to see if the same item exist after empty slots
        if (!skipSecondCheck)
            foreach (GameObject slot in m_Itemslots)
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
            return true;
        }

        return false;
    }

    public bool isFull()
    {
        foreach (GameObject slot in m_Itemslots)
        {
            CSlotInfo currSlot = slot.GetComponent<CSlotInfo>();
            if (currSlot.isEmpty())
                return false;
        }
        return true;
    }

    public bool isFull(CItemSlot _newItem)
    {
        foreach (GameObject slot in m_Itemslots)
        {
            CSlotInfo currSlot = slot.GetComponent<CSlotInfo>();
            if (currSlot.isEmpty())
                return false;
            if (currSlot.isSameItem(_newItem))
                return false;
        }
        return true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CInventory : MonoBehaviour {

    GameObject m_UseTab;
    GameObject m_EquipTab;

    GameObject[] m_currency;

	// Use this for initialization
	public void Init()
    {
        m_UseTab = transform.GetChild(0).gameObject;
        m_UseTab.GetComponent<CInventorySlots>().Init();
        m_EquipTab = transform.GetChild(1).gameObject;
        m_EquipTab.GetComponent<CInventorySlots>().Init();

        Debug.Log("UseTab: " + m_UseTab.name + "  EqTab: " + m_EquipTab.name);

        int currencyNum = transform.GetChild(2).childCount;
        m_currency = new GameObject[currencyNum];
        for (int i = 0; i < currencyNum; i++)
        {
            m_currency[i] = transform.GetChild(2).GetChild(i).GetChild(0).gameObject;
        }

        gameObject.SetActive(false);
        m_UseTab.SetActive(true);
        m_EquipTab.SetActive(false);
    }

    public bool AddItem(CItemSlot _newItem)
    {
        switch(_newItem.itemInfo.ItemType)
        {
            case ItemType.Use:
                m_UseTab.GetComponent<CInventorySlots>().AddItem(_newItem);
                break;
            case ItemType.Equip:
                m_EquipTab.GetComponent<CInventorySlots>().AddItem(_newItem);
                break;
        }

        //No Matching types to tabs
        return false;
    }

    public GameObject NoteText
    {
        get { return m_currency[0]; }
    }

    public GameObject GemText
    {
        get { return m_currency[1]; }
    }

    public bool UseTabFull(CItemSlot _newItem)
    {
        return m_UseTab.GetComponent<CInventorySlots>().isFull(_newItem);
    }

    public bool EquipTabFull(CItemSlot _newItem)
    {
        return m_EquipTab.GetComponent<CInventorySlots>().isFull(_newItem);
    }

    #region TabChanger
    public void CloseInventoryTab()
    { gameObject.SetActive(false); }

    public void OpenInventoryTab()
    { gameObject.SetActive(true); }

    public void SwitchtoUseTab()
    {
        m_UseTab.SetActive(true);
        m_EquipTab.SetActive(false);
    }

    public void SwitchtoEquipTab()
    {
        m_UseTab.SetActive(false);
        m_EquipTab.SetActive(true);
    }

    #endregion
}

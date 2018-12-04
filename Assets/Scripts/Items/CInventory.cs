using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CInventory : MonoBehaviour {

    [SerializeField] private GameObject m_UseTab;
    [SerializeField] private GameObject m_EquipTab;
    [SerializeField] private GameObject[] m_currency;

	// Use this for initialization
	public void Init()
    {
        m_UseTab.GetComponent<CInventorySlots>().Init();
        m_EquipTab.GetComponent<CInventorySlots>().Init();

        Debug.Log("UseTab: " + m_UseTab.name + "  EqTab: " + m_EquipTab.name);

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

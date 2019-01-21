using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CInventory : MonoBehaviour
{
    [SerializeField] private GameObject m_UseTab;
    [SerializeField] private GameObject[] m_currency;

    // Use this for initialization
    public void Init(Dictionary<string, CItemSlot> _Items)
    {
        m_UseTab.GetComponent<CInventorySlots>().Init(_Items);

        Debug.Log("UseTab: " + m_UseTab.name);

        gameObject.SetActive(false);
        m_UseTab.SetActive(true);
    }

    public void UpdateNoteText(int _Note)
    {
        m_currency[0].GetComponentInChildren<Text>().text = _Note.ToString();
    }

    public void UpdateGemText(int _Gem)
    {
        m_currency[1].GetComponentInChildren<Text>().text = _Gem.ToString();
    }

    #region TabChanger
    public void CloseInventoryTab()
    { gameObject.SetActive(false); }

    public void OpenInventoryTab()
    { gameObject.SetActive(true); }

    #endregion
}

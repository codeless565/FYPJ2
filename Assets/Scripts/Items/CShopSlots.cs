using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CShopSlots : MonoBehaviour
{
    IItem m_item;
    int m_cost;
    bool m_isGemCurrency;

    public void Init()
    {
        //Randomly generate an item
        m_item = CItemDatabase.Instance.RandomItemData;

        int floor = CProgression.Instance.GetMaxDungeonProgression();

        m_isGemCurrency = false;
        if (m_item.ItemName == CItemDatabase.Instance.ReviveTixData.ItemName)
        {
            m_cost = Random.Range(80, 100);
            m_isGemCurrency = true;
        }
        else if (m_item.ItemName == CItemDatabase.Instance.HPElixirData.ItemName || m_item.ItemName == CItemDatabase.Instance.SPElixirData.ItemName)
            m_cost = Random.Range(floor * 80, floor * 100);
        else if (m_item.ItemName == CItemDatabase.Instance.HPPotionData.ItemName || m_item.ItemName == CItemDatabase.Instance.SPPotionData.ItemName)
            m_cost = Random.Range(floor * 20, floor * 30);
        else
            m_cost = Random.Range(floor * 10, floor * 20);

        UpdateDetails();
    }

    public void Init_High()
    {
        //Randomly generate an item
        m_item = CItemDatabase.Instance.RandomHighGradeItemData;

        int floor = CProgression.Instance.GetMaxDungeonProgression();
        m_isGemCurrency = false;
        if (m_item.ItemName == CItemDatabase.Instance.ReviveTixData.ItemName)
        {
            m_cost = Random.Range(80, 100);
            m_isGemCurrency = true;
        }
        else if (m_item.ItemName == CItemDatabase.Instance.HPElixirData.ItemName || m_item.ItemName == CItemDatabase.Instance.SPElixirData.ItemName)
            m_cost = floor * 100;
        else
            m_cost = Random.Range(floor * 90, floor * 100);

        UpdateDetails();
    }

    public void Init_ReviveTix()
    {
        //Randomly generate an item
        m_item = CItemDatabase.Instance.ReviveTixData;

        m_cost = 150;
        m_isGemCurrency = true;

        UpdateDetails();
    }

    public void BuyItem()
    {
        if (m_isGemCurrency)
        {
            if (ShopGenerator.Instance.PlayerData.m_InventorySystem.Gems >= m_cost)
            {
                ShopGenerator.Instance.PlayerData.m_InventorySystem.AddItem(m_item);
                ShopGenerator.Instance.PlayerData.m_InventorySystem.AddGems(-m_cost);
                DisablePurchase();
                return;
            }
            ShopGenerator.Instance.OpenFailPurchaseNotice(true);
        }
        else
        {
            if (ShopGenerator.Instance.PlayerData.m_InventorySystem.Notes >= m_cost)
            {
                ShopGenerator.Instance.PlayerData.m_InventorySystem.AddItem(m_item);
                ShopGenerator.Instance.PlayerData.m_InventorySystem.AddNotes(-m_cost);
                DisablePurchase();
                return;
            }
            ShopGenerator.Instance.OpenFailPurchaseNotice(false);
        }
    }

    private void UpdateDetails()
    {
        transform.GetChild(0).GetComponent<Image>().sprite = m_item.ItemSprite;
        transform.GetChild(1).GetComponentInChildren<Text>().text = m_cost.ToString();

        GameObject currency;

        if (m_isGemCurrency)
        {
            currency = (GameObject)Resources.Load("Items/Currency_Quaver");
            transform.GetChild(1).GetChild(0).GetComponentInChildren<Image>().sprite = currency.GetComponent<SpriteRenderer>().sprite;
        }
        else
        {
            currency = (GameObject)Resources.Load("Items/Currency_Note");
            transform.GetChild(1).GetChild(0).GetComponentInChildren<Image>().sprite = currency.GetComponent<SpriteRenderer>().sprite;
        }
    }

    private void DisablePurchase()
    {
        transform.GetChild(0).GetComponent<Button>().interactable = false;
        transform.GetChild(0).GetComponent<Image>().color = Color.gray;
    }
}

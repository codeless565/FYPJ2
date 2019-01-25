using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopGenerator : MonoBehaviour
{
    [SerializeField]
    GameObject Player;

    [SerializeField]
    GameObject[] ShopItemSlots;

    [SerializeField]
    GameObject FailedNotice;

    public CPlayer PlayerData
    {
        get { return Player.GetComponent<CPlayer>(); } 
    }

    private static ShopGenerator instance;
    public static ShopGenerator Instance
    {
        get
        { return instance; }
    }


    // Use this for initialization
    void Start () {
		if (ShopItemSlots.Length < 6)
        {
            ShopItemSlots = new GameObject[6];
            for (int i = 0; i < 6; ++i)
                ShopItemSlots[i] = gameObject.transform.GetChild(i).gameObject;
        }

        if (Player == null || Player.GetComponent<CPlayer>() == null)
            Player = GameObject.FindGameObjectWithTag("Player");

        if (FailedNotice != null)
            FailedNotice.SetActive(false);

        instance = this;
        Init();

        gameObject.SetActive(false);
    }

    public void Init()
    {
        for (int i = 0; i < ShopItemSlots.Length / 2; ++i)
        {
            Debug.Log(ShopItemSlots[i].name);
            ShopItemSlots[i].GetComponent<CShopSlots>().Init();
        }

        for (int i = ShopItemSlots.Length / 2 + 1; i < ShopItemSlots.Length - 1; ++i)
        {
            ShopItemSlots[i].GetComponent<CShopSlots>().Init_High();
        }

        ShopItemSlots[ShopItemSlots.Length - 1].GetComponent<CShopSlots>().Init_ReviveTix();
    }

    public void OpenFailPurchaseNotice(bool _gemNotice)
    {
        if (FailedNotice != null)
        {
            FailedNotice.SetActive(true);
            if (_gemNotice)
                FailedNotice.GetComponentInChildren<Text>().text = "You do not have enough gem!";
            else
                FailedNotice.GetComponentInChildren<Text>().text = "You do not have enough notes!";
        }
    }

    public void CloseNotice()
    {
        if (FailedNotice != null)
            FailedNotice.SetActive(false);
    }

    public void OpenShopeMenu()
    {
        gameObject.SetActive(true);
    }

    public void CloseShopMenu()
    {
        gameObject.SetActive(false);
    }
}

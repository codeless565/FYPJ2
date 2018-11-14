using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MObjectPool  {
    private static MObjectPool instance;
    public static MObjectPool Instance
    {
        get
        {
            if (instance == null)
                instance = new MObjectPool();
            return instance;
        }
    }
    private MObjectPool()
    {
        m_GameItemList = new Dictionary<string, CItem>();

        InitItems();
    }

    public Dictionary<string, CItem> m_GameItemList;

    public void InitItems()
    {
        CItem hppotion = new CItem("HP_POTION", true, false, true);
        m_GameItemList.Add(hppotion.ItemName,hppotion);
        CItem hpelixir = new CItem("HP_ELIXIR", false, false, true);
        m_GameItemList.Add(hpelixir.ItemName, hpelixir);
        CItem medkit = new CItem("MEDKIT", true, false, true);
        m_GameItemList.Add(medkit.ItemName,medkit);

        CItem sppotion = new CItem("SP_POTION", true, false, true);
        m_GameItemList.Add(sppotion.ItemName,sppotion);
        CItem spelixir = new CItem("SP_ELIXIR", true, false, true);
        m_GameItemList.Add(spelixir.ItemName,spelixir);
        CItem spgenerator = new CItem("SP_GENERATOR", false, true, false);
        m_GameItemList.Add(spgenerator.ItemName,spgenerator);

        CItem revivetick = new CItem("REVIVE_TICKET", false, true, true);
        m_GameItemList.Add(revivetick.ItemName,revivetick);

        CItem expbooster = new CItem("EXP_BOOSTER", false, true, false);
        m_GameItemList.Add(expbooster.ItemName,expbooster);
        CItem attackbooster = new CItem("ATTACK_BOOSTER", false, true, false);
        m_GameItemList.Add(attackbooster.ItemName,attackbooster);
        CItem defensebooster = new CItem("DEFENSE_BOOSTER", false, true, false);
        m_GameItemList.Add(defensebooster.ItemName,defensebooster);
        CItem playratebooster = new CItem("PLAY_RATE_BOOSTER", false, true, false);
        m_GameItemList.Add(playratebooster.ItemName,playratebooster);
    }

    public CItem GetItem(string _itemname)
    {
        if (!m_GameItemList.ContainsKey(_itemname))
            return null;

        foreach(KeyValuePair<string,CItem> i in m_GameItemList)
        {
            if (i.Key.ToLower().Contains(_itemname.ToLower()))
                return i.Value;
        }

        return null;
    }
}

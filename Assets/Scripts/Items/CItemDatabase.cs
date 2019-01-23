using System.Collections.Generic;
using UnityEngine;

public class CItemDatabase
{
    private static CItemDatabase instance;

    public static CItemDatabase Instance
    {
        get
        {
            if (instance == null)
                instance = new CItemDatabase();
            return instance;
        }
    }

    //Items
    GameObject m_HPRation;
    GameObject m_HPPotion;
    GameObject m_HPElixir;
    GameObject m_SPPotion;
    GameObject m_SPElixir;
    GameObject m_ReviveTix;

    List<GameObject> m_ItemPool;
    List<GameObject> m_LowGradePool;
    List<GameObject> m_HighGradePool;

    private CItemDatabase()
    {
        m_ItemPool = new List<GameObject>();
        m_LowGradePool = new List<GameObject>();
        m_HighGradePool = new List<GameObject>();

        m_HPRation = (GameObject)Resources.Load("Items/HPRation");
        m_ItemPool.Add(m_HPRation);
        m_LowGradePool.Add(m_HPRation);

        m_HPPotion = (GameObject)Resources.Load("Items/HPPotion");
        m_ItemPool.Add(m_HPPotion);
        m_LowGradePool.Add(m_HPPotion);

        m_HPElixir = (GameObject)Resources.Load("Items/HPElixir");
        m_ItemPool.Add(m_HPElixir);
        m_HighGradePool.Add(m_HPElixir);

        m_SPPotion = (GameObject)Resources.Load("Items/SPPotion");
        m_ItemPool.Add(m_SPPotion);
        m_LowGradePool.Add(m_SPPotion);

        m_SPElixir = (GameObject)Resources.Load("Items/SPElixir");
        m_ItemPool.Add(m_SPElixir);
        m_HighGradePool.Add(m_SPElixir);

        m_ReviveTix = (GameObject)Resources.Load("Items/RevivalAmulet");
        m_ItemPool.Add(m_ReviveTix);
        m_HighGradePool.Add(m_ReviveTix);
    }

    #region GetGameObject
    //Only allow other script to "get" without modification to original item
    public GameObject HPRation
    {
        get { return m_HPRation; }
    }
    public GameObject HPPotion
    {
        get { return m_HPPotion; }
    }
    public GameObject HPElixir
    {
        get { return m_HPElixir; }
    }
    public GameObject SPPotion
    {
        get { return m_SPPotion; }
    }
    public GameObject SPElixir
    {
        get { return m_SPElixir; }
    }
    public GameObject ReviveTix
    {
        get { return m_ReviveTix; }
    }

    public GameObject RandomItem
    {
        get { return m_ItemPool[Random.Range(0, m_ItemPool.Count)]; }
    }

    public GameObject RandomLowGradeItem
    {
        get { return m_LowGradePool[Random.Range(0, m_LowGradePool.Count)]; }
    }

    public GameObject RandomHighGradeItem
    {
        get { return m_HighGradePool[Random.Range(0, m_HighGradePool.Count)]; }
    }
    #endregion

    #region GetItemData
    public IItem HPRationData
    {
        get { return m_HPRation.GetComponent<IItem>(); }
    }
    public IItem HPPotionData
    {
        get { return m_HPPotion.GetComponent<IItem>(); }
    }
    public IItem HPElixirData
    {
        get { return m_HPElixir.GetComponent<IItem>(); }
    }
    public IItem SPPotionData
    {
        get { return m_SPPotion.GetComponent<IItem>(); }
    }
    public IItem SPElixirData
    {
        get { return m_SPElixir.GetComponent<IItem>(); }
    }
    public IItem ReviveTixData
    {
        get { return m_ReviveTix.GetComponent<IItem>(); }
    }

    public IItem RandomItemData
    {
        get { return m_ItemPool[Random.Range(0, m_ItemPool.Count)].GetComponent<IItem>(); }
    }

    public IItem RandomLowGradeItemData
    {
        get { return m_LowGradePool[Random.Range(0, m_LowGradePool.Count)].GetComponent<IItem>(); }
    }

    public IItem RandomHighGradeItemData
    {
        get { return m_HighGradePool[Random.Range(0, m_HighGradePool.Count)].GetComponent<IItem>(); }
    }
    #endregion

}

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
    }

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
}

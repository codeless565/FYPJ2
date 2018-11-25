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

    private CItemDatabase()
    {
        m_HPRation = (GameObject)Resources.Load("Items/HPRation");
        m_HPPotion = (GameObject)Resources.Load("Items/HPPotion");
        m_HPElixir = (GameObject)Resources.Load("Items/HPElixir");
        m_SPPotion = (GameObject)Resources.Load("Items/SPPotion");
        m_SPElixir = (GameObject)Resources.Load("Items/SPElixir");
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

}

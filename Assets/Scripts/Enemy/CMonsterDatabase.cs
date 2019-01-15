using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMonsterDatabase
{
    private static CMonsterDatabase instance;

    public static CMonsterDatabase Instance
    {
        get
        {
            if (instance == null)
                instance = new CMonsterDatabase();
            return instance;
        }
    }

    List<GameObject> m_LowLevelPool;
    List<GameObject> m_AllLevelPool;

    //Items
    GameObject m_EnemyNoise;
    GameObject m_EnemyStatic;
    GameObject m_EnemyDisturbance;
    GameObject m_EnemyBuzz;
    GameObject m_EnemyBoom;

    private CMonsterDatabase()
    {
        m_LowLevelPool = new List<GameObject>();
        m_AllLevelPool = new List<GameObject>();

        m_EnemyNoise = (GameObject)Resources.Load("Enemy/EnemyNoise");
        Debug.Log("EnemyNoise = " + m_EnemyNoise);
        m_LowLevelPool.Add(m_EnemyNoise);
        m_AllLevelPool.Add(m_EnemyNoise);

        m_EnemyStatic = (GameObject)Resources.Load("Enemy/EnemyStatic");
        Debug.Log("EnemyStatic = " + m_EnemyStatic);
        m_LowLevelPool.Add(m_EnemyStatic);
        m_AllLevelPool.Add(m_EnemyStatic);

        m_EnemyDisturbance = (GameObject)Resources.Load("Enemy/EnemyDisturbance");
        Debug.Log("EnemyDisturbance = " + m_EnemyDisturbance);
        m_LowLevelPool.Add(m_EnemyDisturbance);
        m_AllLevelPool.Add(m_EnemyDisturbance);

        m_EnemyBuzz = (GameObject)Resources.Load("Enemy/EnemyBuzz");
        Debug.Log("EnemyBuzz = " + m_EnemyBuzz);
        m_AllLevelPool.Add(m_EnemyBuzz);

        m_EnemyBoom = (GameObject)Resources.Load("Enemy/EnemyBoom");
        Debug.Log("EnemyBoom = " + m_EnemyBoom);
        m_AllLevelPool.Add(m_EnemyBoom);
    }

    //Only allow other script to "get" without modification to original item
    public GameObject EnemyNoise
    {
        get { return m_EnemyNoise; }
    }
    public GameObject EnemyStatic
    {
        get { return m_EnemyStatic; }
    }
    public GameObject EnemyDisturbance
    {
        get { return m_EnemyDisturbance; }
    }
    public GameObject EnemyBuzz
    {
        get { return m_EnemyBuzz; }
    }
    public GameObject EnemyBoom
    {
        get { return m_EnemyBoom; }
    }

    public GameObject RandomMonster
    {
        get { return m_AllLevelPool[Random.Range(0, m_AllLevelPool.Count)]; }
    }
    public GameObject RandomLowLevelMonster
    {
        get { return m_LowLevelPool[Random.Range(0, m_LowLevelPool.Count)]; }
    }
}

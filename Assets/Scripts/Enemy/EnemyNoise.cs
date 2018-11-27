
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Sprite))]
public class EnemyNoise : MonoBehaviour, IEnemy
{
    private bool m_IsImmortal;

    private CStats m_EnemyStats;
    private Sprite m_EnemySprite;

    CStateMachine m_SM;

    public void Init()
    {
        m_EnemyStats = new CStats();
        SetStats(1, 5, 10, 10, 10, 10, 10, 10, 10, 1, 2);
        m_IsImmortal = false;
        m_EnemySprite = GetComponent<SpriteRenderer>().sprite;
        
        m_SM = new CStateMachine();
        m_SM.AddState(new StateNoisePatrol(this.gameObject));
        m_SM.SetNextState("StateNoisePatrol");
        
    }

    public void Delete()
    {
        PostOffice.Instance.Send("Player", new Message(MESSAGE_TYPE.ADDEXP,this.gameObject));
        Destroy(gameObject);
    }

    public bool GetIsImmortalObject()
    {
        return m_IsImmortal;
    }

    public Sprite GetSprite()
    {
        return m_EnemySprite;
    }

    public CStats GetStats()
    {
        return m_EnemyStats;
    }

    public void IsDamaged(int damage)
    {
        throw new System.NotImplementedException();
    }

    public void SetSprite(Sprite _sprite)
    {
        m_EnemySprite = _sprite;
    }

    public void SetStats(int _level, float _exp, float _maxexp, float _hp, float _maxhp, float _sp, float _maxsp, int _attack, int _defense, float _playrate, float _movespeed)
    {
        m_EnemyStats.Level = _level;
        m_EnemyStats.EXP = _exp;
        m_EnemyStats.MaxEXP = _maxexp;

        m_EnemyStats.HP = _hp;
        m_EnemyStats.MaxHP = _maxhp;

        m_EnemyStats.SP = _sp;
        m_EnemyStats.MaxSP = _maxsp;

        m_EnemyStats.Attack = _attack;
        m_EnemyStats.Defense = _defense;

        m_EnemyStats.PlayRate = _playrate;
        m_EnemyStats.MoveSpeed = _movespeed;
    }

    public void Spawn()
    {
        
        
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        m_SM.Update();

        if (m_EnemyStats.HP <= 0)
            Delete();

        Debug.Log(name + " : " + m_EnemyStats.HP);
    }

    public void IsAttackedByPlayer()
    {
        throw new System.NotImplementedException();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClass : EntityInterface {
    private Stats m_PlayerStats;
    private bool m_IsImmortal;
    private Sprite m_PlayerSprite;

    public PlayerClass()
    {
        m_PlayerStats = new Stats();
        m_IsImmortal = false;
        m_PlayerSprite = null;
    }

    public void Delete()
    {
        throw new System.NotImplementedException();
    }

    public bool GetIsImmortalObject()
    {
        return m_IsImmortal;
    }

    public Sprite GetSprite()
    {
        return m_PlayerSprite;
    }

    public Stats GetStats()
    {
        return m_PlayerStats;
    }

    public void IsDamaged(int damage)
    {
        throw new System.NotImplementedException();
    }

    public void SetSprite(Sprite _sprite)
    {
        m_PlayerSprite = _sprite;
    }

    public void SetStats(int _level, int _exp, int _hp, int _attack, int _defense, float _playrate)
    {
        m_PlayerStats.Level = _level;
        m_PlayerStats.EXP = _exp;
        m_PlayerStats.HP = _hp;
        m_PlayerStats.Attack = _attack;
        m_PlayerStats.Defense = _defense;
        m_PlayerStats.PlayRate = _playrate;
    }

    public void Spawn()
    {
        throw new System.NotImplementedException();
    }
}

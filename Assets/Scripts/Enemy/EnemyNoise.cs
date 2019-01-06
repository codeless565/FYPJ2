
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

    CTRoomCoordinate m_RoomCoord;
    public CTRoomCoordinate roomCoordinate
    {
        get { return m_RoomCoord; }

        set {
            if (m_RoomCoord == null)
            {
                m_RoomCoord = new CTRoomCoordinate(value);
                return;
            }
            m_RoomCoord = value;
        }
    }

    public void Init()
    {
        m_RoomCoord = new CTRoomCoordinate(0,0);

        m_EnemyStats = new CStats();
        SetStats(1, 5, 10, 0, 10, 10, 10, 10, 10, 10, 1, 2);
        m_IsImmortal = false;
        m_EnemySprite = GetComponent<SpriteRenderer>().sprite;

        m_SM = new CStateMachine();
        //m_SM.AddState(new StateNoisePatrol(this.gameObject));
        //m_SM.SetNextState("StateNoisePatrol");
        m_SM.AddState(new CStatePathTest(this.gameObject));
        m_SM.SetNextState("StatePathTest");
    }

    public void Init(CTRoomCoordinate _spawnCoord)
    {
        m_RoomCoord = new CTRoomCoordinate(_spawnCoord);

        m_EnemyStats = new CStats();
        SetStats(1, 5, 10, 10, 10, 10, 10, 10, 10, 1, 2);
        m_IsImmortal = false;
        m_EnemySprite = GetComponent<SpriteRenderer>().sprite;
        
        m_SM = new CStateMachine();
        //m_SM.AddState(new StateNoisePatrol(this.gameObject));
        //m_SM.SetNextState("StateNoisePatrol");
        m_SM.AddState(new CStatePathTest(this.gameObject));
        m_SM.SetNextState("StatePathTest");
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

    public void SetStats(int _level, float _exp, float _maxexp, float _expboost, float _hp, float _maxhp, float _sp, float _maxsp, int _attack, int _defense, float _playrate, float _movespeed)
    {
        m_EnemyStats.Level = _level;
        m_EnemyStats.EXP = _exp;
        m_EnemyStats.MaxEXP = _maxexp;
        m_EnemyStats.EXPBoost = _expboost;

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
        if (m_EnemyStats.HP <= 0)
            Delete();

        if (m_SM != null)
            m_SM.Update();

        Debug.Log(name + " : " + m_EnemyStats.HP);
    }

    public void IsAttackedByPlayer()
    {
        throw new System.NotImplementedException();
    }
}

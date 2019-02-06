using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossRed : MonoBehaviour, IEnemy
{
    private bool m_IsImmortal;

    private CStats m_EnemyStats;
    private Sprite m_EnemySprite;

    bool m_IsInRoom;
    public bool IsInRoom
    {
        get { return m_IsInRoom; }

        set { m_IsInRoom = value; }
    }

    CStateMachine m_SM;
    CStateMachine IEnemy.StateMachine
    {
        get { return m_SM; }
    }

    CTRoomCoordinate m_RoomCoord;
    CTRoomCoordinate IEntity.RoomCoordinate
    {
        get { return m_RoomCoord; }

        set
        {
            if (m_RoomCoord == null)
            {
                m_RoomCoord = new CTRoomCoordinate(value);
                return;
            }
            m_RoomCoord = value;
        }
    }

    GameObject m_Target;
    public GameObject Target
    {
        get { return m_Target; }

        set { m_Target = value; }
    }

    float m_AttackTimer;
    public bool CanAttack
    {
        get
        {
            return m_AttackTimer <= 0.0f;
        }
    }

    public void Init()
    {
        m_RoomCoord = new CTRoomCoordinate(0, 0);

        m_EnemyStats = new CStats();
        SetStats(25, //lvl
            100, 1250, 0, //exp,maxExp,expBoost
            1000, 1000, //HP
            100, 100, //SP
            250,     //atk
            250,     //def
            2,      //playrate
            2);     //movespeed
        m_IsImmortal = false;
        m_EnemySprite = GetComponent<SpriteRenderer>().sprite;

        m_SM = new CStateMachine();
        m_SM.AddState(new CStateBossIdle(this.gameObject, transform.position));
        m_SM.AddState(new CStateBossChase(this.gameObject));
        m_SM.AddState(new CStateBossTeleport(this.gameObject, CTDungeon.Instance.Floors[CTDungeon.Instance.currentFloor].GetRoomFromCoord(m_RoomCoord)));
        m_SM.AddState(new CStateBossAttack(this.gameObject));
        m_SM.AddState(new CStateDead(this.gameObject));

        m_SM.SetNextState("StateIdle");
    }

    public void Init(CTRoomCoordinate _spawnCoord)
    {
        m_RoomCoord = new CTRoomCoordinate(_spawnCoord);

        m_EnemyStats = new CStats();
        SetStats(25, //lvl
            100, 1250, 0, //exp,maxExp,expBoost
            1000, 1000, //HP
            100, 100, //SP
            250,     //atk
            250,     //def
            3,      //playrate
            2);     //movespeed
        m_IsImmortal = false;
        m_EnemySprite = GetComponent<SpriteRenderer>().sprite;

        m_SM = new CStateMachine();
        m_SM.AddState(new CStateBossIdle(this.gameObject, transform.position));
        m_SM.AddState(new CStateBossChase(this.gameObject));
        m_SM.AddState(new CStateBossTeleport(this.gameObject, CTDungeon.Instance.Floors[CTDungeon.Instance.currentFloor].Rooms[1]));
        m_SM.AddState(new CStateBossAttack(this.gameObject));
        m_SM.AddState(new CStateDead(this.gameObject));

        m_SM.SetNextState("StateIdle");
    }

    public void Delete()
    {
        PostOffice.Instance.Send("Player", new Message(MESSAGE_TYPE.ADDEXP, m_EnemyStats.EXP));
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

    public void IsDamaged(float _IncomingDamage, AttackType _attackType)
    {
        if (_attackType == AttackType.Size)
        {
            m_EnemyStats.HP += _IncomingDamage;
            if (m_EnemyStats.HP > m_EnemyStats.MaxHP)
                m_EnemyStats.HP = m_EnemyStats.MaxHP;
        }
        else if (_attackType == AttackType.Charged)
        {
            m_EnemyStats.HP -= CDamageCalculator.Instance.CalculateDamage(_IncomingDamage, m_EnemyStats.Defense * 2);
        }
        else
            m_EnemyStats.HP -= CDamageCalculator.Instance.CalculateDamage(_IncomingDamage, m_EnemyStats.Defense);
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
            m_SM.SetNextState("StateDead");

        if (m_SM != null)
            m_SM.Update();

        if (m_AttackTimer > 0.0f)
            m_AttackTimer -= Time.deltaTime;
    }

    public void IsAttackedByPlayer()
    {
    }

    public void ResetAtkTimer()
    {
        m_AttackTimer = 1 / m_EnemyStats.PlayRate;
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


}

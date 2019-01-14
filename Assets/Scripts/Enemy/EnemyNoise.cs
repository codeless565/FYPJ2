using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Sprite))]
public class EnemyNoise : MonoBehaviour, IEnemy
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

        set {
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
        m_RoomCoord = new CTRoomCoordinate(0,0);

        m_EnemyStats = new CStats();
        SetStats(1, 5, 10, 0, 20, 10, 10, 10, 10, 10, 1, 2);
        m_IsImmortal = false;
        m_EnemySprite = GetComponent<SpriteRenderer>().sprite;

        m_SM = new CStateMachine();
        m_SM.AddState(new CStateIdle(this.gameObject));
        m_SM.AddState(new CStatePatrol(this.gameObject));
        m_SM.AddState(new CStateChangeRoom(this.gameObject));
        m_SM.AddState(new CStateChase(this.gameObject));
        m_SM.AddState(new CStateAttack(this.gameObject));
        m_SM.AddState(new CStateDead(this.gameObject));

        m_SM.SetNextState("StateIdle");
    }

    public void Init(CTRoomCoordinate _spawnCoord)
    {
        m_RoomCoord = new CTRoomCoordinate(_spawnCoord);

        m_EnemyStats = new CStats();
        SetStats(1, 5, 10, 0, 10, 10, 10, 10, 10, 10, 1, 2);
        m_IsImmortal = false;
        m_EnemySprite = GetComponent<SpriteRenderer>().sprite;
        
        m_SM = new CStateMachine();
        m_SM.AddState(new CStateIdle(this.gameObject));
        m_SM.AddState(new CStatePatrol(this.gameObject));
        m_SM.AddState(new CStateChangeRoom(this.gameObject));
        m_SM.AddState(new CStateChase(this.gameObject));
        m_SM.AddState(new CStateAttack(this.gameObject));
        m_SM.AddState(new CStateDead(this.gameObject));

        m_SM.SetNextState("StateIdle");
    }

    public void Delete()
    {
        PostOffice.Instance.Send("Player", new Message(MESSAGE_TYPE.ADDEXP, m_EnemyStats.Level * m_EnemyStats.EXP));
        PostOffice.Instance.Send("Player", new Message(MESSAGE_TYPE.ADDPROP, NoiseSlayer.m_AchievementName, KillNoiseProp.m_propertyname, 1f));

        QBQuest.Instance.UpdateQuest(QuestType.SLAY, QuestTarget.NOISE);
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

    public void IsDamaged(float damage)
    {
        m_EnemyStats.HP -= damage;
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
        throw new System.NotImplementedException();
    }

    public void ResetAtkTimer()
    {
        m_AttackTimer = m_EnemyStats.PlayRate;
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

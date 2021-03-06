﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Sprite))]
public class CPlayer : MonoBehaviour ,IEntity
{
    [SerializeField]
    bool IsInTown = false;

    private bool m_IsImmortal;
    public bool isDead;

    private CStats m_PlayerStats;
    private Sprite m_PlayerSprite;

    public CInventorySystem m_InventorySystem;
    public PrestigeSystem m_PrestigeSystem;
    public GameObject InventoryPanel;
    public GameObject InventoryUI;

    CWeapon m_EquippedWeapon;
    public CWeapon EquippedWeapon
    {
        get { return m_EquippedWeapon; }
        set { m_EquippedWeapon = value; }
    }

    float OOCtimer = 0f;

    public PlayerUIScript UIScript;

    private List<QuestBase> m_playerQuestList;
    public List<QuestBase> QuestList
    {
        get
        { return m_playerQuestList; }
    }

    bool m_IsInRoom;
    public bool IsInRoom
    {
        get { return m_IsInRoom; }

        set { m_IsInRoom = value; }
    }

    CTRoomCoordinate m_RoomCoord;
    public CTRoomCoordinate RoomCoordinate
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

    public CPlayer()
    {
        m_RoomCoord = new CTRoomCoordinate(0,0);
    }

    public void Init()
    {
        this.name = "Player";
        m_PlayerStats = new CStats();
        m_InventorySystem = new CInventorySystem(InventoryPanel.GetComponent<CInventorySlots>(), InventoryUI.GetComponent<CInventory>());
        m_playerQuestList = new List<QuestBase>();

        CProgression.Instance.LoadPlayerSave(this);

        m_RoomCoord = new CTRoomCoordinate(0, 0);

        PostOffice.Instance.Register(name, gameObject); // TODO Move to Spawn() ?

        m_IsImmortal = false;
        m_PlayerSprite = GetComponent<SpriteRenderer>().sprite;

        m_PrestigeSystem = new PrestigeSystem();
        AchievementSystem.Instance.Init(this.GetStats());

        UIScript = GetComponent<PlayerUIScript>();
        UIScript.Init();
    }

    public void Update()
    {
        //if (Input.GetKeyDown(KeyCode.L))
        //{
        //    m_PlayerStats.EXP = m_PlayerStats.MaxEXP;
        //}
        //if (Input.GetKeyDown(KeyCode.K))
        //{
        //    //IsDamaged(2);
        //    //GetComponent<PlayerUIScript>().AddEXP(m_PlayerStats.EXP,7);
        //    //GetComponent<PlayerUIScript>().AddHealth(m_PlayerStats.HP, 2);
        //}
        if (m_PlayerStats.EXP >= m_PlayerStats.MaxEXP)
            LevelingSystem();

        m_PrestigeSystem.Update();
        OutOfCombatSystem();
        #region
        //Test Add
        if (Input.GetKeyDown(KeyCode.Z))
        {
            GameObject newItem = CItemDatabase.Instance.HPRation;
            m_InventorySystem.AddItem(newItem.GetComponent<IItem>());
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            GameObject newItem = CItemDatabase.Instance.HPPotion;
            m_InventorySystem.AddItem(newItem.GetComponent<IItem>());
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            GameObject newItem = CItemDatabase.Instance.HPElixir;
            m_InventorySystem.AddItem(newItem.GetComponent<IItem>());
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            GameObject newItem = CItemDatabase.Instance.SPPotion;
            m_InventorySystem.AddItem(newItem.GetComponent<IItem>());
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            GameObject newItem = CItemDatabase.Instance.SPElixir;
            m_InventorySystem.AddItem(newItem.GetComponent<IItem>());
        }

        if (Input.GetKeyDown(KeyCode.Keypad1))
            m_InventorySystem.RemoveItem("HPRATION");
        if (Input.GetKeyDown(KeyCode.Keypad2))
            m_InventorySystem.RemoveItem("HPPOTION");
        if (Input.GetKeyDown(KeyCode.Keypad3))
            m_InventorySystem.RemoveItem("HPELIXIR");
        if (Input.GetKeyDown(KeyCode.Keypad4))
            m_InventorySystem.RemoveItem("SPPOTION");
        if (Input.GetKeyDown(KeyCode.Keypad5))
            m_InventorySystem.RemoveItem("SPELIXIR");
        if (Input.GetKeyDown(KeyCode.Keypad0))
            m_InventorySystem.DebugLogAll();


        if (Input.GetKeyDown(KeyCode.N))
            UseItem("HPPO");
        #endregion
        // Controls
        Move();
        Attack();

        // Weapon System Update
        m_EquippedWeapon.UpdateWeapon(Time.deltaTime);
        //Debug.Log(m_PlayerStats.Level + ": " + m_PlayerStats.EXP + "/" + m_PlayerStats.MaxEXP);

        //if(Input.GetKeyDown(KeyCode.C))
        //{
        //    if(m_playerQuestList.Count > 0)
        //        PostOffice.Instance.Send("Player", new Message(MESSAGE_TYPE.QUEST, QuestType.SLAY.ToString(), QuestTarget.NOISE.ToString()));
        //}
        if(m_PlayerStats.SP <= 100f)
       UIScript.AddSP(m_PlayerStats.SP, Time.deltaTime);

        //Use item
        if (Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1))
        {
            UseItem(CItemDatabase.Instance.HPRationData.ItemKey);
        }
        if (Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Alpha2))
        {
            UseItem(CItemDatabase.Instance.HPPotionData.ItemKey);
        }
        if (Input.GetKeyDown(KeyCode.Keypad3) || Input.GetKeyDown(KeyCode.Alpha3))
        {
            UseItem(CItemDatabase.Instance.HPElixirData.ItemKey);
        }
        if (Input.GetKeyDown(KeyCode.Keypad4) || Input.GetKeyDown(KeyCode.Alpha4))
        {
            UseItem(CItemDatabase.Instance.SPPotionData.ItemKey);
        }
        if (Input.GetKeyDown(KeyCode.Keypad5) || Input.GetKeyDown(KeyCode.Alpha5))
        {
            UseItem(CItemDatabase.Instance.SPElixirData.ItemKey);
        }

        //CHEAT
        if (Input.GetKeyDown(KeyCode.F12))
        {
            m_InventorySystem.AddNotes(1000);
            m_InventorySystem.AddGems(1000);
        }
        if (Input.GetKeyDown(KeyCode.F11))
        {
            m_InventorySystem.AddNotes(-1000);
            m_InventorySystem.AddGems(-1000);
        }

    }


    public void OutOfCombatSystem()
    {
        OOCtimer += Time.deltaTime;


        if (OOCtimer >= 5f) // 5s
        {
            if(m_PrestigeSystem.GetList().ContainsKey("Encore"))
            {
                Encore encoreprestige = (Encore)m_PrestigeSystem.GetPrestige(Encore.prestigename);
                if(encoreprestige.isActive)
                   UIScript.AddHealth(m_PlayerStats.HP, 1f * Time.deltaTime);
            }
            else
               UIScript.AddHealth(m_PlayerStats.HP, 0.5f*Time.deltaTime);


        }
    }

    public void UpdateQuest(string _questtype, string _questtarget)
    {
        foreach (QuestBase qb in m_playerQuestList)
        {
            if (qb.QuestComplete)
                continue;

            // if slay
            if (_questtype == QuestType.SLAY.ToString())
            {
                if (qb.QuestTarget.ToString() == _questtarget.ToString())
                    qb.QuestAmount++;
                if (qb.QuestAmount >= qb.QuestCompleteAmount)
                    qb.QuestComplete = true;
                
            }
            else if (_questtype == QuestType.REACH.ToString())
            {
                if (CTDungeon.Instance.currentFloor == qb.QuestCompleteAmount)
                    qb.QuestComplete = true;
                
            }
        }
    }

    public bool AddNewQuest(QuestBase _quest)
    {
        if (m_playerQuestList.Count >= 3)
            return false;
        else
        {
            m_playerQuestList.Add(_quest);
            //GetComponent<PlayerUIScript>().UpdateQuestUI();
            return true;
        }
    }

    public void RemoveAllPrestigeStats()
    {
        foreach (KeyValuePair<string, PrestigeBase> pb in m_PrestigeSystem.GetList())
            pb.Value.RemovePrestigeStats();
    }

    public void AddAllPrestigeStats()
    {
        foreach (KeyValuePair<string, PrestigeBase> pb in m_PrestigeSystem.GetList())
            pb.Value.AddPrestigeStats();
    }

    public void LevelingSystem()
    {
        RemoveAllPrestigeStats();

        int CurrLevel = m_PlayerStats.Level += 1;
        float excessEXP =UIScript.m_TargetedEXP - m_PlayerStats.MaxEXP;
        m_PlayerStats.EXP = 0f;

        m_PlayerStats.MaxEXP = m_PlayerStats.Level * 10;
       UIScript.EXPSlider.fillAmount = m_PlayerStats.MaxEXP;
       UIScript.m_FromEXP = m_PlayerStats.EXP;
       UIScript.m_TargetedEXP = m_PlayerStats.EXP;

        // Update State according to level
        m_PlayerStats.MaxHP = CurrLevel * 10;
        m_PlayerStats.HP += m_PlayerStats.MaxHP * 0.1f;
        m_PlayerStats.MaxSP = CurrLevel * 10;
        m_PlayerStats.SP += m_PlayerStats.MaxHP * 0.1f;
        m_PlayerStats.Attack = CurrLevel * 8;
        m_PlayerStats.Defense = CurrLevel * 8;
        m_PlayerStats.PlayRate = CurrLevel * 0.05f + 1;

        print("Level up");
        if (m_PlayerStats.Level == 10)
            m_PrestigeSystem.AddPrestige(new Maintenance(this));
        else if (m_PlayerStats.Level == 20)
            m_PrestigeSystem.AddPrestige(new Metronome(this));
        else if (m_PlayerStats.Level == 30)
            m_PrestigeSystem.AddPrestige(new Amplifier(this));
        else if (m_PlayerStats.Level == 40)
            m_PrestigeSystem.AddPrestige(new NoiseCanceller(this));
        else if (m_PlayerStats.Level == 50)
            m_PrestigeSystem.AddPrestige(new PopularityBoost(this));
        else if (m_PlayerStats.Level == 60)
            m_PrestigeSystem.AddPrestige(new Perfection(this));
        else if (m_PlayerStats.Level == 70)
            m_PrestigeSystem.AddPrestige(new Encore(this));
        else if (m_PlayerStats.Level == 80)
            m_PrestigeSystem.AddPrestige(new GuardianAngel(this));
        //else if (m_PlayerStats.Level == 90)
        //    m_PrestigeSystem.GetList().Add(new Euphoria());

        // Add Excess EXP
       UIScript.AddEXP(m_PlayerStats.EXP, excessEXP);


        AddAllPrestigeStats();
    }

    public void Move()
    {
        if (Input.GetKey(KeyCode.W))
            transform.position += new Vector3(0, 1) * m_PlayerStats.MoveSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.S))
            transform.position -= new Vector3(0, 1) * m_PlayerStats.MoveSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.D))
            transform.position += new Vector3(1, 0) * m_PlayerStats.MoveSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
            transform.position -= new Vector3(1, 0) * m_PlayerStats.MoveSpeed * Time.deltaTime;
    }

    public void Attack()
    {
        if (IsInTown)
            return;

        if (Input.GetMouseButtonDown(0))
            m_EquippedWeapon.IsCharging();

        if (Input.GetMouseButtonUp(0))
            m_EquippedWeapon.IsAttacking(m_PlayerStats.Attack, transform, ((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position).normalized, 1/m_PlayerStats.PlayRate);

        if (Input.GetMouseButtonDown(1))
            m_PlayerStats.SP -= m_EquippedWeapon.SpecialAttack(m_PlayerStats.SP, m_PlayerStats.Attack, transform, ((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position).normalized, 1/m_PlayerStats.PlayRate);
    }

    public void UseItem(string _itemKey)
    {
        IItem temp = m_InventorySystem.GetItem(_itemKey);

        if (temp != null)
        {
            if (temp.UseItem(this))
                m_InventorySystem.RemoveItem(_itemKey);
            return;
        }
    }

    public void Delete()
    {
        PostOffice.Instance.Remove(this.name, this.gameObject);
        Destroy(gameObject);
    }

    public bool GetIsImmortalObject()
    {
        return m_IsImmortal;
    }

    public Sprite GetSprite()
    {
        return m_PlayerSprite;
    }

    public CStats GetStats()
    {
        return m_PlayerStats;
    }

    public void IsDamaged(float _incomingDamage, AttackType _attackType)
    {
        // Guardian Angel 
        if (m_PrestigeSystem.GetList().ContainsKey("GuardianAngel"))
        {
            GuardianAngel perfectionprestige = (GuardianAngel)m_PrestigeSystem.GetPrestige("GuardianAngel");

            if (perfectionprestige.isInvulnerable)
                return;
        }

        // Perfection
        if (m_PrestigeSystem.GetList().ContainsKey("Perfection"))
        {
            Perfection perfectionprestige = (Perfection)m_PrestigeSystem.GetPrestige("Perfection");

            if (perfectionprestige.isProtected)
            {
                perfectionprestige.timer = 0f;
                return;
            }
        }

       UIScript.RemoveHealth(m_PlayerStats.HP, CDamageCalculator.Instance.CalculateDamage(_incomingDamage, m_PlayerStats.Defense));
        OOCtimer = 0f;
    }

    public void SetSprite(Sprite _sprite)
    {
        m_PlayerSprite = _sprite;
    }

    public void SetStats(int _level, float _exp, float _maxexp, float _expboost, float _hp, float _maxhp, float _sp, float _maxsp, int _attack, int _defense, float _playrate, float _movespeed)
    {
        m_PlayerStats.Level = _level;
        m_PlayerStats.EXP = _exp;
        m_PlayerStats.MaxEXP = _maxexp;
        m_PlayerStats.EXPBoost = _expboost;
        
        m_PlayerStats.HP = _hp;
        m_PlayerStats.MaxHP = _maxhp;
        
        m_PlayerStats.SP = _sp;
        m_PlayerStats.MaxSP = _maxsp;
        
        m_PlayerStats.Attack = _attack;
        m_PlayerStats.Defense = _defense;
      
        m_PlayerStats.PlayRate = _playrate;
        m_PlayerStats.MoveSpeed = _movespeed;
    }

    public void Spawn(Vector3 _pos)
    {
        transform.position = _pos;
    }
}

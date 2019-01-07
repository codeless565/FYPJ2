using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Sprite))]
public class CPlayer : MonoBehaviour ,IEntity
{
    private bool m_IsImmortal;
    public bool isDead;

    private CStats m_PlayerStats;
    private Sprite m_PlayerSprite;

    public CInventorySystem m_InventorySystem;
    public PrestigeSystem m_PrestigeSystem;
    public GameObject InventoryPanel;
    public GameObject InventoryUI;

    CWeapon m_EquippedWeapon;

    CTRoomCoordinate m_RoomCoord;
    public CTRoomCoordinate roomCoordinate
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
        m_InventorySystem = new CInventorySystem(InventoryPanel.GetComponent<CInventorySlots>(), InventoryUI.GetComponent<CInventory>());
        m_RoomCoord = new CTRoomCoordinate(0, 0);

        PostOffice.Instance.Register(name, gameObject); // TODO Move to Spawn() ?

        m_PlayerStats = new CStats();
        SetStats(1, 0, 10, 1, 5, 10, 100, 100, 10, 10, 1, 5);
        m_IsImmortal = false;
        m_PlayerSprite = GetComponent<SpriteRenderer>().sprite;

        m_EquippedWeapon = new TestWeapon();
        GetComponent<PlayerUIScript>().Init();
        m_PrestigeSystem = new PrestigeSystem();
        AchievementSystem.Instance.Init(this.GetStats());
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            m_PlayerStats.EXP = m_PlayerStats.MaxEXP;
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            //IsDamaged(2);
            //GetComponent<PlayerUIScript>().AddEXP(m_PlayerStats.EXP,7);
            //GetComponent<PlayerUIScript>().AddHealth(m_PlayerStats.HP, 2);
        }
        if (m_PlayerStats.EXP >= m_PlayerStats.MaxEXP)
            LevelingSystem();

        m_PrestigeSystem.Update();

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

            m_PlayerStats.Level += 1;
            float excessEXP = GetComponent<PlayerUIScript>().m_TargetedEXP - m_PlayerStats.MaxEXP;
            m_PlayerStats.EXP = 0f;

            m_PlayerStats.MaxEXP = m_PlayerStats.Level * 10;
            GetComponent<PlayerUIScript>().EXPSlider.maxValue = m_PlayerStats.MaxEXP;
            GetComponent<PlayerUIScript>().m_FromEXP = m_PlayerStats.EXP;
            GetComponent<PlayerUIScript>().m_TargetedEXP = m_PlayerStats.EXP;

            // HP/SP level update with UI Update
            //m_PlayerStats.MaxHP = m_PlayerStats.Level * 10;
            //m_PlayerStats.HP = m_PlayerStats.MaxHP;

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
            //else if (m_PlayerStats.Level == 60)
            //    m_PrestigeSystem.GetList().Add(new Perfection(this));
            //else if (m_PlayerStats.Level == 70)
            //    m_PrestigeSystem.GetList().Add(new Encore());
            //else if (m_PlayerStats.Level == 80)
            //    m_PrestigeSystem.GetList().Add(new GuardianAngel());
            //else if (m_PlayerStats.Level == 90)
            //    m_PrestigeSystem.GetList().Add(new Euphoria());

            // Add Excess EXP
            GetComponent<PlayerUIScript>().AddEXP(m_PlayerStats.EXP, excessEXP);
            
        
        AchievementSystem.Instance.CheckAchievementRequirement();
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
        if (Input.GetMouseButtonDown(0))
            m_EquippedWeapon.IsCharging();

        if (Input.GetMouseButtonUp(0))
            m_EquippedWeapon.IsAttacking(m_PlayerStats.Attack, transform.position, ((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position).normalized, m_PlayerStats.PlayRate);

        if (Input.GetMouseButtonDown(1))
            m_PlayerStats.SP -= m_EquippedWeapon.SpecialAttack(m_PlayerStats.SP, m_PlayerStats.Attack, transform.position, ((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position).normalized, m_PlayerStats.PlayRate);
    }

    public void UseItem(string _itemKey)
    {
        IItem temp = m_InventorySystem.GetItem(_itemKey);

        if (temp != null)
        {
            if (temp.UseItem(ref m_PlayerStats))
                m_InventorySystem.RemoveItem(_itemKey);
            return;
        }
        Debug.Log("Item dont exist");
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

    public void IsDamaged(int damage)
    {
        //if (m_PrestigeSystem.GetList().ContainsKey(Perfection.prestigename))
        //{
        //    Perfection perfectionprestige = (Perfection)m_PrestigeSystem.GetPrestige(Perfection.prestigename);

        //    if (perfectionprestige.timer >= 10f)
        //        return;

        //    perfectionprestige.timer = 0f;
        //}
        GetComponent<PlayerUIScript>().RemoveHealth(m_PlayerStats.HP, damage);
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

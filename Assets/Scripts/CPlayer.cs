using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Sprite))]
public class CPlayer : MonoBehaviour , CPrestige,IEntity
{
    private bool m_IsImmortal;
    public bool isDead;

    private CStats m_PlayerStats;
    private PRESTIGELEVEL m_PlayerPrestige;
    private Sprite m_PlayerSprite;

    public CInventorySystem m_InventorySystem;
    public GameObject InventoryPanel;
    public GameObject InventoryUI;

    CWeapon m_EquippedWeapon;
    
    public CPlayer()
    {
    }

    public void Init()
    {
        this.name = "Player";
        m_InventorySystem = new CInventorySystem(InventoryPanel.GetComponent<CInventorySlots>(), InventoryUI.GetComponent<CInventory>());
        
        PostOffice.Instance.Register(name, gameObject);

        m_PlayerStats = new CStats();
        SetStats(1, 0, 10, 1, 10, 10, 10, 10, 10, 10, 1, 5);
        m_IsImmortal = false;
        m_PlayerSprite = GetComponent<SpriteRenderer>().sprite;

        m_EquippedWeapon = new TestWeapon();
        GetComponent<PlayerUI>().Init();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
            print(m_PlayerStats.HP);

        if (Input.GetKeyDown(KeyCode.L))
            RemoveHealth(2);

        LevelingSystem();

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

        // Controls
        Move();
        Attack();

        // Weapon System Update
        m_EquippedWeapon.UpdateWeapon(Time.deltaTime);
    }

    public void AddPrestigeDescription(PRESTIGELEVEL _prestige)
    {
        switch (_prestige)
        {
            case PRESTIGELEVEL.MAINTENANCE:
                m_PlayerStats.HP *= 1.1f;
                break;
            case PRESTIGELEVEL.METRONOME:
                m_PlayerStats.PlayRate *= 1.1f;
                break;
            case PRESTIGELEVEL.AMPLIFIER:
                m_PlayerStats.Attack = m_PlayerStats.Attack / 100 * 110;
                break;
            case PRESTIGELEVEL.NOICECANCELLER:
                m_PlayerStats.Defense = m_PlayerStats.Defense / 100 * 110;
                break;
            case PRESTIGELEVEL.POPULARITY:
                m_PlayerStats.BonusEXP = 2;
                break;
            case PRESTIGELEVEL.PERFECTION:
                throw new System.NotImplementedException();
            case PRESTIGELEVEL.ENCORE:
                throw new System.NotImplementedException();
            case PRESTIGELEVEL.GUARDIANANGEL:
                throw new System.NotImplementedException();
            case PRESTIGELEVEL.EUPHORIA:
                throw new System.NotImplementedException();
        }
    }

    public void PrestigeSystem()
    {
        for (int i = 0; i < (int)m_PlayerPrestige; i++)
        {
            AddPrestigeDescription((PRESTIGELEVEL)i);
        }
    }

    public void LevelingSystem()
    {
        if(m_PlayerStats.EXP >= m_PlayerStats.MaxEXP)
        {
            m_PlayerStats.Level += 1;
            m_PlayerStats.EXP -= m_PlayerStats.MaxEXP;
            m_PlayerStats.MaxEXP = m_PlayerStats.Level * 10;
            GetComponent<PlayerUI>().EXPSlider.maxValue = m_PlayerStats.MaxEXP;
            // HP/SP update
            PrestigeSystem();
            print("Level up");
        }
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
            m_EquippedWeapon.NormalAttack(m_PlayerStats.Attack, transform.position, (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized, m_PlayerStats.PlayRate);
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
        throw new System.NotImplementedException();
    }

    public void SetSprite(Sprite _sprite)
    {
        m_PlayerSprite = _sprite;
    }

    public void SetStats(int _level, float _exp, float _maxexp, float _bonusexp, float _hp, float _maxhp, float _sp, float _maxsp, int _attack, int _defense, float _playrate, float _movespeed)
    {
        m_PlayerStats.Level = _level;
        m_PlayerStats.EXP = _exp;
        m_PlayerStats.MaxEXP = _maxexp;
        m_PlayerStats.BonusEXP = _bonusexp;
        
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

    public void RemoveHealth(float _health)
    {
        GetComponent<PlayerUI>().m_FromHealth = m_PlayerStats.HP;
        GetComponent<PlayerUI>().m_TargetedHealth -= _health;
    }
    public void AddHealth(float _health)
    {
        GetComponent<PlayerUI>().m_FromHealth = m_PlayerStats.HP;
        GetComponent<PlayerUI>().m_TargetedHealth += _health;
    }

    public void AddEXP(float _exp)
    {
        
        m_PlayerStats.EXP += (_exp * m_PlayerStats.BonusEXP);
    }

    public PRESTIGELEVEL GetPretige()
    {
        return m_PlayerPrestige;
    }

    public void SetPrestige(PRESTIGELEVEL _prestige)
    {
        m_PlayerPrestige = _prestige;
    }

    public void IncreasePrestige()
    {
        if (m_PlayerPrestige == PRESTIGELEVEL.EUPHORIA)
            return;

        m_PlayerPrestige++;
    }
}

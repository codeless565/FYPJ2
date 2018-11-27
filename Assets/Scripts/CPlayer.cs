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

    public CInventory m_Inventory;
    public GameObject InventoryPanel;

    CWeapon m_EquippedWeapon;
    
    public Slider HPSlider;
    public Slider SPSlider;
    public Slider EXPSlider;

    float m_FromHealth;
    float m_TargetedHealth;

    public CPlayer()
    {
    }

    public void Init()
    {
        PostOffice.Instance.Register(name, gameObject); // TODO Move to Spawn() ?
        this.name = "Player";
        m_Inventory = new CInventory(InventoryPanel.GetComponent<CInventorySlots>());
        m_PlayerStats = new CStats();
        SetStats(1, 0, 10, 10, 10, 10, 10, 10, 10, 1, 5);
        m_IsImmortal = false;
        m_PlayerSprite = GetComponent<SpriteRenderer>().sprite;

        m_EquippedWeapon = new TestWeapon();
        
        HPSlider.maxValue = m_PlayerStats.MaxHP;
        SPSlider.maxValue = m_PlayerStats.MaxSP;
        EXPSlider.maxValue = m_PlayerStats.MaxEXP;

        m_TargetedHealth = m_PlayerStats.HP;
        m_FromHealth = m_PlayerStats.HP;
    }

    public void Update()
    {
        if (m_PlayerStats.HP > m_TargetedHealth)
            m_PlayerStats.HP -= Mathf.Abs(m_FromHealth-m_TargetedHealth) * Time.deltaTime;
        else if (m_PlayerStats.HP < m_TargetedHealth)
            m_PlayerStats.HP = m_TargetedHealth;



        if (Input.GetKeyDown(KeyCode.K))
            print(m_PlayerStats.HP);

        if (Input.GetKeyDown(KeyCode.L))
            RemoveHealth(9);

        HPSlider.value = m_PlayerStats.HP;
        SPSlider.value = m_PlayerStats.SP;
        EXPSlider.maxValue = m_PlayerStats.EXP;

        //Test Add
        if (Input.GetKeyDown(KeyCode.Z))
        {
            GameObject newItem = CItemDatabase.Instance.HPRation;
            m_Inventory.AddItem(newItem.GetComponent<IItem>());
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            GameObject newItem = CItemDatabase.Instance.HPPotion;
            m_Inventory.AddItem(newItem.GetComponent<IItem>());
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            GameObject newItem = CItemDatabase.Instance.HPElixir;
            m_Inventory.AddItem(newItem.GetComponent<IItem>());
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            GameObject newItem = CItemDatabase.Instance.SPPotion;
            m_Inventory.AddItem(newItem.GetComponent<IItem>());
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            GameObject newItem = CItemDatabase.Instance.SPElixir;
            m_Inventory.AddItem(newItem.GetComponent<IItem>());
        }

        if (Input.GetKeyDown(KeyCode.Keypad1))
            m_Inventory.RemoveItem("HPRATION");
        if (Input.GetKeyDown(KeyCode.Keypad2))
            m_Inventory.RemoveItem("HPPOTION");
        if (Input.GetKeyDown(KeyCode.Keypad3))
            m_Inventory.RemoveItem("HPELIXIR");
        if (Input.GetKeyDown(KeyCode.Keypad4))
            m_Inventory.RemoveItem("SPPOTION");
        if (Input.GetKeyDown(KeyCode.Keypad5))
            m_Inventory.RemoveItem("SPELIXIR");
        if (Input.GetKeyDown(KeyCode.Keypad0))
            m_Inventory.DebugLogAll();


        if (Input.GetKeyDown(KeyCode.N))
            UseItem("HPPO");

        // Controls
        Move();
        Attack();

        // Weapon System Update
        m_EquippedWeapon.UpdateWeapon(Time.deltaTime);
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
        IItem temp = m_Inventory.GetItem(_itemKey);

        if (temp != null)
        {
            if (temp.UseItem(ref m_PlayerStats))
                m_Inventory.RemoveItem(_itemKey);
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

    public void SetStats(int _level, float _exp, float _maxexp, float _hp, float _maxhp, float _sp, float _maxsp, int _attack, int _defense, float _playrate, float _movespeed)
    {
        m_PlayerStats.Level = _level;
        m_PlayerStats.EXP = _exp;
        m_PlayerStats.MaxEXP = _maxexp;
        
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
        m_FromHealth = m_PlayerStats.HP;
        m_TargetedHealth -= _health;
    }
    public void AddHealth(float _health)
    {
        m_FromHealth = m_PlayerStats.HP;
        m_TargetedHealth += _health;
    }

    public void AddEXP(int _exp)
    {
        m_PlayerStats.EXP += _exp;
    }
}

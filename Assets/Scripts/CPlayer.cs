using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Sprite))]
public class CPlayer : MonoBehaviour ,IEntity
{
    private CStats m_PlayerStats;
    private bool m_IsImmortal;
    private Sprite m_PlayerSprite;
    public Dictionary<string, CItem> m_ItemDictonary;
    CWeapon m_EquippedWeapon;

    public void Start()
    {
        Spawn();
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
            print(m_PlayerStats.HP);




        if (Input.GetKeyDown(KeyCode.U))
        {
            PostOffice.Instance.Send("Player", Message.MESSAGE_TYPE.ADDHPPOT);
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            PostOffice.Instance.Send("Player", Message.MESSAGE_TYPE.USEHPPOT);
        }
        Move();


        Attack();
    }
    public void Attack()
    {
        if (Input.GetMouseButtonDown(0))
            m_EquippedWeapon.NormalAttack();
    }

    public void UseItem(string _itemname)
    {
        if (!m_ItemDictonary.ContainsKey(_itemname))
            return; // TODO user does not have item

        m_ItemDictonary[_itemname].PlayerUseItem(gameObject,_itemname);
        //print("Player used " + _itemname);
    }



    public void Move()
    {
        if (Input.GetKey(KeyCode.W))
            transform.position += new Vector3(0,1) * m_PlayerStats.MoveSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.S))
            transform.position -= new Vector3(0, 1) * m_PlayerStats.MoveSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.D))
            transform.position += new Vector3(1, 0) * m_PlayerStats.MoveSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
            transform.position -= new Vector3(1, 0) * m_PlayerStats.MoveSpeed * Time.deltaTime;
    }

    public CPlayer()
    {
        //throw new System.NotImplementedException();
        
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

    public void SetStats(int _level, int _exp, int _hp, int _attack, int _defense, float _playrate, float _movespeed)
    {
        m_PlayerStats.Level = _level;
        m_PlayerStats.EXP = _exp;
        m_PlayerStats.HP = _hp;
        m_PlayerStats.Attack = _attack;
        m_PlayerStats.Defense = _defense;
        m_PlayerStats.PlayRate = _playrate;
        m_PlayerStats.MoveSpeed = _movespeed;
    }

    public void Spawn()
    {
        this.name = "Player";
        PostOffice.Instance.Register(name, gameObject); // TODO Move to Spawn() ?

        m_ItemDictonary = new Dictionary<string, CItem>();
        m_PlayerStats = new CStats();
        m_IsImmortal = false;
        m_PlayerSprite = null;
        //m_PrimaryWeapon = new Firebolt();
    }

    public void AddItem(string _itemname)
    {
        CItem newitem = MObjectPool.Instance.GetItem(_itemname);
        if (!newitem.IsStackable)
        {
            if (!m_ItemDictonary.ContainsKey(_itemname))
            {
                m_ItemDictonary.Add(_itemname, newitem);
                m_ItemDictonary[_itemname].Quantity += 1;
            }
            else
                return; // TODO annoucement user alr have 
        }
        else
        {
            if(!m_ItemDictonary.ContainsKey(_itemname))
                                m_ItemDictonary.Add(_itemname, newitem);
            

            m_ItemDictonary[_itemname].Quantity += 1;
        }
        //print(_itemname + " added to Player");
    }
}

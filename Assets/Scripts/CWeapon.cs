using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CWeapon
{
    protected bool m_isCharging;
    protected float C_chargeTime;
    protected float m_firingDelay;
    protected CWeaponStats m_WeaponStats;
    protected Sprite m_WeaponSprite;

    protected GameObject NormalBullet;
    protected GameObject ChargeBullet;
    protected GameObject SpecialBullet;

    public CWeapon()
    {
        m_WeaponStats = new CWeaponStats();
        m_WeaponSprite = null;
    }

    public virtual void UpdateWeapon(float _dt)
    {
        Debug.Log("Using base function: CWeapon.UpdateWeapon");
        if (m_firingDelay > 0)
            m_firingDelay -= _dt;
    }

    public virtual void IsCharging()
    {
        Debug.Log("Using base function: CWeapon.IsCharging");
        m_isCharging = true;
    }

    public virtual void IsAttacking(int _damage, Vector2 _position, Vector2 _direction, float _firingDelay)
    {
        Debug.Log("Using base function: CWeapon.IsAttacking");
    }

    protected virtual void NormalAttack(int _damage, Vector2 _position, Vector2 _direction, float _firingDelay)
    {
        //Create NormalAttack prefab or smthing with projectile script
        Debug.Log("Using base function: CWeapon.NormalAttack");
    }

    protected virtual void ChargeAttack(int _damage, Vector2 _position, Vector2 _direction, float _firingDelay)
    {
        //Create normal attack prefab or smthing with projectile script
        Debug.Log("Using base function: CWeapon.ChargeAttack");
    }

    public virtual float SpecialAttack(float userSP, int _damage, Vector2 _position, Vector2 _direction, float _firingDelay)
    {
        //Create SpecialAttack prefab or smthing with projectile script
        Debug.Log("Using base function: CWeapon.SpecialAttack");
        return 0.0f;
    }

    public Sprite WeaponSprite
    {
        get
        {
            return m_WeaponSprite;
        }

        set
        {
            m_WeaponSprite = value;
        }
    }

    public CWeaponStats Stats
    {
        get
        {
            return m_WeaponStats;
        }

        set
        {
            m_WeaponStats = value;
        }
    }

    public void SetWeaponStats(float _attackMultiplier, int _attackRange)
    {
        m_WeaponStats.AttackMultiplier = _attackMultiplier;
        m_WeaponStats.Range = _attackRange;
    }

    public virtual string Name
    { get { return ""; } }
}

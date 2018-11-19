using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CWeapon
{
    protected float firingDelay;
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
        if (firingDelay > 0)
            firingDelay -= _dt;
    }

    public virtual void NormalAttack(int _damage, Vector2 _position, Vector2 _direction, float _firingDelay)
    {
        //Create NormalAttack prefab or smthing with projectile script
        Debug.Log("Using base function: CWeapon.NormalAttack");
    }

    public virtual void ChargeAttack(int _damage, Vector2 _position, Vector2 _direction, float _firingDelay)
    {
        //Create normal attack prefab or smthing with projectile script
        Debug.Log("Using base function: CWeapon.ChargeAttack");
    }

    public virtual void SpecialAttack(int _damage, Vector2 _position, Vector2 _direction, float _firingDelay)
    {
        //Create SpecialAttack prefab or smthing with projectile script
        Debug.Log("Using base function: CWeapon.SpecialAttack");
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
}

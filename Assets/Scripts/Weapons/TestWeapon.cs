﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Weapon Format - READY
public class TestWeapon : CWeapon
{
    float R_chargeTime = 2.0f;
    float m_SPCost = 50.0f;

    public TestWeapon()
    {
        m_WeaponStats = new CWeaponStats();
        m_WeaponSprite = null;

        m_WeaponStats.Range = 10;
        m_WeaponStats.AttackMultiplier = 1;

        NormalBullet = (GameObject)Resources.Load("Projectiles/NormalNote");
        ChargeBullet = (GameObject)Resources.Load("Projectiles/PiercingNote");
        SpecialBullet = (GameObject)Resources.Load("Projectiles/NormalNote");
    }

    public override void UpdateWeapon(float _dt)
    {
        if (m_firingDelay > 0)
            m_firingDelay -= _dt;

        if (m_isCharging)
            C_chargeTime += _dt;
    }

    public override void IsCharging()
    {
        if (m_firingDelay > 0)
        {
            Debug.Log("TestWeapon.NormalAttack Called - m_firingDelay > 0");
            return;
        }
        m_isCharging = true;
    }

    public override void IsAttacking(int _damage, Vector2 _position, Vector2 _direction, float _firingDelay)
    {
        if (!m_isCharging)
            return;

        Debug.Log("TestWeapon.IsAttacking Called - m_firingDelay = " + m_firingDelay);

        if (C_chargeTime >= R_chargeTime)
            ChargeAttack(_damage, _position, _direction, _firingDelay);
        else
            NormalAttack(_damage, _position, _direction, _firingDelay);
    }

    protected override void NormalAttack(int _damage, Vector2 _position, Vector2 _direction, float _firingDelay)
    {
        //Create NormalAttack prefab or smthing with projectile script
        Debug.Log("TestWeapon.NormalAttack Called - Fired");
        GameObject newBullet = Object.Instantiate(NormalBullet, _position, Quaternion.identity);
        newBullet.GetComponent<IProjectile>().Init(_damage * m_WeaponStats.AttackMultiplier, 10, m_WeaponStats.Range, _direction, ProjectileType.Normal, AttackType.Basic);

        //"reload" weapon
        m_firingDelay = _firingDelay;

        //reset charging status
        m_isCharging = false;
        C_chargeTime = 0.0f;
    }

    protected override void ChargeAttack(int _damage, Vector2 _position, Vector2 _direction, float _firingDelay)
    {
        //Create ChargeAttack prefab or smthing with projectile script
        Debug.Log("TestWeapon.ChargeAttack Called - Fired");
        GameObject newBullet = Object.Instantiate(ChargeBullet, _position, Quaternion.identity);
        newBullet.GetComponent<IProjectile>().Init(_damage * m_WeaponStats.AttackMultiplier, 20, m_WeaponStats.Range, _direction, ProjectileType.Piercing, AttackType.Charged);

        //"reload" weapon
        m_firingDelay = _firingDelay;

        //reset charging status
        m_isCharging = false;
        C_chargeTime = 0.0f;
    }

    public override float SpecialAttack(float userSP, int _damage, Vector2 _position, Vector2 _direction, float _firingDelay)
    {
        if (userSP < m_SPCost)
            return 0.0f;

        //Create SpecialAttack prefab or smthing with projectile script
        Debug.Log("TestWeapon.SpecialAttack Called");

        GameObject newBullet = Object.Instantiate(SpecialBullet, _position, Quaternion.identity);
        newBullet.GetComponent<IProjectile>().Init(_damage * m_WeaponStats.AttackMultiplier, 20, m_WeaponStats.Range, _direction, ProjectileType.Normal, AttackType.Special);

        //"reload" weapon
        m_firingDelay = _firingDelay;

        //reset charging status
        m_isCharging = false;
        C_chargeTime = 0.0f;

        return m_SPCost;
    }
}

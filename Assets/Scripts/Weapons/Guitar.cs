using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Weapon Format - READY
public class Guitar : CWeapon
{
    float R_chargeTime = 2.0f;
    float m_SPCost = 20.0f;

    //unique
    bool m_isFiring;
    int m_firedCount;
    int m_damage;
    float m_burstFireDelay;
    float m_firingBTime;
    float m_reloadTime;

    Transform m_playerTransform;
    Vector2 m_firingDir;

    public override string Name
    {
        get { return "Guitar"; }
    }

    public Guitar()
    {
        m_WeaponStats = new CWeaponStats();
        m_WeaponSprite = null;

        m_WeaponStats.Range = 10;
        m_WeaponStats.AttackMultiplier = 1;

        m_isFiring = false;
        m_firedCount = 0;
        m_burstFireDelay = 0.3f;
        m_firingBTime = 0.0f;

        NormalBullet = (GameObject)Resources.Load("Projectiles/NormalNote");
        ChargeBullet = (GameObject)Resources.Load("Projectiles/NormalNote");
        SpecialBullet = (GameObject)Resources.Load("Projectiles/NormalNote");
    }

    public override void UpdateWeapon(float _dt)
    {
        if (m_firingDelay > 0)
            m_firingDelay -= _dt;

        if (m_isCharging)
            C_chargeTime += _dt;

        if (m_isFiring)
            BurstFire(m_damage, m_playerTransform, m_firingDir);
    }

    public override void IsCharging()
    {
        if (m_firingDelay > 0)
        {
            return;
        }
        m_isCharging = true;
    }

    public override void IsAttacking(int _damage, Transform _position, Vector2 _direction, float _firingDelay)
    {
        if (!m_isCharging)
        {
            if (m_firingDelay <= 0)
            {
                NormalAttack(_damage, _position, _direction, _firingDelay);
            }
            return;
        }

        if (C_chargeTime >= R_chargeTime)
            ChargeAttack(_damage, _position, _direction, _firingDelay);
        else
            NormalAttack(_damage, _position, _direction, _firingDelay);
    }

    protected override void NormalAttack(int _damage, Transform _transfrom, Vector2 _direction, float _firingDelay)
    {
        //Create NormalAttack prefab or smthing with projectile script
        GameObject newBullet = Object.Instantiate(NormalBullet, _transfrom.position, Quaternion.identity);
        newBullet.GetComponent<IProjectile>().Init(_damage * m_WeaponStats.AttackMultiplier, 10, m_WeaponStats.Range, _direction, ProjectileType.Normal, AttackType.Basic);

        //"reload" weapon
        m_firingDelay = _firingDelay;

        //reset charging status
        m_isCharging = false;
        C_chargeTime = 0.0f;
    }

    protected override void ChargeAttack(int _damage, Transform _transfrom, Vector2 _direction, float _firingDelay)
    {
        //Create ChargeAttack prefab or smthing with projectile script
        m_isFiring = true;
        m_damage = _damage;
        m_playerTransform = _transfrom;
        m_firingDir = _direction;
        m_reloadTime = _firingDelay;

        GameObject newBullet = Object.Instantiate(ChargeBullet, _transfrom.position, Quaternion.identity);
        newBullet.GetComponent<IProjectile>().Init(_damage * m_WeaponStats.AttackMultiplier, 20, m_WeaponStats.Range, _direction, ProjectileType.Normal, AttackType.Charged);
        m_firedCount += 1;
        m_firingBTime = 0.0f;

        //reset charging status
        m_isCharging = false;
        C_chargeTime = 0.0f;
    }

    public override float SpecialAttack(float userSP, int _damage, Transform _transfrom, Vector2 _direction, float _firingDelay)
    {
        if (userSP < m_SPCost)
            return 0.0f;

        //Create SpecialAttack prefab or smthing with projectile script
        GameObject newBullet = Object.Instantiate(SpecialBullet, _transfrom.position, Quaternion.identity);
        newBullet.GetComponent<IProjectile>().Init(_damage * m_WeaponStats.AttackMultiplier, 20, m_WeaponStats.Range, _direction, ProjectileType.Explosive, AttackType.Special);

        //"reload" weapon
        m_firingDelay = _firingDelay;

        //reset charging status
        m_isCharging = false;
        C_chargeTime = 0.0f;

        return m_SPCost;
    }

    private void BurstFire(int _damage, Transform _transfrom, Vector2 _direction)
    {
        m_firingBTime += Time.deltaTime;
        if (m_firingBTime >= m_burstFireDelay)
        {
            GameObject newBullet = Object.Instantiate(ChargeBullet, _transfrom.position, Quaternion.identity);
            newBullet.GetComponent<IProjectile>().Init(_damage * m_WeaponStats.AttackMultiplier, 20, m_WeaponStats.Range, _direction, ProjectileType.Normal, AttackType.Charged);
            m_firedCount += 1;
            m_firingBTime = 0.0f;

            if (m_firedCount >= 3)
            {
                m_firedCount = 0;
                m_firingDelay = m_reloadTime;
                m_isFiring = false;
            }
        }
    }
}

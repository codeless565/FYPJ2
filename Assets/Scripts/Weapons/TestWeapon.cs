using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWeapon : CWeapon
{
    public TestWeapon()
    {
        m_WeaponStats = new CWeaponStats();
        m_WeaponSprite = null;

        m_WeaponStats.Range = 10;
        m_WeaponStats.AttackMultiplier = 1;

        NormalBullet = (GameObject)Resources.Load("Projectiles/NormalNote");
        ChargeBullet = (GameObject)Resources.Load("Projectiles/NormalNote");
        SpecialBullet = (GameObject)Resources.Load("Projectiles/NormalNote");
    }

    public override void UpdateWeapon(float _dt)
    {
        if (firingDelay > 0)
            firingDelay -= _dt;
    }

    public override void NormalAttack(int _damage, Vector2 _position, Vector2 _direction, float _firingDelay)
    {
        if (firingDelay > 0)
        {
            Debug.Log("TestWeapon.NormalAttack Called - FiringDelay > 0");
            return;
        }
        //Create NormalAttack prefab or smthing with projectile script
        Debug.Log("TestWeapon.NormalAttack Called - Fired");
        GameObject newBullet = Object.Instantiate(NormalBullet, _position, Quaternion.identity);
        newBullet.GetComponent<NormalNote>().Init((int)(_damage * m_WeaponStats.AttackMultiplier), 10, m_WeaponStats.Range, _direction, ProjectileType.Normal, AttackType.Basic);
        firingDelay = _firingDelay;
    }

    public override void ChargeAttack(int _damage, Vector2 _position, Vector2 _direction, float _firingDelay)
    {
        //Create normal attack prefab or smthing with projectile script
        Debug.Log("TestWeapon.ChargeAttack Called");
    }

    public override void SpecialAttack(int _damage, Vector2 _position, Vector2 _direction, float _firingDelay)
    {
        //Create SpecialAttack prefab or smthing with projectile script
        Debug.Log("TestWeapon.SpecialAttack Called");
    }
}

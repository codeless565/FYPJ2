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

        NormalBullet = (GameObject)Resources.Load("/Prefabs/Projectiles/NormalNote");
        ChargeBullet = (GameObject)Resources.Load("/Prefabs/Projectiles/NormalNote");
        SpecialBullet = (GameObject)Resources.Load("/Prefabs/Projectiles/NormalNote");
    }

    public override void NormalAttack(int _damage, int _speed, Vector2 _position, Vector2 _direction, ProjectileType _projectileType, AttackType _attackType)
    {
        //Create NormalAttack prefab or smthing with projectile script
        Debug.Log("TestWeapon.NormalAttack Called");
        GameObject newBullet = Object.Instantiate(NormalBullet, _position, Quaternion.identity);
        newBullet.GetComponent<NormalNote>().Init((int)(_damage * m_WeaponStats.AttackMultiplier), _speed, m_WeaponStats.Range, _direction);
    }

    public override void ChargeAttack(int _damage, int _speed, Vector2 _position, Vector2 _direction, ProjectileType _projectileType, AttackType _attackType)
    {
        //Create normal attack prefab or smthing with projectile script
        Debug.Log("TestWeapon.ChargeAttack Called");
    }

    public override void SpecialAttack(int _damage, int _speed, Vector2 _position, Vector2 _direction, ProjectileType _projectileType, AttackType _attackType)
    {
        //Create SpecialAttack prefab or smthing with projectile script
        Debug.Log("TestWeapon.SpecialAttack Called");
    }
}

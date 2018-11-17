using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWeapon : CWeapon
{
    public TestWeapon()
    {
        m_WeaponStats.Range = 10;
        m_WeaponStats.AttackMultiplier = 1;
    }

    public override void NormalAttack(int _damage, int _speed, Vector2 _position, Vector2 _direction, ProjectileType _projectileType, AttackType _attackType)
    {
        //Create NormalAttack prefab or smthing with projectile script
        GameObject note = Object.Instantiate((GameObject)Resources.Load("/Prefabs/Projectiles/NormalNote"), _position, Quaternion.identity);
        note.GetComponent<NormalNote>().Init(_damage, _speed, m_WeaponStats.Range, _direction);
    }

    public override void ChargeAttack(int _damage, int _speed, Vector2 _position, Vector2 _direction, ProjectileType _projectileType, AttackType _attackType)
    {
        //Create normal attack prefab or smthing with projectile script
    }

    public override void SpecialAttack(int _damage, int _speed, Vector2 _position, Vector2 _direction, ProjectileType _projectileType, AttackType _attackType)
    {
        //Create SpecialAttack prefab or smthing with projectile script
    }
}

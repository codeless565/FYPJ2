using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProjectile
{
    void Init(int _damage, int _speed, int _travelDist, Vector2 _direction, ProjectileType _projectileType = ProjectileType.Normal, AttackType _attackType = AttackType.Basic);
    void CheckTravelDist();
    int GetDamage();
    int GetSpeed();
    Vector2 GetDirection();
    Sprite GetSprite();
    ProjectileType GetProjectileType();
    AttackType GetAttackType();
}

public enum ProjectileType
{
    Normal, Piercing, Explosive, Shockwave, Size
}

public enum AttackType
{
    Basic, Charged, Special, Size
}

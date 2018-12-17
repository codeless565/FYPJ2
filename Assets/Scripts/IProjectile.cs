using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProjectile
{
    void Init(float _damage, float _speed, float _travelDist, Vector2 _direction, ProjectileType _projectileType = ProjectileType.Normal, AttackType _attackType = AttackType.Basic);
    void CheckTravelDist();
    float GetDamage();
    float GetSpeed();
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

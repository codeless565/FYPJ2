using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ProjectileInterface  {
    void Create(int _damage, int _speed, Vector2 _direction);
    int GetDamage();
    int GetSpeed();
    Vector2 GetDirection();
    Sprite GetSprite();
}

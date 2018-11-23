using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEntity {
    void Init();
    void Delete();
    void IsDamaged(int damage);
    bool GetIsImmortalObject();
    CStats GetStats();
    void SetStats(int _level, int _exp, int _hp, int _attack, int _defense, float _playrate, float _movespeed);
    Sprite GetSprite();
    void SetSprite(Sprite _sprite); 
}

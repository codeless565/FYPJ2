using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEntity {
    void Init();
    void Delete();
    void IsDamaged(int damage);
    bool GetIsImmortalObject();
    CStats GetStats();
    void SetStats(int _level, float _exp, float _maxexp, float _hp, float _maxhp, float _sp, float _maxsp, int _attack, int _defense, float _playrate, float _movespeed);
    Sprite GetSprite();
    void SetSprite(Sprite _sprite); 
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface EntityInterface {
    void Spawn();
    void Delete();
    void IsDamaged(int damage);
    bool GetIsImmortalObject();
    Stats GetStats();
    void SetStats(int _level, int _exp, int _hp, int _attack, int _defense, float _playrate);
    Sprite GetSprite();
    void SetSprite(Sprite _sprite); 
}

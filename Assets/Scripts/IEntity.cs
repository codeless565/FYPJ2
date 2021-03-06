﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEntity {
    void Init();
    void Delete();
    void IsDamaged(float _IncomingDamage, AttackType _attackType = AttackType.Basic);
    bool GetIsImmortalObject();
    CStats GetStats();
    void SetStats(int _level, float _exp, float _maxexp, float _expboost, float _hp, float _maxhp, float _sp, float _maxsp, int _attack, int _defense, float _playrate, float _movespeed);
    Sprite GetSprite();
    void SetSprite(Sprite _sprite);

    bool IsInRoom
    { get; set; }

    CTRoomCoordinate RoomCoordinate
    { get; set; }
}

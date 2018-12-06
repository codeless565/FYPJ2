using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IBuff
{
    void Init(GameObject _target);
    void UpdateBuff();
    void DestoryBuff();
    void ResetBuff();

    bool Active
    { get; }

    bool isDebuff
    { get; }

    string BuffName
    { get; }

    Sprite BuffSprite
    { get; }

    string BuffDescription
    { get; }
}

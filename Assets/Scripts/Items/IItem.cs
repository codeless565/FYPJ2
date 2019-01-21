using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItem
{
    bool UseItem(ref CStats _playerStats);

    string ItemKey
    { get; }

    string ItemName
    { get; }

    string Description
    { get; }

    Sprite ItemSprite
    { get;  }
}

public enum ItemType
{
    Use, Equip
}

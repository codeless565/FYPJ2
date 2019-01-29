using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItem
{
    bool UseItem(CPlayer _player);

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

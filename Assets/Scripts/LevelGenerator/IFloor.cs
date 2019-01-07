using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFloor
{
    bool Generated
    { get; }

    string Name
    { get; set; }

    Vector2 StairsForward
    { get; }

    Vector2 StairsBack
    { get; }

    List<CTRoom> Rooms
    { get; }

    TileType[][] Tiles
    { get; }

    void InitNewLevel(int _floorNum, int _columns, int _rows, int _numRooms, int _gridSize, int _roomWidth, int _roomHeight, int _corridorLength);

}

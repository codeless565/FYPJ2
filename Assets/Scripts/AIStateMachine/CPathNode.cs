using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPathNode : MonoBehaviour
{
    private Vector2 m_position;
    private PathNodeDir m_pathDir;
    private CTRoom m_room;

    public CPathNode (float _posX, float _posY, PathNodeDir _dir, CTRoom _room)
    {
        m_position = new Vector2(_posX, _posY);
        gameObject.transform.position = m_position;
        m_pathDir = _dir;
        m_room = _room;
    }

    public void Init(float _posX, float _posY, PathNodeDir _dir, CTRoom _room)
    {
        m_position = new Vector2(_posX, _posY);
        gameObject.transform.position = m_position;
        m_pathDir = _dir;
        m_room = _room;
    }

    public Vector2 position
    { get { return m_position; } }

    public PathNodeDir direction
    { get { return m_pathDir; } }

    public CTRoom room
    { get { return m_room; } }
}

public enum PathNodeDir
{
    NorthL, NorthR, SouthL, SouthR, EastU, EastD, WestU, WestD
}

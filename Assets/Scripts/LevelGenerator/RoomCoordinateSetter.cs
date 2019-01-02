using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCoordinateSetter : MonoBehaviour {

    CTRoomCoordinate m_roomCoord;

	// Use this for initialization
	void Start () {
        if (m_roomCoord == null)
            m_roomCoord = new CTRoomCoordinate(0, 0);
    }

    public void Init(CTRoomCoordinate _coord)
    {
        m_roomCoord = _coord;
    }

    private void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.GetComponent<IEntity>() != null)
        {
            if (_other.GetComponent<IEntity>().roomCoordinate != null)
            {
                _other.GetComponent<IEntity>().roomCoordinate = m_roomCoord;
            }
            Debug.Log("Coordinate of " + " is " + _other.GetComponent<IEntity>().roomCoordinate.x + ", " + _other.GetComponent<IEntity>().roomCoordinate.y);
        }
    }
}

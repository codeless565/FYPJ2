
public class CTRoomCoordinate
{
    public int x;
    public int y;

    public CTRoomCoordinate(int _x, int _y)
    {
        x = _x;
        y = _y;
    }

    public CTRoomCoordinate(CTRoomCoordinate _mirror)
    {
        x = _mirror.x;
        y = _mirror.y;
    }

    public void setCoordinate(int _x, int _y)
    {
        x = _x;
        y = _y;
    }
}

/******************
 * General Codes
 ******************/

public enum Direction
{
    // Do not change this arrangement
    NORTH, EAST, SOUTH, WEST, Size
}

[System.Serializable]
public class IntRange
{
    public int m_Min;       // The minimum value in this range.
    public int m_Max;       // The maximum value in this range.

    // Constructor to set the values.
    public IntRange(int min, int max)
    {
        m_Min = min;
        m_Max = max;
    }

    // Get a random value from the range.
    public int Random
    {
        get { return UnityEngine.Random.Range(m_Min, m_Max); }
    }
}

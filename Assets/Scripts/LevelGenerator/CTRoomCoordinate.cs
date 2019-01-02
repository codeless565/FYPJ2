
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

    public bool sameAs(CTRoomCoordinate _otherCoord)
    {
        return _otherCoord.x == this.x && _otherCoord.y == this.y;
    }

    public Direction directionTo(CTRoomCoordinate _otherCoord)
    {
        Direction newDir = Direction.Size;
        int diffX = _otherCoord.x - x;
        int diffY = _otherCoord.y - y;

        if (diffY > 0)
        {
            if (newDir != Direction.Size)
                return Direction.Size;
            newDir = Direction.NORTH;
        }

        if (diffY < 0)
        {
            if (newDir != Direction.Size)
                return Direction.Size;
            newDir = Direction.SOUTH;
        }

        if (diffX > 0)
        {
            if (newDir != Direction.Size)
                return Direction.Size;
            newDir = Direction.EAST;
        }

        if (diffX < 0)
        {
            if (newDir != Direction.Size)
                return Direction.Size;
            newDir = Direction.WEST;
        }

        return newDir;
    }
}

/******************
 * General Codes
 ******************/
// The type of tile that will be laid in a specific position.
public enum TileType
{
    // Q1 - Q4 is based on angle theory, Q1 being the top right, proceeding to anticlockwise to Q4 being the bottom right
    None,
    Wall_Up, Wall_Down, Wall_Left, Wall_Right,
    WallOuterCorner_Q1, WallOuterCorner_Q2, WallOuterCorner_Q3, WallOuterCorner_Q4,
    WallInnerCorner_Q1, WallInnerCorner_Q2, WallInnerCorner_Q3, WallInnerCorner_Q4, 
    Floor
}

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

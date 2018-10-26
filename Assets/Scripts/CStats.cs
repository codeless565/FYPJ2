public class CStats
{
    int m_Level;
    int m_EXP;
    int m_HP;
    int m_Attack;
    int m_Defense;
    float m_PlayRate;

    public CStats()
    {
        m_Level = 0;
        m_EXP = 0;
        m_HP = 0;
        m_Attack = 0;
        m_Defense = 0;
        m_PlayRate = 0.0f;
    }
    
    public int Level
    {
        get
        {
            return m_Level;
        }
        set
        {
            m_Level = value;
        }
    }

    public int EXP
    {
        get
        {
            return m_EXP;
        }
        set
        {
            m_EXP = value;
        }
    }

    public int HP
    {
        get
        {
            return m_HP;
        }
        set
        {
            m_HP = value;
        }
    }

    public int Attack
    {
        get
        {
            return m_Attack;
        }
        set
        {
            m_Attack = value;
        }
    }

    public int Defense
    {
        get
        {
            return m_Defense;
        }
        set
        {
            m_Defense = value;
        }
    }

    public float PlayRate
    {
        get
        {
            return m_PlayRate;
        }
        set
        {
            m_PlayRate = value;
        }
    }

}

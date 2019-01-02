public class CStats
{
    int m_Level;
    float m_EXP;
    float m_MaxEXP;
    float m_EXPBoost;

    float m_HP;
    float m_MaxHP;

    float m_SP;
    float m_MaxSP;

    int m_Attack;
    int m_Defense;

    float m_PlayRate;
    float m_MovementSpeed;

    public CStats()
    {
        m_Level = 0;
        m_EXP = 0;
        m_MaxEXP = 0;
        m_EXPBoost = 0;

        m_HP = 0;
        m_MaxHP = 0;

        m_SP = 0;
        m_MaxSP = 0;

        m_Attack = 0;
        m_Defense = 0;

        m_PlayRate = 0.0f;
        m_MovementSpeed = 1.0f;
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

    public float EXP
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

    public float MaxEXP
    {
        get
        {
            return m_MaxEXP;
        }
        set
        {
            m_MaxEXP = value;
        }
    }

    public float EXPBoost
    {
        get
        {
            return m_EXPBoost;
        }
        set
        {
            m_EXPBoost = value;
        }
    }

    public float HP
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

    public float MaxHP
    {
        get
        {
            return m_MaxHP;
        }
        set
        {
            m_MaxHP = value;
        }
    }

    public float SP
    {
        get
        {
            return m_SP;
        }
        set
        {
            m_SP = value;
        }
    }

    public float MaxSP
    {
        get
        {
            return m_MaxSP;
        }
        set
        {
            m_MaxSP = value;
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
    
    public float MoveSpeed
    {
        get
        {
            return m_MovementSpeed;
        }
        set
        {
            m_MovementSpeed = value;
        }
    }
}

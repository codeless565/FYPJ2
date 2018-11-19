public class CWeaponStats
{
    float m_AttackMultiplier;
    int m_AttackRange;

    public CWeaponStats()
    {
        m_AttackMultiplier = 0;
        m_AttackRange = 0;
    }

    public float AttackMultiplier
    {
        get
        {
            return m_AttackMultiplier;
        }

        set
        {
            m_AttackMultiplier = value;
        }
    }

    public int Range
    {
        get
        {
            return m_AttackRange;
        }

        set
        {
            m_AttackRange = value;
        }
    }
}

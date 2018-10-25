using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponClass
{
    private Stats m_WeaponStats;
    private Sprite m_WeaponSprite;

    public WeaponClass()
    {
        m_WeaponStats = new Stats();
        m_WeaponSprite = null;
    }

    public void NormalAttack()
    {

    }

    public void ChargeAttack()
    {

    }

    public void SpecialAttack()
    {

    }

    public Sprite GetWeaponSprite()
    {
        return m_WeaponSprite;
    }

    public void SetWeaponSprite(Sprite _sprite)
    {
        m_WeaponSprite = _sprite;
    }
}

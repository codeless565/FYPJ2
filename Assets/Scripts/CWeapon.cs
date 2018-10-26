using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CWeapon
{
    private CStats m_WeaponStats;
    private Sprite m_WeaponSprite;

    public CWeapon()
    {
        m_WeaponStats = new CStats();
        m_WeaponSprite = null;
    }

    public void NormalAttack()
    {
        //Create NormalAttack prefab or smthing with projectile script
    }

    public void ChargeAttack()
    {
        //Create normal attack prefab or smthing with projectile script
    }

    public void SpecialAttack()
    {
        //Create SpecialAttack prefab or smthing with projectile script
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

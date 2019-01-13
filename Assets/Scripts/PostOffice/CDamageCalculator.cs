using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDamageCalculator
{
    private static CDamageCalculator instance;

    public static CDamageCalculator Instance
    {
        get
        {
            if (instance == null)
                instance = new CDamageCalculator();
            return instance;
        }
    }

    private CDamageCalculator()
    {

    }

    public bool SendDamage(GameObject _Target, GameObject _Attacker)
    {
        if (_Target == null   || _Target.GetComponent<IEntity>() == null ||
            _Attacker == null || _Attacker.GetComponent<IEntity>() == null)
            return false;

        float AttackerAtk = _Attacker.GetComponent<IEntity>().GetStats().Attack;
        float DefenderDef = _Target.GetComponent<IEntity>().GetStats().Defense;

        float FinalDMG = FinalDMGAlgo(AttackerAtk, DefenderDef);

        _Target.GetComponent<IEntity>().IsDamaged(FinalDMG);

        return true;
    }

    public bool SendDamage(GameObject _Target, float _damage)
    {
        if (_Target == null || _Target.GetComponent<IEntity>() == null)
            return false;

        float DefenderDef = _Target.GetComponent<IEntity>().GetStats().Defense;

        float FinalDMG = FinalDMGAlgo(_damage, DefenderDef);

        _Target.GetComponent<IEntity>().IsDamaged(FinalDMG);

        return true;
    }

    private float FinalDMGAlgo(float _atk, float _def)
    {
        return 100 / (100 + _def * 10) * _atk;
    }
}

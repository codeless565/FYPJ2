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

    public float CalculateDamage(float _theirDamage, float _myDefense)
    {
        return 100 / (100 + _myDefense * 10) * _theirDamage;
    }

    public float CalculateDamage(float _theirDamage, float _myDefense, float _damageMultiplier)
    {
        return 100 / (100 + _myDefense * 10) * _theirDamage * _damageMultiplier;
    }

    private float FinalDMGAlgo(float _atk, float _def)
    {
        return 100 / (100 + _def * 10) * _atk;
    }
}

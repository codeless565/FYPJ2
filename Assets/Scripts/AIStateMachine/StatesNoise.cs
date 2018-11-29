using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateNoisePatrol : CStateBase
{
    public GameObject NoiseGO;
    int counter = 0;
    int random = 0;

    public string StateID
    {
        get
        {
            return "StateNoisePatrol";
        }
    }

    public GameObject GO
    {
        get
        {
            return NoiseGO;
        }
    }

    public StateNoisePatrol(GameObject _noisego)
    {
        NoiseGO = _noisego;
    }

    public void EnterState()
    {
        Debug.Log("Entering Noise Patrol with " + NoiseGO.name);
    }

    public void UpdateState()
    {
        counter++;
        if (counter >= 30)
        {
            counter = 0;
            random = Random.Range(0, 3);
        }
        if (random == 0)
            NoiseGO.transform.position += new Vector3(NoiseGO.GetComponent<EnemyNoise>().GetStats().MoveSpeed, 0) * Time.deltaTime;
        else if (random == 1)
            NoiseGO.transform.position -= new Vector3(NoiseGO.GetComponent<EnemyNoise>().GetStats().MoveSpeed, 0) * Time.deltaTime;
        else if (random == 2)
            NoiseGO.transform.position += new Vector3(0, NoiseGO.GetComponent<EnemyNoise>().GetStats().MoveSpeed) * Time.deltaTime;
        else if (random == 3)
            NoiseGO.transform.position -= new Vector3(0, NoiseGO.GetComponent<EnemyNoise>().GetStats().MoveSpeed) * Time.deltaTime;

    }

    public void ExitState()
    {
        Debug.Log("Exiting Noise Patrol with " + NoiseGO.name);
    }
}
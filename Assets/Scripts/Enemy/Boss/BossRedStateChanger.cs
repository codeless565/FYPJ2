using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRedStateChanger : MonoBehaviour
{
    [SerializeField]
    int healthCondition = 30;   //Condition: At X percentile of HP, state will be changed

    float m_ThreshholdAmt;

    IEnemy m_OwnerEnemy;
    IStateBase NewState;

	// Use this for initialization
	void Start () {
        m_OwnerEnemy = GetComponent<IEnemy>();
        NewState = new CStateBossRageAttack(gameObject);

        m_ThreshholdAmt = m_OwnerEnemy.GetStats().MaxHP * healthCondition/100;
    }
	
	// Update is called once per frame
	void Update () {
		if (m_OwnerEnemy.GetStats().HP < m_ThreshholdAmt)
        {
            m_OwnerEnemy.StateMachine.SwapExistingState(NewState);
            m_OwnerEnemy.StateMachine.RefreshState();
            m_OwnerEnemy.GetStats().Attack = (int)(m_OwnerEnemy.GetStats().Attack * 1.5f);
            m_OwnerEnemy.GetStats().Defense = (int)(m_OwnerEnemy.GetStats().Defense * 1.5f);
            m_OwnerEnemy.GetStats().PlayRate = m_OwnerEnemy.GetStats().PlayRate * 2;
            Destroy(this);
        }
	}
}

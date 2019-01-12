using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy : IEntity
{
    CStateMachine StateMachine
    { get; }

    GameObject Target
    { get; set; }

    bool CanAttack
    { get; }

    void ResetAtkTimer();

    //this function is used to send info to Player's Monster HP bar HUD
    void IsAttackedByPlayer();

}
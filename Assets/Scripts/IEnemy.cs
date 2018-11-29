using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy : IEntity
{
    //this function is used to send info to Player's Monster HP bar HUD
    void IsAttackedByPlayer();

}
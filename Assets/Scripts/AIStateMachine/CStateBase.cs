﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface CStateBase
{
    void EnterState();
    void UpdateState();
    void ExitState();

    string StateID
    { get; }
    GameObject GO
    { get; }
}

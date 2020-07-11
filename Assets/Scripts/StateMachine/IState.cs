using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    void Enter();
    void Execute();
    void Exit();

    bool HandleMessage(Component Source, string Verb, int Data);
}

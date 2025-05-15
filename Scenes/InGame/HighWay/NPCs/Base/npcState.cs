using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public abstract class npcState
{
   
    protected virtual void Enter() {}

    public void OnExit() {Exit();}
    protected  virtual void Exit() {}

    public void OnTick() {Tick();}
    protected virtual void Tick() {}

    public void OnPhysicsTick() {PhysicsTick();}
    protected virtual void PhysicsTick() {}
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarState : npcState
{
    protected CarNPC manager;
    protected Transform transform;

    public void OnEnter(CarNPC carNPC) {this.manager = carNPC;transform = carNPC.transform; Enter();}

}

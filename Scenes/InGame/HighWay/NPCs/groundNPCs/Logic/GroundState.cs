using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundState : npcState
{
    protected GroundNPC manager;
    protected Transform transform;

    public void OnEnter(GroundNPC groundNPC) {this.manager = groundNPC;transform = groundNPC.transform; Enter();}

}

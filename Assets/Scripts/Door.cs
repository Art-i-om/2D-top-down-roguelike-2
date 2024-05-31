using System.Collections;
using System.Collections.Generic;
using Edgar.Unity.Examples;
using UnityEngine;

public class Door : InteractableBase
{
    public DoorState State;
    
    public enum DoorState
    {
        Unlocked,
        Locked,
        EnemyLocked
    }
}

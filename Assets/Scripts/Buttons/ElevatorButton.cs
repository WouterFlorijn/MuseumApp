﻿using UnityEngine;
using System.Collections;

public class ElevatorButton : BackButton
{
    public override void OnNextScene()
    {
        Museum.ToRoom(0);
        Museum.ToNextFloor();
    }

    public override bool IsActive()
    {
        return Museum.CanContinue;
    }
}
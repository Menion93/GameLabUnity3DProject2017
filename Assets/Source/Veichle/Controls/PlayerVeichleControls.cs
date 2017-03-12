﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVeichleControls : VeichleControls, ICarControls {

    CarSimulation veichle;

    protected override void MyStart()
    {
        veichle = GetComponent<CarSimulation>();
    }

    // MyUpdate is called once per frame
    protected override void MyFixedUpdate ()
    {
        Accelerate(Input.GetAxis("Accelerate"));
        Steer(Input.GetAxis("Steer"));
        Brake(Input.GetKey(KeyCode.Space));
    }

    public void Accelerate(float accelValue)
    {
        veichle.Accelerate(accelValue);
    }

    // steerValue > 0, then steer right
    // steerValue < 0, then steer left
    public void Steer(float steerDirection)
    {
        veichle.Steer(steerDirection);
    }

    public void Notify(string message)
    {
        // Do nothing 
    }

    public void Brake(bool isBraking)
    {
        veichle.Brake(isBraking);
    }
}

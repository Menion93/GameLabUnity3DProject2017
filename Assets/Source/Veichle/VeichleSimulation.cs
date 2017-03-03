﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VeichleSimulation : MonoBehaviour, IVeichleControls
{

    public float accelSpeed;
    public float brakeValue;
    public float maxTurnAngle;
    public float turnSpeed;

    public GameObject wheelModelsGo;
    public GameObject wheelColliderGo;

    IVeichleControls controller;

    WheelCollider[] wheelsColliders;
    Transform[] anteriorWheels;

    float accel;
    float brake;
    float angle;

	// Use this for initialization
	void Start ()
    {
        wheelsColliders = wheelColliderGo.GetComponentsInChildren<WheelCollider>();

        anteriorWheels = new Transform[2];

        anteriorWheels[0] = wheelModelsGo.transform.GetChild(0);
        anteriorWheels[1] = wheelModelsGo.transform.GetChild(1);
    }

    public void Accelerate(float accelValue)
    {

        if (accelValue > 0)
        {
            if (wheelsColliders[0].motorTorque >= 0)
            {
                accel = accelValue * accelSpeed;
                brake = 0;

            }
            else
            {
                accel = 0;
                brake = brakeValue;
            }
        }
        else if(accelValue < 0)
        {
            if (wheelsColliders[0].motorTorque <= 0)
            {
                accel = accelValue * accelSpeed * 0.5f;
                brake = 0;
            }
            else
            {
                brake = brakeValue;
                accel = 0;
            }
        }
        else
        {
            brake = 0;
            accel = 0;
        }

        foreach (WheelCollider wheel in wheelsColliders)
        {
            wheel.motorTorque = accel;
            wheel.brakeTorque = brake;
        }

    }

    public void Steer(float steerValue)
    {
        if(steerValue == 0)
        {
            if(angle> 0)
            {
                angle -= Time.deltaTime * maxTurnAngle * turnSpeed;
                angle = Mathf.Max(angle, 0);
            }
            else
            {
                angle += Time.deltaTime * maxTurnAngle * turnSpeed;
                angle = Mathf.Min(angle, 0);
            }

        }
        else
        {
            angle += steerValue * Time.deltaTime * maxTurnAngle * turnSpeed;
            angle = Mathf.Clamp(angle, -maxTurnAngle, maxTurnAngle);
        }

        for (int i = 0; i<2; i++)
        {
            wheelsColliders[i].steerAngle = angle;
            anteriorWheels[i].localRotation = Quaternion.Euler(0, 0, -angle);
        }


        Debug.Log(angle);
    }


    // Usefull to communicate events to the Controller
    public void Notify(string message)
    {
        throw new NotImplementedException();
    }
}

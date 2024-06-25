using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCCar : MonoBehaviour
{
    public Rigidbody body;
    public WheelColliders colliders;
    public WheelMeshes meshes;
    public float gasInput;
    public float brakeInput;
    public float steeringInput;

    public float motorPower;
    public float brakePower;
    public float slipAngle;
    public float speed;
    public AnimationCurve steeringCurve;

    public void Start()
    {
        gasInput = 1;
    }

    private void Update()
    {
        speed = body.velocity.magnitude;
        ApplyWheelPosition();
        ApplyMotor();
        ApplySteering();
        ApplyBreak();
    }

    void ApplyBreak()
    {
        colliders.FRWheel.brakeTorque = brakeInput * brakePower * 0.7f;
        colliders.FLWheel.brakeTorque = brakeInput * brakePower * 0.7f;

        colliders.RRWheel.brakeTorque = brakeInput * brakePower * 0.3f;
        colliders.RLWheel.brakeTorque = brakeInput * brakePower * 0.3f;
    }

    void ApplyMotor()
    {
        colliders.RRWheel.motorTorque = motorPower * gasInput;
        colliders.RLWheel.motorTorque = motorPower * gasInput;
    }

    void ApplySteering()
    {
        float steeringAngle = steeringInput * steeringCurve.Evaluate(speed);
        //if (slipAngle < 120f)
        //{
        //    steeringAngle += Vector3.SignedAngle(transform.forward, body.velocity + transform.forward, Vector3.up);
        //}
        //steeringAngle = Mathf.Clamp(steeringAngle, -90f, 90f);
        colliders.FRWheel.steerAngle = steeringAngle;
        colliders.FLWheel.steerAngle = steeringAngle;
    }

    void ApplyWheelPosition()
    {
        UpdateWheel(colliders.FRWheel, meshes.FRWheel);
        UpdateWheel(colliders.FLWheel, meshes.FLWheel);
        UpdateWheel(colliders.RRWheel, meshes.RRWheel);
        UpdateWheel(colliders.RLWheel, meshes.RLWheel);
    }

    void UpdateWheel(WheelCollider collider, MeshRenderer renderer)
    {
        Quaternion quat;
        Vector3 position;

        collider.GetWorldPose(out position, out quat);
        renderer.transform.position = position;
        renderer.transform.rotation = quat;
    }
}

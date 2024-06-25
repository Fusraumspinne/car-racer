using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using static UnityEngine.Rendering.DebugUI;

public class Car : MonoBehaviour
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
    
    public ParticleSystem explosionRed;
    public ParticleSystem explosionYellow;
    public MonoBehaviour carScript;

    public GameObject mobileSteeringUI;
    public bool isSteeringMobile;

    public void Start()
    {
        if (PlayerPrefs.GetInt("mobileSteering") == 0)
        {
            isSteeringMobile = false;

            mobileSteeringUI.SetActive(false);
        } 
        else
        {
            isSteeringMobile = true;

            mobileSteeringUI.SetActive(true);

            gasInput = 1;
        }
    }

    private void Update()
    {
        speed = body.velocity.magnitude;
        ApplyWheelPosition();
        CheckInput();
        ApplyMotor();
        ApplySteering();
        ApplyBreak();
    }

    void CheckInput()
    {            
        if(!isSteeringMobile)
        {
            gasInput = Input.GetAxis("Vertical");
            steeringInput = Input.GetAxis("Horizontal");
        }

        slipAngle = Vector3.Angle(transform.forward, body.velocity - transform.forward);

        if (slipAngle < 120f)
        {
            if (gasInput < 0)
            {
                brakeInput = Mathf.Abs(gasInput);
                gasInput = 0;
            }
            else
            {
                brakeInput = 0;
            }
        }
        else
        {
            brakeInput = 0;
        }
    }

    void ApplyBreak()
    {
        colliders.FRWheel.brakeTorque = brakeInput * brakePower;
        colliders.FLWheel.brakeTorque = brakeInput * brakePower;

        colliders.RRWheel.brakeTorque = brakeInput * brakePower;
        colliders.RLWheel.brakeTorque = brakeInput * brakePower;
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

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("NPC"))
        {
            if (speed > 7)
            {
                explosionRed.Play();
                explosionYellow.Play();
                carScript.enabled = false;

                StartCoroutine(PauseTime());
            }
        }
    }

    System.Collections.IEnumerator PauseTime()
    {
        yield return new WaitForSeconds(0.5f);

        Time.timeScale = 0;
    }

    public void SteeringRight()
    {
        steeringInput = 0.75f;
    }

    public void SteeringLeft()
    {
        steeringInput = -0.75f;
    }

    public void StopSteering()
    {
        steeringInput = 0; 
    }
}

[System.Serializable]
public class WheelColliders
{
    public WheelCollider FRWheel;
    public WheelCollider FLWheel;
    public WheelCollider RRWheel;
    public WheelCollider RLWheel;
}

[System.Serializable]
public class WheelMeshes
{
    public MeshRenderer FRWheel;
    public MeshRenderer FLWheel;
    public MeshRenderer RRWheel;
    public MeshRenderer RLWheel;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
public class PoliceAIV2 : MonoBehaviour
{
    public List<WheelCollider> frontWheelColliders;
    public List<WheelCollider> rearWheelColliders;
    public List<Transform> frontWheelTransformers;
    public List<Transform> rearWheelTransformers;
    public List<Vector3> frontWheelRotations;
    public List<Vector3> rearWheelRotations;

    public float motorPower = 5000f;
    public float maxSteerAngle = 25f;
    public float brakePower = 5000f;
    public float maxSpeed = 75f;

    public Transform centerOfMass;

    public GameObject brakeLight;
    public GameObject sirenLights;
    public GameObject target;

    [Header("Debug Values")]
    public float carCurrentSpeed;

    private Rigidbody rb;

    public enum State
    {
        Inactive,
        Active
    }

    public State currentState = State.Inactive;

    //Audio sound
    private AudioSource audioSource;
    public float policeAudioPitch = 0.5f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = centerOfMass.localPosition;
        
        audioSource = GetComponent<AudioSource>();

        target = GameObject.Find("Racing Car");
    }

    private float GetInput(string inputType)
    {
        var path = new NavMeshPath();

        if (!NavMesh.SamplePosition(target.transform.position, out NavMeshHit hit, 20, NavMesh.AllAreas))
            return 0f;
        if (!NavMesh.CalculatePath(transform.position, hit.position, NavMesh.AllAreas, path))
            return 0f;
        if (path.corners.Length < 2)
            return 0f;

        var localTarget = transform.InverseTransformPoint(path.corners[1]);
        var targetAngle = Mathf.Atan2(localTarget.x, localTarget.z) * Mathf.Rad2Deg;

        switch (inputType)
        {
            case "Vertical":
                if (NavMesh.Raycast(transform.position, transform.position + (rb.velocity * 10f), out hit, NavMesh.AllAreas))
                    if (Vector3.Distance(hit.position, transform.position) < rb.velocity.magnitude)
                        return carCurrentSpeed > maxSpeed * 0.4f ? -1f : 1f;
                return 1f;
            case "Horizontal":
                return targetAngle / 180f;
        }

        return 0f;
    }

    private IEnumerator Activate()
    {
        currentState = State.Active;
        startSound();
        sirenLights.SetActive(true);
        yield return new WaitForSeconds(1);
        rb.AddForce(transform.forward * 10000, ForceMode.Impulse);
    }

    public void FixedUpdate()
    {
        bool isGameOver = target.GetComponent<CarDriveV2>().getIsGameOver();
        if (currentState == State.Inactive)
            if (!isGameOver && Vector3.Distance(transform.position, target.transform.position) < 20f)
                StartCoroutine(Activate());
            else
            {
                rb.drag = 1;
                return;
            }

        if (currentState == State.Active && isGameOver)
        {
            stopSound();
            currentState = State.Inactive;
            return;
        }
        // Move the car forward if the maximum speed is not reached
        if (carCurrentSpeed < maxSpeed)
            foreach (WheelCollider Wheel in rearWheelColliders)
                Wheel.motorTorque = GetInput("Vertical") * ((motorPower * 5) / (rearWheelColliders.Count + frontWheelColliders.Count));

        // Steer the car
        var speedRatio = Mathf.Max(0, (1 - carCurrentSpeed / maxSpeed) * maxSteerAngle);
        var steerAngle = GetInput("Horizontal") * (maxSteerAngle + speedRatio);
        
        foreach (WheelCollider Wheel in frontWheelColliders)
            Wheel.steerAngle = steerAngle;
            
        rb.angularDrag = 10 / Mathf.Max(1, Mathf.Abs(GetInput("Horizontal")) * 10);
        rb.drag = -1 * Mathf.Min(0, GetInput("Vertical"));

        // Don't let the car accelerate more if the max speed is reached
        if (carCurrentSpeed > maxSpeed)
            rb.drag = 1;
        
        brakeLight.SetActive(GetInput("Vertical") < 0);

        //Changing speed of the car
        carCurrentSpeed = rb.velocity.magnitude * 3.6f;
        carCurrentSpeed = (int)carCurrentSpeed;

        if (Time.time % 2.0f < Time.deltaTime)
            if (carCurrentSpeed == 0)
                rb.AddForce(transform.forward * -10000, ForceMode.Impulse);
    }

    public void Update()
    {
        alignWheelCollidersAndMeshes();
    }

    private void alignWheelCollidersAndMeshes()
    {
        for (int i = 0; i < (frontWheelColliders.Count); i++)
        {
            frontWheelColliders[i].GetWorldPose(out var pos, out var rot);
            frontWheelTransformers[i].position = pos;
            frontWheelTransformers[i].rotation = rot * Quaternion.Euler(frontWheelRotations[i]);
        }

        for (int i = 0; i < (rearWheelColliders.Count); i++)
        {
            rearWheelColliders[i].GetWorldPose(out var pos, out var rot);
            rearWheelTransformers[i].position = pos;
            rearWheelTransformers[i].rotation = rot * Quaternion.Euler(rearWheelRotations[i]);
        }
    }

    private void startSound()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
            audioSource.pitch = policeAudioPitch;
            audioSource.minDistance = 5f;
            audioSource.maxDistance = 50f;
        }
    }

    private void stopSound()
    {
        if (audioSource.isPlaying)
            audioSource.Stop();
    }
}
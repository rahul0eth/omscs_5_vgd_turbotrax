using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Rigidbody))]
public class CarDriveV2 : MonoBehaviour
{
    public List<WheelCollider> frontWheelColliders;
    public List<WheelCollider> rearWheelColliders;
    public List<Transform> frontWheelTransformers;
    public List<Transform> rearWheelTaransformers;
    public List<Vector3> frontWheelRotations;
    public List<Vector3> rearWheelRotations;

    public float motorPower = 5000f;
    public float maxSteerAngle = 25f;
    public float brakePower = 5000f;
    public float maxSpeed = 75f;

    public Transform centerOfMass;

    public GameObject brakeLight;

    public TextMeshProUGUI speedValue;
    public TextMeshProUGUI fpsText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI gameOverLabel;
    public TextMeshProUGUI gameOverValue;
    public TextMeshProUGUI powerUpCountText;
    public TextMeshProUGUI powerDownCountText;
    public TextMeshProUGUI nitroCountText;

    public TextMeshProUGUI narrationText;
    public TextMeshProUGUI resumingText;

    public GameObject narrationScenes;

    public float pickUpTimerValue = 10f;

    [Header("Debug Values")]
    public float carCurrentSpeed;

    private Rigidbody rb;
    private float deltaTime;

    private bool timerStarted;
    private float gameTimer;
    public float timeAllowedSeconds = 300;
    private int powerUpCountValue = 0;
    private int powerDownCountValue = 0;
    private int nitroCount = 0;

    private bool isGameOver = false;
    private AudioSource carAudio;
    public GameObject mainCamera;
    public AudioClip pickUpAudio;
    public float pickUpVolume = 2f;
    public AudioClip gameOverAudio;
    public float gameOverAudioVolume = 10f;
    public AudioClip victorySound;
    public float victoryVolume = 1f;
    public AudioClip nitroSound;
    public float nitroVolume = 1f;

    public GameObject firstPoliceCar;
    public GameObject firstObstacle;
    public GameObject firstPickUp;
    public GameObject firstNitro;

    private bool isAiCarNarrationDisplayed = false;
    private bool isPickNarrationDisplayed = false;
    private bool isObstacleNarrationDisplayed = false;
    private bool isNitroNarrationDisplayed = false;

    private bool isPausedForNarration = false;

    private string AI_CAR_NARRATION = "Police! Try to evade them. You can detect their closeness from the amount of noise.";
    private string PICKUP_NARRATION = "One of them saves your time and the other takes away the time.";
    private string OBSTACLE_NARRATION = "Beware of the obstacles, although easily dodgeable, might hinder the car if hit by them!";
    private string NITRO_NARRATION = "Yay Nitro! Once picked up, use fire button to get a boost.";

    public int pauseScreenTime = 5;
    // added for nitro
    private bool isBoosting;
    private float boostDuration = 5.0f;
    private float boostTimer;
    public ParticleSystem nitroParticles;

    public bool getIsPausedForNarration()
    {
        return isPausedForNarration;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = centerOfMass.localPosition;
        timerStarted = false;
        carAudio = GetComponent<AudioSource>();
        hideGameOverSection();
        nitroParticles.Stop();
    }

    public void FixedUpdate()
    {
        if (!isGameOver)
        {
            if (carCurrentSpeed < maxSpeed)
                foreach (WheelCollider Wheel in rearWheelColliders)
                    Wheel.motorTorque = Input.GetAxis("Vertical") * ((motorPower * 10) / (rearWheelColliders.Count + frontWheelColliders.Count));

            var speedRatio = Mathf.Max(0, (1 - carCurrentSpeed / maxSpeed) * maxSteerAngle);
            var steerAngle = Input.GetAxis("Horizontal") * (maxSteerAngle + speedRatio);

            foreach (WheelCollider wheel in frontWheelColliders)
                wheel.steerAngle = steerAngle;

            if (carCurrentSpeed > 10 && steerAngle > 0)
                rb.AddForce(Quaternion.Euler(0, 90, 0) * rb.velocity * steerAngle * carCurrentSpeed / 5);

            rb.angularDrag = 10 / Mathf.Max(1, Mathf.Abs(Input.GetAxis("Horizontal")) * 10);
            rb.drag = -1 * Mathf.Min(0, Input.GetAxis("Vertical"));

            if (carCurrentSpeed > maxSpeed)
                rb.drag = 1;

            brakeLight.SetActive(Input.GetAxis("Vertical") < 0);

            carCurrentSpeed = rb.velocity.magnitude * 3.6f;
            carCurrentSpeed = (int)carCurrentSpeed;

            speedValue.text = (carCurrentSpeed) + "";
            powerUpCountText.text = powerUpCountValue + "";
            powerDownCountText.text = powerDownCountValue + "";
            nitroCountText.text = nitroCount + "";

            applyEngineSound(Input.GetAxis("Vertical"));
        }

    }

    private float pauseStartTime;
    public void Update()
    {
        if (!isGameOver)
        {
            if (isPausedForNarration)
            {
                int secondsLeft = pauseScreenTime - Mathf.FloorToInt(Time.realtimeSinceStartup - pauseStartTime);
                resumingText.text = "Resuming in " + secondsLeft + " ..";
                if (secondsLeft <= 0)
                {
                    narrationScenes.SetActive(false);
                    Time.timeScale = 1f;
                    isPausedForNarration = false;
                }

            } else
            {
                updateFrameRate();
                updateTimer();
                alignWheelCollidersAndMeshes();
                checkAndDisplayNarrations();

                // for nitro boost
                if (nitroCount>0 && Input.GetKeyDown(KeyCode.LeftControl) && !isBoosting)
                    {
                        isBoosting = true;
                        boostTimer = boostDuration;
                        rb.AddForce(transform.forward * 10000, ForceMode.Impulse);
                        nitroParticles.Play();
                        AudioSource.PlayClipAtPoint(nitroSound, transform.position, nitroVolume);
                        nitroCount -= 1;
                    }

                if (isBoosting)
                    {
                        if (boostTimer > 0)
                        {
                            // carCurrentSpeed += 25f;
                            maxSpeed = 150f;
                            boostTimer -= Time.deltaTime;
                        }
                        else
                        {
                            maxSpeed = 75f;
                            isBoosting = false;
                            nitroParticles.Stop();
                        }
                    }
            }
        }
        
    }

    private void checkAndDisplayNarrations()
    {
        if (!isAiCarNarrationDisplayed)
        {
            if (Vector3.Distance(transform.position, firstPoliceCar.transform.position) < 20f)
            {
                isAiCarNarrationDisplayed = true;
                isPausedForNarration = true;
                narrationText.text = AI_CAR_NARRATION;
                narrationText.color = new Color(0, 255, 255);
                narrationScenes.SetActive(true);
                Time.timeScale = 0f;
                pauseStartTime = Time.realtimeSinceStartup;
            }
        }

        if (!isPickNarrationDisplayed)
        {
            if (Vector3.Distance(transform.position, firstPickUp.transform.position) < 20f)
            {
                isPickNarrationDisplayed = true;
                isPausedForNarration = true;
                narrationText.text = PICKUP_NARRATION;
                narrationText.color = new Color(0, 255, 255);
                narrationScenes.SetActive(true);
                Time.timeScale = 0f;
                pauseStartTime = Time.realtimeSinceStartup;
            }
        }

        if (!isObstacleNarrationDisplayed)
        {
            if (Vector3.Distance(transform.position, firstObstacle.transform.position) < 20f)
            {
                isObstacleNarrationDisplayed = true;
                isPausedForNarration = true;
                narrationText.text = OBSTACLE_NARRATION;
                narrationText.color = new Color(0, 255, 255);
                narrationScenes.SetActive(true);
                Time.timeScale = 0f;
                pauseStartTime = Time.realtimeSinceStartup;
            }
        }

        if (!isNitroNarrationDisplayed)
        {
            if (Vector3.Distance(transform.position, firstNitro.transform.position) < 15f)
            {
                isNitroNarrationDisplayed = true;
                isPausedForNarration = true;
                narrationText.text = NITRO_NARRATION;
                narrationText.color = new Color(0, 255, 255);
                narrationScenes.SetActive(true);
                Time.timeScale = 0f;
                pauseStartTime = Time.realtimeSinceStartup;
            }
        }
    }
    private void applyEngineSound(float verticalInput)
    {
        var engineMaxPitch = 0.4f;
        var inputVariablePitch = 0.3f;

        var pitchFromCar = Mathf.Clamp(carCurrentSpeed/maxSpeed, 0f, engineMaxPitch);
        var pitchFromInput = Mathf.Abs(verticalInput) * inputVariablePitch;

        carAudio.pitch = pitchFromCar + pitchFromInput;        
    }

    private void alignWheelCollidersAndMeshes()
    {
        for (int i = 0; i < (rearWheelColliders.Count); i++)
        {
            rearWheelColliders[i].GetWorldPose(out var pos, out var rot);
            rearWheelTaransformers[i].position = pos;
            rearWheelTaransformers[i].rotation = rot * Quaternion.Euler(rearWheelRotations[i]);
        }

        for (int i = 0; i < (frontWheelColliders.Count); i++)
        {
            frontWheelColliders[i].GetWorldPose(out var pos, out var rot);
            frontWheelTransformers[i].position = pos;
            frontWheelTransformers[i].rotation = rot * Quaternion.Euler(frontWheelRotations[i]);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            AudioSource.PlayClipAtPoint(pickUpAudio, transform.position, pickUpVolume);
            other.gameObject.SetActive(false);
            // maxSpeed = maxSpeed + 15f;
            gameTimer -= pickUpTimerValue;
            powerUpCountValue += 1;
        }

        if (other.gameObject.CompareTag("Pickup_bad"))
        {
            AudioSource.PlayClipAtPoint(pickUpAudio, transform.position, pickUpVolume);
            other.gameObject.SetActive(false);
            // maxSpeed = maxSpeed - 15f;
            gameTimer += pickUpTimerValue;
            powerDownCountValue += 1;
        }

        if (other.gameObject.CompareTag("Finish_line"))
        {
            timerStarted = false;
            showGameOverSection(true);
            isGameOver = true;
            playGameOverAudio(true);
        }            
        
        if (other.gameObject.CompareTag("Start_line"))
            timerStarted = true;

        if (other.gameObject.CompareTag("Nitro"))
            {
                AudioSource.PlayClipAtPoint(pickUpAudio, transform.position, pickUpVolume);
                other.gameObject.SetActive(false);
                nitroCount += 1;
            }
    }

    private void updateFrameRate()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        if (fps < 0) {
            fps = 0;
        }
        if (fps > 999) {
            fps = 999;
        }
        fpsText.text = Mathf.Ceil(fps).ToString();
    }

    private void updateTimer()
    {
        if (timerStarted == true)
            gameTimer += Time.deltaTime;
        
        float timeLeft = Mathf.Max(timeAllowedSeconds - gameTimer, 0);

        float minutes = Mathf.Floor(timeLeft / 60);
        float seconds = timeLeft % 60;
        string timerTextValue =  minutes.ToString("00") + ":" + Mathf.RoundToInt(seconds).ToString("00");

        timerText.text = timerTextValue;

        if (timeLeft <= 0)
        {
            isGameOver = true;
            showGameOverSection(false);
            playGameOverAudio(false);
        }
    }

    private void hideGameOverSection()
    {
        gameOverLabel.enabled = false;
        gameOverValue.enabled = false;
    }

    private void showGameOverSection(bool isWon)
    {
        gameOverLabel.enabled = true;
        gameOverValue.enabled = true;

        string gameOverStatus = isWon ? "You Won!" : "You Lose!";

        gameOverValue.text = gameOverStatus;

        if (isWon)
            gameOverValue.color = new Color(00, 230, 00, 255);
        else
            gameOverValue.color = new Color(222, 00, 200, 255);
    }

    private void playGameOverAudio(bool isAWin)
    {
        carAudio.Stop();
        mainCamera.GetComponent<AudioSource>().Stop();
        if (isAWin)
        {
            AudioSource.PlayClipAtPoint(victorySound, transform.position, victoryVolume);
        } else
        {
            AudioSource.PlayClipAtPoint(gameOverAudio, transform.position, gameOverAudioVolume);
        }
    }

    public bool getIsGameOver()
    {
        return isGameOver;
    }
}
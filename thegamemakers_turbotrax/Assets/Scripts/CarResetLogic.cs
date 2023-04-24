using TMPro;
using UnityEngine;

public class CarResetLogic : MonoBehaviour
{
    private Vector3 lastValidPosition;
    private Quaternion lastValidRotation;
    private Rigidbody rb;

    public TextMeshProUGUI narrationText;
    public TextMeshProUGUI resumingText;

    public GameObject narrationScenes;

    private void Start()
    {
        lastValidPosition = transform.position;
        rb = GetComponent<Rigidbody>();
    }

    private float pauseStartTime;
    private bool isPausedForNarration = false;

    void Update()
    {
        if (isPausedForNarration)
        {
            int secondsLeft = 2 - Mathf.FloorToInt(Time.realtimeSinceStartup - pauseStartTime);
            if (secondsLeft <=0 )
            {
                isPausedForNarration = false;
                narrationScenes.SetActive(false);
                Time.timeScale = 1f;
                transform.SetPositionAndRotation(lastValidPosition, lastValidRotation);
                rb.velocity = Vector3.zero;
            }
            return;
        }
        if (Time.time % 5.0f < Time.deltaTime)
        {
            if (!IsCarOnRoad())
            {
                if (narrationScenes!= null)
                {
                    narrationText.text = "You went off the road, resetting the car...";
                    resumingText.text = "";
                    narrationScenes.SetActive(true);
                    isPausedForNarration = true;
                    Time.timeScale = 0f;
                    pauseStartTime = Time.realtimeSinceStartup;
                    return;
                }
                transform.SetPositionAndRotation(lastValidPosition, lastValidRotation);
                rb.velocity = Vector3.zero;
            }
            else if (Time.time % 10.0f < Time.deltaTime)
            {
                lastValidPosition = transform.position;
                lastValidRotation = transform.rotation;
            }
        }
    }

    private bool IsCarOnRoad()
    {
        var hits = Physics.RaycastAll(this.transform.position, Vector3.down, 5);

        foreach (RaycastHit hit in hits)
            if (hit.collider.gameObject.CompareTag("Road"))
                return true;

        return false;
    }
}

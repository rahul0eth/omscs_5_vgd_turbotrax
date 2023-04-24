using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalRotation : MonoBehaviour
{
    public GameObject target;
    public float rotationSpeed = 5f;
    public float distanceToActivate = 50f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, target.transform.position) < distanceToActivate)
        {
            transform.Rotate(new Vector3(0, 30, 0) * rotationSpeed * Time.deltaTime, Space.World);
        }
    }
}

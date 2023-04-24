using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningBlock : MonoBehaviour
{
    public GameObject target;
    public float rotationSpeed = 10f;
    public float distanceToActivate = 50f;
    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, target.transform.position) < distanceToActivate)
        {
            transform.Rotate(new Vector3(0, 0, -30) * Time.deltaTime * rotationSpeed);
        }
    }
}

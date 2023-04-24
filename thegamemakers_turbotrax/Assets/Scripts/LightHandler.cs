using UnityEngine;

public class LightHandler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player Car"))
            GetComponent<Light>().renderMode = LightRenderMode.ForcePixel;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player Car"))
            GetComponent<Light>().renderMode = LightRenderMode.Auto;
    }
}

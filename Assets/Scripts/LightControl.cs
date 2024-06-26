using System.Collections;
using UnityEngine;

public class LightControl : MonoBehaviour
{
    public Light[] lights; // Array to hold references to the lights
    public float delayBeforeTurningOn = 3.0f; // Delay in seconds before turning the lights back on

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Ensure the player has the tag "VRPlayer"
        {
            foreach (Light light in lights)
            {
                light.gameObject.SetActive(false); // Disable the lights completely
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // Ensure the player has the tag "VRPlayer"
        {
            StartCoroutine(TurnLightsOnAfterDelay());
        }
    }

    private IEnumerator TurnLightsOnAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeTurningOn);
        foreach (Light light in lights)
        {
            light.gameObject.SetActive(true); // Enable the lights
        }
    }
}

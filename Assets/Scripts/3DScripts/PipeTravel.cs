using UnityEngine;
using System.Collections;

public class PipeTravel : MonoBehaviour
{
    public Transform exitPoint;
    public float travelTime = 1.5f;

    private bool isTravelling = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isTravelling)
        {
            StartCoroutine(TravelThroughPipe(other.gameObject));
        }
    }

    private IEnumerator TravelThroughPipe(GameObject player)
    {
        isTravelling = true;
        player.GetComponent<PlayerMovement>().enabled = false; // optional: freeze movement

        // Optional: play animation or hide player
        player.SetActive(false);

        yield return new WaitForSeconds(travelTime);

        player.transform.position = exitPoint.position;

        player.SetActive(true);
        player.GetComponent<PlayerMovement>().enabled = true;

        isTravelling = false;
    }
}


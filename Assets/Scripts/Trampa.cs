using UnityEngine;

public class Trampa : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealthSystem.Instance?.TakeDamage(3);
        }
    }
}


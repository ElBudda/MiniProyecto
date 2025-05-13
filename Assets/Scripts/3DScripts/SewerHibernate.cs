using UnityEngine;

public class SewerHibernate : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TryHibernate();
        }
    }

    void TryHibernate()
    {
        if (PlayerFoodSystem.Instance.CanHibernate())
        {
            Debug.Log("Hibernating... Day Passed!");
            PlayerFoodSystem.Instance.ResetFoodPoints();
            PlayerHealthSystem.Instance.FullHeal();

            // TODO: Trigger day/night cycle advance here
        }
        else
        {
            Debug.Log("Not enough food to survive! Need more.");
            // TODO: Show UI message to player
        }
    }
}


using UnityEngine;
using TMPro;
using System.Collections;

public class SewerHibernate : MonoBehaviour
{
    [Header("References")]
    public Transform sewerExitPoint;
    public TMP_Text interactionPrompt;
    public Transform player;
    public float interactionRange = 2.5f;

    private bool inRange = false;

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(player.position, transform.position);
        inRange = distance <= interactionRange;

        if (inRange)
        {
            interactionPrompt.gameObject.SetActive(true);
            UpdatePrompt();

            if (Input.GetKeyDown(KeyCode.E))
            {
                TryHibernate();
            }
        }
        else
        {
            interactionPrompt.gameObject.SetActive(false);
        }
    }

    void UpdatePrompt()
    {
        if (!LightingManager.Instance.IsNight())
        {
            interactionPrompt.text = "Sewer is closed until night.";
        }
        else if (!PlayerFoodSystem.Instance.CanHibernate())
        {
            int needed = PlayerFoodSystem.Instance.requiredFoodPoints - PlayerFoodSystem.Instance.currentFoodPoints;
            interactionPrompt.text = $"You need {needed} more food to hibernate.";
        }
        else
        {
            interactionPrompt.text = "Press E to hibernate.";
        }
    }

    void TryHibernate()
    {
        if (!LightingManager.Instance.IsNight())
        {
            Debug.Log("❌ Sewer is closed during the day!");
            return;
        }

        if (PlayerFoodSystem.Instance.CanHibernate())
        {
            Debug.Log("✅ Hibernating...");
            StartCoroutine(HibernateSequence());
        }
        else
        {
            Debug.Log("❌ Not enough food to hibernate! Need more.");
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }
    IEnumerator HibernateSequence()
    {
        yield return FadeScreen.Instance.FadeOut();

        PlayerFoodSystem.Instance.AdvanceDay();
        PlayerHealthSystem.Instance.FullHeal();
        LightingManager.Instance.TimeOfDay = 6f;
        LightingManager.Instance.hasHibernatedToday = true;
        ItemRespawner.Instance?.RespawnAllItems();

        Debug.Log($"✅ New Day {PlayerFoodSystem.Instance.dayNumber} started!");

        if (sewerExitPoint != null && player != null)
        {
            player.position = sewerExitPoint.position;
        }

        yield return FadeScreen.Instance.FadeIn();

        interactionPrompt.gameObject.SetActive(false);
    }
}




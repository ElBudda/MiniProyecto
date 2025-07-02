using TMPro;
using UnityEngine;

public class PlayerHealthUI : MonoBehaviour
{
    public TMP_Text healthText;

    void Update()
    {
        healthText.text = $"Health: {PlayerHealthSystem.Instance.currentHealth}/{PlayerHealthSystem.Instance.maxHealth}";
    }
}


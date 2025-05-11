using UnityEngine;

public class InventoryToggle : MonoBehaviour
{
    public GameObject inventoryPanel;
    private bool isInventoryOpen = false;

    void Start()
    {
        // Make sure inventory is hidden at start
        inventoryPanel.SetActive(false);
    }

    void Update()
    {
        // Toggle inventory when Tab key is pressed
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleInventory();
        }
    }

    void ToggleInventory()
    {
        isInventoryOpen = !isInventoryOpen;
        inventoryPanel.SetActive(isInventoryOpen);

        // Optionally slow time when inventory is open
        Time.timeScale = isInventoryOpen ? 0.1f : 1f;
    }
}

using UnityEngine;

public class Item : MonoBehaviour
{
    public string itemName;
    public Sprite icon; // optional for inventory UI
    public bool isCollectible = true;


    public void OnCollect()
    {
        Debug.Log("Collected: " + itemName);
        // Hide item or destroy if needed
        gameObject.SetActive(false);
    }
}

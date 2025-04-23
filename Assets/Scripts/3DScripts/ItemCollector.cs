using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    public float interactRange = 1.5f;
    public KeyCode interactKey = KeyCode.E;

    private List<Item> nearbyItems = new List<Item>();

    void Update()
    {
        if (Input.GetKeyDown(interactKey))
        {
            TryCollect();
        }
    }

    void TryCollect()
    {
        if (nearbyItems.Count == 0) return;

        Item closestItem = null;
        float closestDist = Mathf.Infinity;

        foreach (Item item in nearbyItems)
        {
            if (item == null) continue;
            float dist = Vector3.Distance(transform.position, item.transform.position);
            if (dist < closestDist)
            {
                closestDist = dist;
                closestItem = item;
            }
        }

        if (closestItem != null)
        {
            closestItem.OnCollect();
            nearbyItems.Remove(closestItem); // Optional: remove it after pickup
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Item item = other.GetComponent<Item>();
        if (item != null && item.isCollectible)
        {
            nearbyItems.Add(item);
        }
    }

    void OnTriggerExit(Collider other)
    {
        Item item = other.GetComponent<Item>();
        if (item != null)
        {
            nearbyItems.Remove(item);
        }
    }
}

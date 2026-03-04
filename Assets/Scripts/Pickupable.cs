using UnityEngine;

public class Pickupable : MonoBehaviour
{
    public bool isPickedUp = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isPickedUp)
        {
            PlayerInventory inventory = other.GetComponent<PlayerInventory>();
            if (inventory != null)
            {
                inventory.PickupItem(this);
            }
        }
    }
}
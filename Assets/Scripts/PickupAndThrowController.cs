using UnityEngine;

public class PickupAndThrowController : MonoBehaviour
{
    [SerializeField] private Transform inventoryHolder;
    [SerializeField] private GameObject playerCamera;
    
    private GameObject currentItem;
    private bool canPickUp;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryPickup();
        }

        if (Input.GetMouseButtonDown(0))
        {
            TryDrop();
        }
    }

    private void TryPickup()
    {
        var ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);

        if (!Physics.Raycast(ray, out var hit, 10f)) return;
        if (!hit.transform.CompareTag("Interactable")) return;
        if (canPickUp)
        {
            TryDrop();
        }

        PickUpItem(hit.transform.gameObject);
    }

    private void TryDrop()
    {
        if (currentItem != null)
        {
            DropItem();
        }
    }

    private void PickUpItem(GameObject item)
    {
        currentItem = item;
        var itemRigidbody = currentItem.GetComponent<Rigidbody>();
        
        itemRigidbody.isKinematic = true;
        
        currentItem.transform.parent = inventoryHolder;
        currentItem.transform.localPosition = Vector3.zero;
        currentItem.transform.localEulerAngles = new Vector3(10f, 0f, 0f);
        canPickUp = true;
    }

    private void DropItem()
    {
        currentItem.transform.parent = null;

        var itemRigidbody = currentItem.GetComponent<Rigidbody>();
        
        itemRigidbody.isKinematic = false;
        itemRigidbody.AddForce(transform.forward * 300);
        
        canPickUp = false;
        currentItem = null;
    }
}

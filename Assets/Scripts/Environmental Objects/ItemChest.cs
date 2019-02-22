using UnityEngine;

public class ItemChest : MonoBehaviour
{
    [SerializeField] Inventory inventory = null;
    [SerializeField] ItemGenerator itemGenerator = null;
    public Item item;
    private Color originalColor;

    private bool isInRange;
    private bool isHovering;
    private bool hasClicked;
    private bool isEmpty;

    private void OnTriggerEnter(Collider other) {
        if (hasClicked && !isEmpty) {
            Debug.Log("Opened chest: " + this);
            //Item item = itemGenerator.GenerateRandomEquippableItem();
            inventory.AddItem(item.GetCopy());
            isEmpty = true;
            hasClicked = false;
        }

        isInRange = true;
    }

    private void OnTriggerExit(Collider other) {
        isInRange = false;
    }

    private void OnMouseEnter() {
        originalColor = GetComponent<Renderer>().material.color;
        GetComponent<Renderer>().material.color = Color.yellow;
        isHovering = true;
    }

    private void OnMouseExit() {
        GetComponent<Renderer>().material.color = originalColor;
        isHovering = false;
    }

    private void OnMouseDown() {
        if (isInRange) {
            if (!isEmpty) {
                Debug.Log("Opened chest: " + this);
                //Item item = itemGenerator.GenerateRandomEquippableItem();
                inventory.AddItem(item.GetCopy());
                isEmpty = true;
            }
        }

        if (isHovering) {
            hasClicked = true;
        }
    }
}

using UnityEngine;

public class UserInterfaceController : MonoBehaviour
{
    [SerializeField] GameObject inventoryGameObject = null;
    [SerializeField] KeyCode[] toggleInventoryKeys = null;
    
    void Update()
    {
        for (int i = 0; i < toggleInventoryKeys.Length; i++) {
            if (Input.GetKeyDown(toggleInventoryKeys[i])) {
                inventoryGameObject.SetActive(!inventoryGameObject.activeSelf);
                break;
            }
        }
    }
}

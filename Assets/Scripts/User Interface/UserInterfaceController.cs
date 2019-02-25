using UnityEngine;

public class UserInterfaceController : MonoBehaviour
{
    public static UserInterfaceController instance;

    [SerializeField] GameObject characterPanelObject = null;
    [SerializeField] KeyCode[] toggleInventoryKeys = null;

    public bool isInventoryOpen = false;

    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(this);
        } else {
            Destroy(this);
        }
    }
    
    void Update()
    {
        for (int i = 0; i < toggleInventoryKeys.Length; i++) {
            if (Input.GetKeyDown(toggleInventoryKeys[i])) {
                characterPanelObject.SetActive(!characterPanelObject.activeSelf);
                isInventoryOpen = !isInventoryOpen;
                break;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemDrop : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    private Image imageOverHead;
    private Text textOverHead;
    private Transform itemTransform;

    public Shader shader;
    private Shader oldShader;
    private Material tempMaterial;

    public EquippableItem item;
    public GameObject characterObject;


    // Start is called before the first frame update
    void Awake()
    {
        imageOverHead = GetComponentInChildren<Image>();
        textOverHead = GetComponentInChildren<Text>();
        itemTransform = this.transform;

        tempMaterial = new Material(this.GetComponent<Renderer>().sharedMaterial);
        oldShader = tempMaterial.shader;

        item = GameManager.instance.itemGenerator.GenerateRandomEquippableItem();
        textOverHead.text = item.displayName;

        UpdateTextPosition();
    }

    void Start() {
        switch(item.rarity) {
            case ItemRarity.Normal:
                imageOverHead.color = new Color32(255, 255, 255, 100);
                break;
            case ItemRarity.Magic:
                imageOverHead.color = new Color32(0, 106, 255, 100);
                break;
            case ItemRarity.Rare:
                imageOverHead.color = new Color32(255, 255, 0, 100);
                break;
            case ItemRarity.Legendary:
                imageOverHead.color = new Color32(255, 130, 0, 100);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTextPosition();
    }

    private void UpdateTextPosition() {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(itemTransform.position);
        screenPos.y += 10;
        imageOverHead.transform.position = screenPos;
    }

    public void OnPointerEnter(PointerEventData pointerEventData) {
        tempMaterial.shader = shader;
        this.GetComponent<Renderer>().sharedMaterial = tempMaterial;
    }

    public void OnPointerExit(PointerEventData pointerEventData) {
        tempMaterial.shader = oldShader;
        this.GetComponent<Renderer>().sharedMaterial = tempMaterial;
    }

    public void OnPointerDown(PointerEventData pointerEventData) {
        CharacterMovement movement = characterObject.GetComponent<CharacterMovement>();
        movement.targetObject = this.gameObject;
        movement.pickingUpItem = true;
    }
}
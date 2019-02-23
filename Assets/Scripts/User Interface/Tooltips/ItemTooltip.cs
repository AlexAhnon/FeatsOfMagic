using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ItemTooltip : MonoBehaviour
{
    [SerializeField] Text itemNameText = null;
    [SerializeField] Text itemTypeText = null;
    [SerializeField] Text itemDescriptionText = null;

    public void ShowTooltip(Item item) {
        itemNameText.text = item.displayName;
        itemTypeText.text = item.GetItemType();

        //itemDescriptionText.text = sb.ToString();
        itemDescriptionText.text = item.GetDescription();

        if (item is EquippableItem) {
            EquippableItem equippableItem = (EquippableItem)item;
            switch(equippableItem.rarity) {
                case ItemRarity.Normal:
                    this.GetComponent<Image>().color = new Color32(255, 255, 255, 100);
                    break;
                case ItemRarity.Magic:
                    this.GetComponent<Image>().color = new Color32(0, 106, 255, 100);
                    break;
                case ItemRarity.Rare:
                    this.GetComponent<Image>().color = new Color32(255, 255, 0, 100);
                    break;
                case ItemRarity.Legendary:
                    this.GetComponent<Image>().color = new Color32(255, 130, 0, 100);
                    break;
            }
        }

        gameObject.SetActive(true);
    }

    public void HideTooltip() {
        gameObject.SetActive(false);
    }

    
}

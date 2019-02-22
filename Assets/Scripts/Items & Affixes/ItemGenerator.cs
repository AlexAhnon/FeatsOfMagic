using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    public ItemDatabase baseEquipment;
    public AffixDatabase sharedAffixes;
    public AffixDatabase armourAffixes;
    public AffixDatabase oneHandedMeleeAffixes;

    public EquippableItem GenerateRandomEquippableItem() {
        List<Item> items = baseEquipment.items;

        // Get a random base item from the database.
        int randomNum = UnityEngine.Random.Range(0, items.Count);

        // Instatiate a copy of that item.
        EquippableItem item = Instantiate((EquippableItem)items[randomNum]);

        // Set random rarity.
        // NOTE: Remember to add one extra to upper limit since (int) version of Random.Range doesn't round up to limit.
        item.rarity = (ItemRarity)UnityEngine.Random.Range(1, 4+1);

        // Get a random amount of affixes depending on rarity.
        switch(item.rarity) {
            case ItemRarity.Normal:
                //Debug.Log(item.rarity + " " + item.name);
                return item;
            case ItemRarity.Magic:
                randomNum = UnityEngine.Random.Range(1, 2+1);
                //Debug.Log(item.rarity + " " + item.name);
                break;
            case ItemRarity.Rare:
                randomNum = UnityEngine.Random.Range(3, 4+1);
                //Debug.Log(item.rarity + " " + item.name);
                break;
            case ItemRarity.Legendary:
                randomNum = UnityEngine.Random.Range(5, 6+1);
                //Debug.Log(item.rarity + " " + item.name);
                break;
        }

        switch(item.type) {
            case ItemType.Helm:
                item.affixes = GetListOfRandomAffixes(randomNum, sharedAffixes.affixes, armourAffixes.affixes);
                break;
            case ItemType.Chest:
                item.affixes = GetListOfRandomAffixes(randomNum, sharedAffixes.affixes, armourAffixes.affixes);
                break;
            case ItemType.OneHandedAxe:
                item.affixes = GetListOfRandomAffixes(randomNum, sharedAffixes.affixes, oneHandedMeleeAffixes.affixes);
                break;
        }

        return item;
    }

    public List<ItemAffix> GetListOfRandomAffixes(int amount, List<ItemAffix> firstAffixList, List<ItemAffix> secondAffixList) {
        List<ItemAffix> affixList = new List<ItemAffix>(firstAffixList);
        affixList.AddRange(secondAffixList);

        List<ItemAffix> temp = new List<ItemAffix>();

        //for (int i = 0; i <= amount; i++) {
        while(temp.Count < amount) {
            if(temp.Count == 0) {
                temp.Add(GetRandomAffix(affixList));
            } else {
                // We don't want the same type of affix to appear twice, so check through the affixes to make sure.
                ItemAffix affix = GetRandomAffix(affixList);
                
                bool hasAffix = false;

                for (int j = 0; j < temp.Count; j++) {
                    if (temp[j].id == affix.id && temp[j].modifierType == affix.modifierType) {
                        hasAffix = true;
                    }
                }

                if (!hasAffix) {
                    temp.Add(affix);
                }
            }
        }

        return temp;
    }

    public ItemAffix GetRandomAffix(List<ItemAffix> affixes) {
        ItemAffix temp = Instantiate(affixes[UnityEngine.Random.Range(0, affixes.Count)]);

        if (temp.modifierType == StatModifierType.Flat) {
            temp.value = UnityEngine.Random.Range((int)temp.minValue, (int)temp.maxValue + 1);
        } else if (temp.modifierType == StatModifierType.PercentMulti) {
            temp.value = UnityEngine.Random.Range(temp.minValue, temp.maxValue);

            // Get only the first two decimals.
            temp.value = Truncate(temp.value, 2);
        }

        return temp;
    }

    public float Truncate(float value, int digits)
    {
        double mult = Math.Pow(10.0, digits);
        double result = Math.Truncate( mult * value ) / mult;
        return (float) result;
    }
}

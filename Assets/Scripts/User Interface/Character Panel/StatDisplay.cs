using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StatDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private CharacterStat _stat;
    public CharacterStat stat {
        get {return _stat; }
        set {
            _stat = value;
            UpdateStatValue();
        }
    }

    private string _statName;
    public string statName {
        get { return _statName; }
        set {
            _statName = value;
            nameText.text = _statName;
        }
    }

    public Text nameText;
    public Text valueText;
    [SerializeField] StatTooltip tooltip;

    private void OnValidate()
    {
        Text[] texts = GetComponentsInChildren<Text>();
        nameText = texts[0];
        valueText = texts[1];

        if (tooltip == null) {
            tooltip = FindObjectOfType<StatTooltip>();
        }
    }

    public void UpdateStatValue() {
        valueText.text = _stat.value.ToString();
    }

    public void OnPointerEnter(PointerEventData eventData) {
        tooltip.ShowTooltip(stat, statName);
    }

    public void OnPointerExit(PointerEventData eventData) {
        tooltip.HideTooltip();
    }
}

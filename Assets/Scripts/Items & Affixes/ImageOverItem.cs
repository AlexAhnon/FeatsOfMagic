using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageOverItem : MonoBehaviour
{
    private Image imageOverHead;
    private Transform itemObject;
    public Shader shader;
    public Shader oldShader;

    // Start is called before the first frame update
    void OnValidate()
    {
        imageOverHead = GetComponentInChildren<Image>();
        itemObject = this.transform;
        oldShader = this.GetComponent<Renderer>().sharedMaterial.shader;
        UpdateTextPosition();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTextPosition();
    }

    private void UpdateTextPosition() {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(itemObject.position);
        screenPos.y += 10;
        imageOverHead.transform.position = screenPos;
    }

    private void OnMouseOver() {
        this.GetComponent<Renderer>().sharedMaterial.shader = shader;
    }

    private void OnMouseExit() {
        this.GetComponent<Renderer>().sharedMaterial.shader = oldShader;
    }
}
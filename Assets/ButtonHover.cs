using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonHover : MonoBehaviour
{

    private Button pb;
    public Sprite hoverSprite;
    public Sprite noHoverSprite;

    void Start()
    {
        pb = GetComponent<Button>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        pb.image.sprite = hoverSprite;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        pb.image.sprite = noHoverSprite;
    }

}

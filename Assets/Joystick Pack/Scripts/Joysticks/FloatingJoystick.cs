using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FloatingJoystick : Joystick
{
    protected override void Start()
    {
        base.Start();
        var tempColor = image.color;
        tempColor.a = .4f;
        image.color = tempColor;
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
        background.gameObject.SetActive(true);
        var tempColor = image.color;
        tempColor.a = 1f;
        image.color = tempColor;
        base.OnPointerDown(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        var tempColor = image.color;
        tempColor.a = .4f;
        image.color = tempColor;
        base.OnPointerUp(eventData);
    }
}
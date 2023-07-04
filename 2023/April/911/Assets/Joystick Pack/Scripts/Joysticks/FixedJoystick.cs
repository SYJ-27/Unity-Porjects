using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class FixedJoystick : Joystick
{
    public override void OnPointerDown(PointerEventData eventData)
    {
        Car.isPointerDown = false;
        base.OnPointerDown(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        Car.isPointerDown = true;
        base.OnPointerUp(eventData);
    }
}
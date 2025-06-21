using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerFloorIntersect : MonoBehaviour
{
    public GameObject floorIntersectObject;
    public float floorOpenDuration;
    public Transform floorIntersectOpenPos;
    private Vector3 floorIntersectOriginalPos;
    public bool isPressed = false;

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(isPressed) return;
            FloorIntersectTriggered();
            isPressed = true;
        }
    }
    public void FloorIntersectTriggered()
    {
        floorIntersectOriginalPos = floorIntersectObject.transform.position;
        LeanTween.moveLocal(floorIntersectObject, floorIntersectOpenPos.position, 0.9f).setEaseOutQuad();
        LeanTween.moveLocal(floorIntersectObject, floorIntersectOriginalPos, 0.9f).setEaseInQuad().setDelay(floorOpenDuration);
        LeanTween.delayedCall(floorOpenDuration, ResetIsPressed);
    }

    public void ResetIsPressed()
    {
        isPressed = false;
    }
}

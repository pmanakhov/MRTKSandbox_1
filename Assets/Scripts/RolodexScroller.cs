using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine;

public class RolodexScroller : MonoBehaviour, IMixedRealityPointerHandler
{
#pragma warning disable 649
    [SerializeField]
    private Transform rotatingObject;
#pragma warning restore 649
    [SerializeField]
    private float velocityMultiplier = 10f;
    [SerializeField]
    private float velocityDampen = 1f;

    private float prevPointerYPos;
    private bool isDragging;
    private float currentRotation;
    private IMixedRealityPointer storedPointer;

    protected virtual void Start()
    {
        if (rotatingObject == null)
        {
            Debug.LogError(this.GetType().Name + ": The field 'Rotating Object' cannot be left unassigned. Disabling the script");
            enabled = false;
            return;
        }
    }

    protected virtual void Update()
    {
        if (isDragging)
        {
            currentRotation = -velocityMultiplier * (storedPointer.Position.y - prevPointerYPos);
            rotatingObject.Rotate(0f, currentRotation, 0f);

            prevPointerYPos = storedPointer.Position.y;
        }
        else
        {
            if (!currentRotation.Approximately(0f, 0.1f))
            {
                currentRotation -= Mathf.Sign(currentRotation) * velocityDampen * Time.deltaTime;
                rotatingObject.Rotate(0f, currentRotation, 0f);
            }
        }
    }

    public virtual void OnPointerClicked(MixedRealityPointerEventData eventData) { }

    public virtual void OnPointerDown(MixedRealityPointerEventData eventData)
    {
        if (storedPointer != null) return;

        if (!IsPointerTargetValid(eventData.Pointer)) return;

        storedPointer = eventData.Pointer;
        prevPointerYPos = eventData.Pointer.Position.y;
    }

    public virtual void OnPointerDragged(MixedRealityPointerEventData eventData)
    {
        if (storedPointer == null || eventData.Pointer.PointerId != storedPointer.PointerId) return;

        isDragging = true;
    }

    public virtual void OnPointerUp(MixedRealityPointerEventData eventData)
    {
        if (storedPointer == null || eventData.Pointer.PointerId != storedPointer.PointerId) return;

        storedPointer = null;
        isDragging = false;
    }

    private bool IsPointerTargetValid(IMixedRealityPointer pointer)
    {
        GameObject pointerTarget = pointer.Result?.CurrentPointerTarget;

        return (pointerTarget != null && pointerTarget.transform.IsChildOf(transform));
    }
}
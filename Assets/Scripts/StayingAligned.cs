using UnityEngine;

public class StayingAligned : MonoBehaviour
{
#pragma warning disable 649
    [SerializeField]
    private Transform objToStayAlignedWith;
#pragma warning restore 649

    private Vector3 prevLocalPos;

    private void Start()
    {
        if (objToStayAlignedWith == null)
        {
            Debug.LogError(this.GetType().Name + ": The field 'Obj To Stay Aligned With' cannot be left unassigned. Disabling the script");
            enabled = false;
            return;
        }

        prevLocalPos = GetLocalPosition();
    }

    private void Update()
    {
        if (!MyLocalPosHasChanged()) return;

        transform.rotation = objToStayAlignedWith.rotation;
        prevLocalPos = GetLocalPosition();
    }

    private bool MyLocalPosHasChanged()
    {
        return (prevLocalPos != GetLocalPosition());
    }

    private Vector3 GetLocalPosition()
    {
        return transform.position - objToStayAlignedWith.position;
    }
}

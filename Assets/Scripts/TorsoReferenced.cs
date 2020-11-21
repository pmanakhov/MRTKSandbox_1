using UnityEngine;
using Microsoft.MixedReality.Toolkit.Utilities.Solvers;

[RequireComponent(typeof(SolverHandler))]
public class TorsoReferenced : Solver
{
    [SerializeField]
    private float placementDistance = 0f;
    public float PlacementDistance { get => placementDistance; set => placementDistance = value; }

    private Vector3 posOffset;
    private Quaternion rot;

    protected override void OnEnable()
    {
        base.OnEnable();

        if (SolverHandler.TransformTarget != null)
        {
            var target = SolverHandler.TransformTarget;

            // We take into account only rotation around the Y axis
            Quaternion correctedRot = Quaternion.Euler(0, target.rotation.eulerAngles.y, 0);

            posOffset = correctedRot * (Vector3.forward * placementDistance);
            rot = correctedRot;
        }
    }

    public override void SolverUpdate()
    {
        if (SolverHandler != null && SolverHandler.TransformTarget != null)
        {
            GoalPosition = SolverHandler.TransformTarget.position + posOffset;
            GoalRotation = rot;
        }
    }
}

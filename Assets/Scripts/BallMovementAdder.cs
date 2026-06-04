using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class BallMovementAdder : MonoBehaviour
{
    [SerializeField] private float ForceMutliplier = 30f;

    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void BallJump(SwipeInfo swipeInfo)
    {
        Vector3 forceDirection = new Vector3(Mathf.Cos(swipeInfo.Angle), 0, Mathf.Sin(swipeInfo.Angle));

        Vector3 force = forceDirection * swipeInfo.Magnitude * ForceMutliplier + Vector3.up;

        _rb.AddForce(force, ForceMode.Impulse);
    }
}

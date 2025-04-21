using UnityEngine;

public class Ledge : MonoBehaviour
{

    private LedgeGrabbing lB;
    public Transform orientation;
    public Transform cam;

    public float ledgeDetectionLength;
    public float ledgeSphereCastRadius;
    public LayerMask whatIsLedge;
    private Transform lastLedge;
    private Transform currLedge;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LedgeDetection()
    {
        bool ledgeDetected = Physics.SphereCast(transform.position, ledgeSphereCastRadius, cam.forward, out ledgeHit, ledgeDetectionLength, whatIsLedge);

        if (!ledgeDetected) return;

        float distanceToLedge = Vector3.Distance(transform.position, ledgeHit.transform.position);

        if (ledgeHit.transform == lastLedge) return;

        if (distanceToLedge < maxLedgeGrabDistance && !holding) EnterLedgeHold();
    }
}

using Unity.VisualScripting;
using UnityEngine;

public class Lasso : MonoBehaviour {
    private bool _enableLasso = false;
    private LineRenderer _lassoLineRenderer;
    private Rigidbody _rb;

    [Header("Lasso Spring Constants")]
    [SerializeField]
    private float _springK = 1.0f;
    [SerializeField]
    private float _springMinLength = 3.0f;

    [Header("Lasso Parameters")]
    public Transform LassoPoint;

    private void Start() {
        _lassoLineRenderer = gameObject.AddComponent<LineRenderer>();
        _lassoLineRenderer.SetPosition(0, LassoPoint.position);
        _lassoLineRenderer.enabled = false;
        _rb = GetComponent<Rigidbody>();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.L)) {
            _enableLasso = !_enableLasso;
            _lassoLineRenderer.enabled = _enableLasso;
        }

        // Update LineRenderer
        if (_enableLasso) {
            _lassoLineRenderer.SetPosition(1, this.transform.position);
        }
    }

    private void FixedUpdate() {
        if (_enableLasso) {
            // Find spring force
            float springDisplacement = Vector3.Distance(LassoPoint.position, this.transform.position) - _springMinLength;
            float springMagnitude = springDisplacement * _springK;
            Vector3 springDirection = (LassoPoint.position - this.transform.position).normalized;
            Vector3 springForce = springMagnitude * springDirection;

            // Apply spring force
            _rb.AddForce(springForce, ForceMode.Impulse);
        }
    }
}

using UnityEngine;

public class Lasso : MonoBehaviour {
    private bool _enableLasso = false;

    [Header("Lasso Spring Constants")]
    [SerializeField]
    private float _springK = 1.0f;
    [SerializeField]
    private float _springMinLength = 3.0f;

    [Header("Lasso Parameters")]
    public Vector3 LassoPoint;

    private void Start() {
        
    }
}

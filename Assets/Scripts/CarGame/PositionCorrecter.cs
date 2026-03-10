using UnityEngine;

public class PositionCorrecter : MonoBehaviour
{
    [SerializeField]
    private Transform _positionOwner;
    private void OnValidate()
    {
        if (_positionOwner == null) return;

        var meshRenderer = _positionOwner.GetComponent<MeshRenderer>();
        transform.position = meshRenderer.bounds.center;
    }
}

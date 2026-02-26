using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _sensitivity = 0.1f;
    private readonly float _offset = 30f;
    private void Update()
    {
        var input = Assets.Scripts.Input.Input._inputActions.Camera.Rotation.ReadValue<Vector2>() * _sensitivity;
        if (input.x != 0)
            transform.position += transform.right * -input.x;
        if (input.y != 0)
            transform.position += transform.up * -input.y;

        var direction = Vector3.Normalize(transform.position - _target.position);
        transform.position = _target.position + direction * _offset;
    }
    private void LateUpdate()
    {
        transform.LookAt(_target.position, Vector3.up);
    }
}

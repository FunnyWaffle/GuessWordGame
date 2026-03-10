using UnityEngine;

namespace Assets.Scripts.Car
{
    public class Wheel : MonoBehaviour
    {
        [SerializeField]
        private Transform _model;
        [SerializeField]
        private WheelCollider _collider;
        public float TotalSteerAngle => _collider.steerAngle;
        public float Radius => _collider.radius;

        private void Start()
        {
            _collider.radius = _model.GetComponentInChildren<Renderer>().bounds.size.y / 2f;
        }
        public void Torque(float power)
        {
            _collider.motorTorque = power;
        }
        public void Brake(float power)
        {
            _collider.brakeTorque = power;
        }
        public void Steer(float angle)
        {
            _collider.steerAngle = angle;
        }
        public void RotateModelAfterCollider()
        {
            _collider.GetWorldPose(out _, out var quaternion);
            _model.rotation = quaternion;
        }
        public bool GetGroundHit(out WheelHit hit)
        {
            return _collider.GetGroundHit(out hit);
        }
    }
}

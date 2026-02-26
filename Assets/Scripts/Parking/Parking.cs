using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Parking
{
    public class Parking : MonoBehaviour
    {
        [SerializeField] private List<Transform> Lines;
        [SerializeField] private List<Transform> Cars;

        private readonly List<Collider> _parkedCarColliders = new();
        private readonly List<Vector3> _parkedCarCorners = new();
        private readonly List<Vector3> _collidedCarCorners = new();

        private Transform _collidedCarTransform;
        private MeshCollider _collidedCarCollider;

        private void Start()
        {
            foreach (Transform car in Cars)
            {
                var carCollider = car.GetComponentInChildren<MeshCollider>();
                _parkedCarColliders.Add(carCollider);
                GetCarEdges(carCollider, _parkedCarCorners);
                //GetCarCornersFor(car, carCollider, _parkedCarCorners);
            }
        }
        private void Update()
        {
            if (!_collidedCarTransform || !_collidedCarCollider)
                return;

            _collidedCarCorners.Clear();
            GetCarEdges(_collidedCarCollider, _collidedCarCorners);
            foreach (var collidedCarCorner in _collidedCarCorners)
            {
                float minDistance = float.MaxValue;
                Vector3? closestCorner = null;
                Vector3? targetCorner = null;

                foreach (var corner in _parkedCarCorners)
                {
                    var distance = Vector3.Distance(corner, collidedCarCorner);

                    if (distance <= 7f && distance < minDistance)
                    {
                        minDistance = distance;
                        closestCorner = collidedCarCorner;
                        targetCorner = corner;
                    }
                }
                if (!closestCorner.HasValue)
                    continue;

                var color = Color.green;
                if (minDistance <= 2.8f)
                    color = Color.red;

                Debug.DrawLine(targetCorner.Value, closestCorner.Value, color, .5f);
            }
        }
        private void GetCarEdges(MeshCollider collider, List<Vector3> angles)
        {
            var halfExtents = collider.sharedMesh.bounds.extents;
            var center = collider.sharedMesh.bounds.center;

            var transform = collider.transform;

            var edge = GetCarEdge(transform, collider, center, new Vector3(halfExtents.x, 0f, halfExtents.z));
            angles.Add(edge);
            edge = GetCarEdge(transform, collider, center, new Vector3(halfExtents.x, 0f, -halfExtents.z));
            angles.Add(edge);
            edge = GetCarEdge(transform, collider, center, new Vector3(-halfExtents.x, 0f, halfExtents.z));
            angles.Add(edge);
            edge = GetCarEdge(transform, collider, center, new Vector3(-halfExtents.x, 0f, -halfExtents.z));
            angles.Add(edge);
        }
        private Vector3 GetCarEdge(Transform car, MeshCollider collider, Vector3 center, Vector3 offset)
        {
            var edge = car.TransformPoint(center + offset);
            return collider.ClosestPoint(edge);
        }
        private void OnTriggerEnter(Collider other)
        {
            if (_parkedCarColliders.Contains(other))
                return;

            if (other is MeshCollider meshCollider)
            {
                _collidedCarTransform = other.transform;
                _collidedCarCollider = meshCollider;
            }
        }
        //private void OnDrawGizmos()
        //{
        //    foreach (var corner in _collidedCarCorners)
        //    {
        //        Gizmos.DrawSphere(corner, 0.2f);
        //    }
        //    foreach (var corner in _parkedCarCorners)
        //    {
        //        Gizmos.DrawSphere(corner, 0.2f);
        //    }
        //}
    }
}

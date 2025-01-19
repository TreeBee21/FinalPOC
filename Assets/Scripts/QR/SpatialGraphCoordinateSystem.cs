using UnityEngine;
using Microsoft.MixedReality.OpenXR;

namespace QRTracking
{
    public class SpatialGraphNodeTracker : MonoBehaviour
    {
        private System.Guid _id;
        private SpatialGraphNode node;
        private float sideLength = 0;

        public void SetSideLength(float sideLength) => this.sideLength = sideLength;

        public System.Guid Id
        {
            get => _id;

            set
            {
                if (_id != value)
                {
                    _id = value;
                    InitializeSpatialGraphNode(force: true);
                }
            }
        }

        // Use this for initialization
        void Start()
        {
            InitializeSpatialGraphNode();
        }

        // Update is called once per frame
        void Update()
        {
            InitializeSpatialGraphNode();

            if (node != null)
            {
                if (node.TryLocate(FrameTime.OnUpdate, out Pose pose))
                {
                    // If there is a parent to the camera that means we are using teleport and we should not apply the teleport
                    // to these objects so apply the inverse
                    if (Camera.main.transform.parent != null)
                    {
                        pose = pose.GetTransformedBy(Camera.main.transform.parent);
                    }

                    pose.rotation *= Quaternion.Euler(90, 0, 0);

                    // Move the anchor point to the *center* of the QR code
                    var deltaToCenter = sideLength * 0.5f;
                    pose.position += (pose.rotation * (deltaToCenter * Vector3.right) -
                                      pose.rotation * (deltaToCenter * Vector3.forward));
                    gameObject.transform.SetPositionAndRotation(pose.position, pose.rotation);
                }
                else
                {
                    Debug.LogWarning("Cannot locate " + Id);
                }
            }
        }

        private void InitializeSpatialGraphNode(bool force = false)
        {
            if (node == null || force)
            {
                node = (Id != System.Guid.Empty) ? SpatialGraphNode.FromStaticNodeId(Id) : null;
                Debug.Log("Initialize SpatialGraphNode Id= " + Id);
            }
        }
    }
}
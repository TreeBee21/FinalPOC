using Microsoft.MixedReality.QR;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using QRTracking;
using System;
using Unity.VisualScripting;

public class QrMarker : MonoBehaviour
{
    public UnityEvent<string> Selected = new();

    private const float QrExpireTime = 1000;

    public QRCode Code { get; private set; }

    private SpatialGraphNodeTracker spatialGraph;
    private Collider col;
    private MeshRenderer meshRenderer;

    private bool isTracking = true;

    private float sideLengthUpperBound = 0;
    private float sideLengthLowerBound = 0;

    private void Awake()
    {
        Collider col = GetComponentInChildren<Collider>();
        if (col != null)
        {
            this.col = col;
        }

        MeshRenderer mr = GetComponentInChildren<MeshRenderer>();
        if (mr != null)
        {
            this.meshRenderer = mr;
        }
       
    }

    private void Update()
    {
        float sideLength = Code.PhysicalSideLength;
        if (!(sideLength > sideLengthLowerBound && sideLength < sideLengthUpperBound))
        { 
            SetScale(sideLength);
        }
        if ((DateTimeOffset.UtcNow - Code.LastDetectedTime.UtcDateTime).TotalMilliseconds >= QrExpireTime && isTracking)
        {
            ToggleTracking(false);
        }
        else if ((DateTimeOffset.UtcNow - Code.LastDetectedTime.UtcDateTime).TotalMilliseconds < QrExpireTime && !isTracking)
        { 
            ToggleTracking(true);
        }
    }

    private void OnEnable()
    {
        if (Code == null) { enabled = false; }
    }

    public virtual void Select() => Selected.Invoke(Code.Data);

    public void AttachMarker(QRCode code)
    {
        this.Code = code;
        spatialGraph = gameObject.AddComponent<SpatialGraphNodeTracker>();
        spatialGraph.SetSideLength(code.PhysicalSideLength);
        spatialGraph.Id = code.SpatialGraphNodeId;
        SetScale();
        enabled = true;
    }

    private void SetScale() => SetScale(Code.PhysicalSideLength);

    private void SetScale(float sideLength)
    {
        if (Code != null)
        {
            transform.localScale = Vector3.one * sideLength;
            SetSideLengthBounds(sideLength);
        }
    }

    private void SetSideLengthBounds(float sideLength)
    { 
        spatialGraph.SetSideLength(sideLength);
        sideLengthUpperBound = sideLength + 0.01f;
        sideLengthLowerBound = sideLength - 0.01f;
    }

    private void ToggleTracking(bool active)
    {
        isTracking = active;
        if (spatialGraph != null)
        {
            spatialGraph.enabled = active;
        } 
        meshRenderer.enabled = active;
        col.enabled = active;
    }

}

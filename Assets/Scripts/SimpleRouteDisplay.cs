using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(LineRenderer))]
public class SimpleRouteDisplay : MonoBehaviour
{
    [Tooltip("Waypoints for route from A to B.")]
    public Transform[] routeWaypoints_AB;

    [Tooltip("Waypoints for route from B to C.")]
    public Transform[] routeWaypoints_BC;

    [Header("Route Events")]
    public UnityEvent OnRouteABActivated; // Event for A->B route
    public UnityEvent OnRouteABDeactivated; // Event for hiding A->B route
    public UnityEvent OnRouteBCActivated; // Event for B->C route
    public UnityEvent OnRouteBCDeactivated; // Event for hiding B->C route

    private LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void DrawRouteAB()
    {
        if (routeWaypoints_AB == null || routeWaypoints_AB.Length < 2)
        {
            Debug.LogError("routeWaypoints_AB must have at least 2 transforms.");
            return;
        }

        lineRenderer.enabled = true;
        lineRenderer.positionCount = routeWaypoints_AB.Length;
        for (int i = 0; i < routeWaypoints_AB.Length; i++)
        {
            lineRenderer.SetPosition(i, routeWaypoints_AB[i].position);
        }

        // Trigger A->B active event
        OnRouteABActivated?.Invoke();
    }

    public void DrawRouteBC()
    {
        if (routeWaypoints_BC == null || routeWaypoints_BC.Length < 2)
        {
            Debug.LogError("routeWaypoints_BC must have at least 2 transforms.");
            return;
        }

        lineRenderer.enabled = true;
        lineRenderer.positionCount = routeWaypoints_BC.Length;
        for (int i = 0; i < routeWaypoints_BC.Length; i++)
        {
            lineRenderer.SetPosition(i, routeWaypoints_BC[i].position);
        }

        // Trigger B->C active event
        OnRouteBCActivated?.Invoke();
    }

    public void HideRoute()
    {
        if (lineRenderer.enabled)
        {
            if (lineRenderer.positionCount == routeWaypoints_AB.Length)
            {
                OnRouteABDeactivated?.Invoke(); // Trigger A->B deactivation
            }
            else if (lineRenderer.positionCount == routeWaypoints_BC.Length)
            {
                OnRouteBCDeactivated?.Invoke(); // Trigger B->C deactivation
            }

            lineRenderer.enabled = false;
        }
    }
}

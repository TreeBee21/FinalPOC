using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class SimpleRouteDisplay : MonoBehaviour
{
    [Tooltip("Waypoints for route from A to B.")]
    public Transform[] routeWaypoints_AB;

    [Tooltip("Waypoints for route from B to C.")]
    public Transform[] routeWaypoints_BC;

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

        // Show the line
        lineRenderer.enabled = true;

        // Set positions in the line
        lineRenderer.positionCount = routeWaypoints_AB.Length;
        for (int i = 0; i < routeWaypoints_AB.Length; i++)
        {
            lineRenderer.SetPosition(i, routeWaypoints_AB[i].position);
        }
    }

    public void DrawRouteBC()
    {
        if (routeWaypoints_BC == null || routeWaypoints_BC.Length < 2)
        {
            Debug.LogError("routeWaypoints_BC must have at least 2 transforms.");
            return;
        }

        // Show the line
        lineRenderer.enabled = true;

        // Set positions in the line
        lineRenderer.positionCount = routeWaypoints_BC.Length;
        for (int i = 0; i < routeWaypoints_BC.Length; i++)
        {
            lineRenderer.SetPosition(i, routeWaypoints_BC[i].position);
        }
    }

    public void HideRoute()
    {
        lineRenderer.enabled = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route : MonoBehaviour
{
    public bool ReachDestination { get; private set; }
    public int Length { get; private set; }

    private List<Connector> connectors = new List<Connector>();

    public void AddConnector(Connector connector)
    {
        if (!connectors.Contains(connector))
        {
            connectors.Add(connector);
            Length++;
        }
    }

    public void VisualizeRoute()
    {
        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.positionCount = connectors.Count + 1;

        for (int i = 0; i < connectors.Count; i++)
        {
            lineRenderer.SetPosition(i, connectors[i].PointOne.transform.position);
        }
        lineRenderer.SetPosition(connectors.Count, connectors[^1].PointTwo.transform.position);
    }
}
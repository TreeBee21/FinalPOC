using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    public List<Connector> ConnectedConnectors { get; private set; } = new List<Connector>();

    public void AddConnector(Connector connector)
    {
        if (!ConnectedConnectors.Contains(connector))
        {
            ConnectedConnectors.Add(connector);
        }
    }

    private void Start()
    {
        Debug.Log($"Point {name} is connected to {ConnectedConnectors.Count} connectors.");
    }
}
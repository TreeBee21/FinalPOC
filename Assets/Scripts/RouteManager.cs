using System.Collections.Generic;
using UnityEngine;

public class RouteManager : MonoBehaviour
{
    public List<Point> allPoints;

    public Route CreateShortestRoute(Point startPoint, Point endPoint)
    {
        var queue = new Queue<Point>();
        var visited = new HashSet<Point>();
        var previous = new Dictionary<Point, Point>();

        queue.Enqueue(startPoint);

        while (queue.Count > 0)
        {
            var currentPoint = queue.Dequeue();

            if (currentPoint == endPoint)
            {
                return BuildRoute(previous, startPoint, endPoint);
            }

            visited.Add(currentPoint);

            foreach (var connector in currentPoint.ConnectedConnectors)
            {
                var nextPoint = connector.GetOppositePoint(currentPoint);
                if (!visited.Contains(nextPoint))
                {
                    queue.Enqueue(nextPoint);
                    if (!previous.ContainsKey(nextPoint))
                    {
                        previous[nextPoint] = currentPoint;
                    }
                }
            }
        }
        Debug.Log("No Route found");
        return null; // No route found
    }

    private Route BuildRoute(Dictionary<Point, Point> previous, Point startPoint, Point endPoint)
    {
        var route = new GameObject("Route").AddComponent<Route>();
        var current = endPoint;

        while (current != startPoint)
        {
            var previousPoint = previous[current];
            var connector = previousPoint.ConnectedConnectors
                .Find(c => c.GetOppositePoint(previousPoint) == current);

            route.AddConnector(connector);
            current = previousPoint;
        }

        return route;
    }
}
using UnityEngine;

public class Connector : MonoBehaviour
{
    [SerializeField] private Point pointOne; // Backing field for PointOne
    [SerializeField] private Point pointTwo; // Backing field for PointTwo

    // Public properties to access the points
    public Point PointOne
    {
        get => pointOne;
        set => pointOne = value;
    }

    public Point PointTwo
    {
        get => pointTwo;
        set => pointTwo = value;
    }

    public Point GetOppositePoint(Point point)
    {
        if (PointOne != point) return PointOne;
        else return PointTwo;
    }
}
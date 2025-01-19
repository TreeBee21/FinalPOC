using UnityEngine;

public class ManualLinker : MonoBehaviour
{
    // Start is called before the first frame update

    public Point pointOne;
    public Point pointTwo;
    public Connector connector;

    void Start()
    {
        //add the connector to both points
        pointOne.AddConnector(connector);
        pointTwo.AddConnector(connector);
    }

 
}

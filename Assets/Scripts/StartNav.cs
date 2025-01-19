using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartNav : MonoBehaviour
{
    [SerializeField] SimpleRouteDisplay routeDisplay;
    private bool listening = false;

    public void SetListening(bool listen) => listening = listen;

    public void OnQRSelected(string data)
    {
        if ((!listening) || (!(data == "1")) ) return;
        
        routeDisplay.DrawRouteAB(); 


        listening = false;

    }

}

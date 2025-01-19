using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugMarker : QrMarker
{
    [SerializeField] string data;

    public override void Select()
    {
        Selected.Invoke(data);
    }
}

using System;
using UnityEngine;

public class Door : MonoBehaviour
{
    public event Action CrookDetected;
    public event Action CrookLost;

    private void OnTriggerEnter(Collider collider)
    {
        CrookDetected.Invoke();
    }

    private void OnTriggerExit(Collider collider)
    {
        CrookLost.Invoke();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDeactivate : MonoBehaviour
{
    public GameObject parent;

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public void DeactivateFinal()
    {
        parent.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Leap.Unity;
using Leap;
using Leap.Unity.Interaction;

[RequireComponent(typeof(InteractionBehaviour))]
public class GraspLever : MonoBehaviour
{
    private InteractionBehaviour interact;

    void Start()
    {
        interact = GetComponent<InteractionBehaviour>();
        Debug.Log(interact);
    }

    void Update()
    {
     
    }
}

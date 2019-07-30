using UnityEngine;
using Leap.Unity.Interaction;

[RequireComponent(typeof(InteractionBehaviour))]
public class GraspFront : MonoBehaviour
{
    private InteractionBehaviour interact;
    private MaterialPropertyBlock mat;
    private Renderer render;

    void Start()
    {
        mat = new MaterialPropertyBlock();
        mat.SetColor("_Color", Color.red);
        interact = GetComponent<InteractionBehaviour>();
        render = GetComponent<Renderer>();
    }

    void Update()
    {
        if (interact.isGrasped)
        {
            render.SetPropertyBlock(mat);
        }
        else
        {
            render.SetPropertyBlock(null);
        }
    }
}

using UnityEngine;
using Leap.Unity.Interaction;
using Leap.Unity;

[RequireComponent(typeof(InteractionBehaviour))]
public class GraspHandle : MonoBehaviour
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
            var sourceHand = Hands.Get(Chirality.Right);
            sourceHand = (sourceHand != null ? sourceHand : Hands.Get(Chirality.Left));

            if(sourceHand != null)
            {
                
            }
        }
        else
        {
            render.SetPropertyBlock(null);
        }
    }
}

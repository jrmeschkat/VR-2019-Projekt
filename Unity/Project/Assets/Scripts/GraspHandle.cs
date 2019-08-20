using UnityEngine;
using Leap.Unity.Interaction;
using System.Collections.Generic;

[RequireComponent(typeof(InteractionBehaviour))]
public class GraspHandle : MonoBehaviour {
    private MaterialPropertyBlock mat;

    private InteractionBehaviour _interact;
    private Renderer _render;
    

    private void OnEnable() {
        mat = new MaterialPropertyBlock();
        mat.SetColor("_Color", Color.red);

        _interact = GetComponent<InteractionBehaviour>();
        _render = GetComponent<Renderer>();

        _interact.OnGraspedMovement -= onGrasp;
        _interact.OnGraspedMovement += onGrasp;
    }

    private void OnDisable() {
        _interact.OnGraspedMovement -= onGrasp;
    }

    void Update() {
        Debug.Log(_interact.rigidbody.rotation.eulerAngles);
        if (_interact.isGrasped) {
            _render.SetPropertyBlock(mat);
        }
        else {
            _render.SetPropertyBlock(null);
        }
    }

    private void onGrasp(Vector3 prePos, Quaternion preRot, Vector3 pos, Quaternion rot, List<InteractionController> controller) {
        Vector3 oldLocalRot = transform.localRotation.eulerAngles;
        transform.rotation = rot;
        Vector3 localRot = transform.localRotation.eulerAngles;

        localRot.y = oldLocalRot.y;
        localRot.z = oldLocalRot.z;

        transform.localRotation = Quaternion.Euler( localRot);

        Debug.Log(localRot);
    }
}

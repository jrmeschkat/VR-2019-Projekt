using UnityEngine;

using Leap.Unity.Interaction;
using System.Collections.Generic;

public class HandleTest : MonoBehaviour {

    public float minRotation = 0f;
    public float maxRotation = 360f;

    private InteractionBehaviour _interact;
    private Renderer _render;
    private Rigidbody _body;


    private float startingAngle;
    private MaterialPropertyBlock mat;

    // Start is called before the first frame update
    void Start() {
        mat = new MaterialPropertyBlock();
        mat.SetColor("_Color", Color.red);


    }

    private void OnEnable() {
        _render = GetComponent<Renderer>();
        _body = GetComponent<Rigidbody>();
        _interact = GetComponent<InteractionBehaviour>();

        _interact.manager.OnPostPhysicalUpdate -= rotationConstraint;
        _interact.manager.OnPostPhysicalUpdate += rotationConstraint;
    }

    private void OnDisable() {
        startingAngle = transform.localEulerAngles.x;
        _interact.manager.OnPostPhysicalUpdate -= rotationConstraint;
    }

    private void rotationConstraint() {
        Vector3 angles = transform.localRotation.eulerAngles;
        float rotX = angles.x;

        if(angles.y != 0 && angles.z != 0) {
            rotX = startingAngle - (angles.x - startingAngle);
        } 

        //angles.x = Mathf.Clamp(angles.x, minRotation, maxRotation);
        angles.x = rotX;

        transform.localRotation = Quaternion.Euler(0, 0, 0);        
    }

    // Update is called once per frame
    void Update() {


        if (_interact.isGrasped) {
            _render.SetPropertyBlock(mat);
            _body.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        }
        else {
            _render.SetPropertyBlock(null);
            _body.constraints = RigidbodyConstraints.FreezeAll;
        }
    }


}

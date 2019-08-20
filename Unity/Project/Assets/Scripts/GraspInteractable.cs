using UnityEngine;
using Leap.Unity.Interaction;
using System.Reflection;

public class GraspInteractable : MonoBehaviour {

    public float minXAngle = 0f;
    public float maxXAngle = 360f;
    public float graspValue = 0f;
    public bool invertGraspValue = false;

    private InteractionBehaviour _interact;
    private Renderer _render;
    private HingeJoint _hinge;

    private MaterialPropertyBlock mat;
    private MethodInfo methodGetLocalEulerAngles;
    private PropertyInfo propertyRotationOrder;
    private float offset = 0f;

    private float CalculateGraspValue() {
        object rotationOrder = null;
        if (propertyRotationOrder != null) {
            rotationOrder = propertyRotationOrder.GetValue(transform, null);
        }
        if (methodGetLocalEulerAngles != null) {
            Vector3 rotation = (Vector3)methodGetLocalEulerAngles.Invoke(transform, new object[] { rotationOrder });
            float x = rotation.x;
            if (invertGraspValue) {
                return Mathf.Max(0, ((maxXAngle + offset) - (x + offset)) / (maxXAngle + offset));
            }
            else {
                return Mathf.Max(0, (x + offset) / (maxXAngle + offset));
            }
        }

        return 0f;
    }

    void Start() {
        if (minXAngle != 0) {
            offset = minXAngle * -1;
        }
        methodGetLocalEulerAngles = typeof(Transform).GetMethod("GetLocalEulerAngles", BindingFlags.Instance | BindingFlags.NonPublic);
        propertyRotationOrder = typeof(Transform).GetProperty("rotationOrder", BindingFlags.Instance | BindingFlags.NonPublic);

        _interact = GetComponent<InteractionBehaviour>();
        _render = GetComponent<Renderer>();
        _hinge = GetComponent<HingeJoint>();

        mat = new MaterialPropertyBlock();
        mat.SetColor("_Color", Color.red);

        JointMotor motor = _hinge.motor;
        motor.targetVelocity = 0;
        motor.force = 1000;
        motor.freeSpin = false;
        _hinge.motor = motor;
        _hinge.useMotor = true;

    }

    void Update() {
        graspValue = CalculateGraspValue();

        if (_interact.isGrasped) {
            _hinge.useMotor = false;
            _render.SetPropertyBlock(mat);
        }
        else {
            _hinge.useMotor = true;
            _render.SetPropertyBlock(null);
        }
    }


}

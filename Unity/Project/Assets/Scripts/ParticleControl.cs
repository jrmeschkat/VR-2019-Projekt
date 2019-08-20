using UnityEngine;

public class ParticleControl : MonoBehaviour {
    public GraspInteractable handle;
    public GraspInteractable front;
    public GraspInteractable flush;

    private ParticleSystem _particle;
    private ParticleSystem.MainModule main;
    private ParticleSystem.ShapeModule shape;
    private int initialMaxParticles;
    private float initalStartSpeed;

    void Start() {
        _particle = GetComponent<ParticleSystem>();
        main = _particle.main;
        shape = _particle.shape;
        initialMaxParticles = main.maxParticles;
        initalStartSpeed = main.startSpeed.constant;
    }

    void Update() {
        if (handle.graspValue < .05f) {
            _particle.Stop();
        }
        else {
            int maxParticles = (int)Mathf.Max(5000, initialMaxParticles * handle.graspValue);

            main.maxParticles = maxParticles;
            _particle.Play();
        }

        float angle = Mathf.Max(1, Mathf.Round(45f * front.graspValue));
        shape.angle = angle;

        ParticleSystem.MinMaxCurve startSpeed = main.startSpeed;
        if (flush.graspValue > .95f) {
            startSpeed.constant = 500f;
            main.startSpeed = startSpeed;
        }
        else {
            float speed = Mathf.Max(1000f, initalStartSpeed * ((flush.graspValue + 1) / 2));
            startSpeed.constant = speed;
            main.startSpeed = startSpeed;
        }
    }
}

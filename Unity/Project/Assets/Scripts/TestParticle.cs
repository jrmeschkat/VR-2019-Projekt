using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestParticle : MonoBehaviour
{
    public ParticleSystem particle;

    private float time = 0;
    private int dir = 1;
    private ParticleSystem.ShapeModule shape;

    // Start is called before the first frame update
    void Start()
    {
        shape = particle.shape;
    }

    // Update is called once per frame
    void Update()
    {
        if((time += Time.deltaTime) > .1f) {
            time = 0;
            float angle = shape.angle;
            if(angle >= 80 ||angle <= 10) {
                dir *= -1;
            }

            shape.angle = shape.angle + 1 * dir;
            
        }
    }
}

using Unity.VisualScripting;
using UnityEngine;

public class PointMass : MonoBehaviour
{
    public float radius;

    public Vector2 position;
    public Vector2 prevPosition;

    public Vector2 gravity;
    public Vector2 input;
    public float speed;

    Vector2 netForce;

    public float mass = 0.0f;
    public float inverseMass;

    [SerializeField] bool hasJump;

    private void Start()
    {
        inverseMass = 1.0f / mass;

        position = (Vector2)transform.position;
        prevPosition = position;
    }

    private void Update()
    {
        //VerletStep();
        Vector2 inputValue = InputManager.instance.xInput.action.ReadValue<Vector2>();
        input.x = inputValue.x * speed;  // Directly multiply
        netForce += input;

        if(InputManager.instance.jump.action.triggered && hasJump)
        {
            ApplyImpulse(new Vector2(input.x, 50));
        }
    }

    public void ApplyImpulse(Vector2 impulse)
    {
        prevPosition -= impulse * Time.fixedDeltaTime;
    }

    public void VerletStep()
    {
        //clear forces from last frame
        netForce = Vector2.zero;

        //add continuous forces
        netForce += input;
        netForce += gravity;

        //Verlet Intergration
        Vector2 implicitVelocity = position - prevPosition;
        implicitVelocity *= 0.98f; //dampening

        prevPosition = position;
        position += implicitVelocity;

        position += netForce * Time.deltaTime * Time.deltaTime;

        transform.position = position;

    }
}

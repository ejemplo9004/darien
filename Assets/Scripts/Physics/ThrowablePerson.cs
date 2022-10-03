using UnityEngine;

public class ThrowablePerson : MonoBehaviour
{
    public float gravity = -9.8f;
    private bool isInAir;
    private bool thereIsWind;
    private Vector3 initPosition;
    private Vector3 initVelocity;
    [SerializeField] private Vector3 aceleration;
    private float time;

    private void Update()
    {
        if (isInAir)
        {
            //Vector3 currentVelocity = GetCurrentVelocity();
            //float yVel = currentVelocity.y;
            // if (groundChecker.CheckGround() && yVel < 0)
            // {
            //     rb.isKinematic = false;
            //     rb.velocity = currentVelocity;
            //     EndAirTrip();
            //     return;
            // }
            CalculateNextPosition();
        }
    }

    public void StartAirTrip(Vector3 vel, Vector3 windAc)
    {
        aceleration += windAc;
        initPosition = transform.position;
        initVelocity = vel;
        time = 0f;
        isInAir = true;
    }

    private void CalculateNextPosition()
    {
        time += Time.deltaTime;
        float xAxis = initPosition.x + initVelocity.x * time + (aceleration.x / 2) * Mathf.Pow(time, 2);
        float yAxis = initPosition.y + initVelocity.y * time + (gravity / 2) * Mathf.Pow(time, 2);
        float zAxis = initPosition.z + initVelocity.z * time + (aceleration.z / 2) * Mathf.Pow(time, 2);
        transform.position = new Vector3(xAxis, yAxis, zAxis);
    }

    private Vector3 GetCurrentVelocity()
    {
        float xAxis = initVelocity.x + aceleration.x * time;
        float yAxis = initVelocity.y + gravity * time;
        float zAxis = initVelocity.z + aceleration.z * time;
        return new Vector3(xAxis, yAxis, zAxis);
    }

    public void EndAirTrip()
    {
        isInAir = false;
    }
    // private void OnCollisionEnter(Collision collision)
    // {
    //     if (collision.gameObject.CompareTag("Floor"))
    //     {
    //         EndAirTrip();
    //     }
    // }
}

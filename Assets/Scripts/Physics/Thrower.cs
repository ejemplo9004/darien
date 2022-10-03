using System;
using UnityEngine;

public class Thrower : MonoBehaviour
{
    
    public Vector3 direction;
    private float force;
    public float forceLimit;
    public float impulsePerSecond;
    public KeyCode throwInput;
    public ThrowablePerson person;
    [SerializeField] private ThrowPredictor predictor;
    [SerializeField] private Wind wind;

    private void Update()
    {
        if (Input.GetKey(throwInput))
        {
            GetImpulse();
        }

        if (Input.GetKeyUp(throwInput))
        {
            predictor.EraseLine();
            predictor.StopDrawing(false);
            Throw();
            force = 0;
        }
    }

    public void Throw()
    {
        person.StartAirTrip(force * direction, wind.Aceleration);
    }

    public void GetImpulse()
    {
        if (force < forceLimit)
        {
            force += impulsePerSecond * Time.deltaTime;
            Vector3 pos = person.gameObject.transform.position;
            Vector3 vel = force * direction;
            Vector3 ac = wind.Aceleration + new Vector3(0, person.gravity, 0);
            StartCoroutine(predictor.DrawLine(pos, vel, ac));
            if (force > forceLimit)predictor.StopDrawing(true);
        }
    }
    
    public Vector3 Direction
    {
        get => direction;
        set => direction = value;
    }
}

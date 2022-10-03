using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowPredictor : MonoBehaviour
{
    [SerializeField] private LineRenderer predictionLine;
    public float lineDistance;
    public int pointsToDraw;
    private bool erased;
    private bool isFinished;
    private Vector3 pos;
    private Vector3 vel;
    private Vector3 ac;

    public IEnumerator DrawLine()
    {
        
        predictionLine.positionCount = pointsToDraw;
        float t = 0;
        while (!isFinished)
        {
            for (int i = 0; i < pointsToDraw; i++)
            {
                t = lineDistance * (float)i / (float)(pointsToDraw - 1);
                Vector3 nextPosition = GetNextPosition(t, pos, vel, ac);
                predictionLine.SetPosition(i, nextPosition);
                
            }

            yield return new WaitForEndOfFrame();
        }
        predictionLine.positionCount = 0;
    }

    private Vector3 GetNextPosition(float time, Vector3 pos, Vector3 vel, Vector3 ac)
    {
        float xAxis = pos.x + vel.x * time + (ac.x / 2) * Mathf.Pow(time, 2);
        float yAxis = pos.y + vel.y * time + (ac.y / 2) * Mathf.Pow(time, 2);
        float zAxis = pos.z + vel.z * time + (ac.z / 2) * Mathf.Pow(time, 2);
        return new Vector3(xAxis, yAxis, zAxis);
    }

    public void EraseLine()
    {
        //
        erased = true;
    }

    public void StopDrawing(bool state)
    {
        isFinished = state;
        if (!state)
        {
            StartCoroutine(DrawLine());
        }
    }
    
    public void UpdateParameters(Vector3 _pos, Vector3 _vel, Vector3 _ac)
    {
        pos = _pos;
        vel = _vel;
        ac = _ac;
    }
}

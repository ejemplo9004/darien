using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowPredictor : MonoBehaviour
{
    [SerializeField] private LineRenderer predictionLine;
    public float lineDistance;
    public float pointsToDraw;
    private bool erased;
    private bool isFinished = false;

    public IEnumerator DrawLine(Vector3 pos, Vector3 vel, Vector3 ac)
    {
        predictionLine.positionCount = 0;
        for (float i = 0; i < lineDistance; i += lineDistance / pointsToDraw)
        {
            if(isFinished && predictionLine.positionCount == pointsToDraw - 1)break;
            predictionLine.positionCount++;
            Vector3 nextPosition = GetNextPosition(i, pos, vel, ac);
            predictionLine.SetPosition(predictionLine.positionCount - 1, nextPosition);
            yield return null;
        }
        yield return null;
        if(erased)EraseLine();
        erased = false;
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
        //predictionLine.positionCount = 0;
        erased = true;
    }

    public void StopDrawing(bool state)
    {
        isFinished = state;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoves : MonoBehaviour
{
    public bool interpolation;
    public float speed = 2f;
    private float previousSpeed;
    public float elapsedTime = 0f;
    private float realElapsedTime = 0f;
    private float journeyDuration;
    private float timeUntilNextFrame = 0f;
    public CreateAnchorPoints script;
    private Vector3[] pointsPosition;
    void Start()
    {
        pointsPosition = script.pointsPosition;
        previousSpeed = speed;
        journeyDuration = pointsPosition.Length * speed;
    }

    void Update()
    {
        realElapsedTime += Time.deltaTime;
        timeUntilNextFrame = - Time.deltaTime * 2;
        if (speed != previousSpeed) 
        {
            elapsedTime = realElapsedTime  / journeyDuration / previousSpeed* speed;
            journeyDuration = pointsPosition.Length * speed;
            previousSpeed = speed;
        } else if (elapsedTime <= pointsPosition.Length * speed + speed * Convert.ToByte(interpolation)) {
            elapsedTime += Time.deltaTime;
        } else {
            elapsedTime -= journeyDuration + speed * Convert.ToByte(interpolation);
        }
        MoveCamera();
    }

    private void MoveCamera()
    {
        Vector3 newCameraPos = calculateNextPoint(elapsedTime);
        Vector3 nextPoint = calculateNextPoint(elapsedTime + timeUntilNextFrame);
        Vector3 direction = (transform.position - nextPoint).normalized;
        Quaternion targetRotation;
        if (direction == Vector3.zero) targetRotation = Quaternion.Euler(Vector3.zero);
        else targetRotation = Quaternion.LookRotation(direction);
        transform.position = newCameraPos;
        /* GameObject visPos = GameObject.CreatePrimitive(PrimitiveType.Cube);
        visPos.transform.localScale = new Vector3(.2f, .2f, .2f);
        visPos.transform.localPosition = newCameraPos; */
        transform.rotation = targetRotation;
    }

    private Vector3 calculateNextPoint(float timeFromStart)
    {
        int currentIteration = (int)Mathf.Floor(timeFromStart / speed);
        if (interpolation) currentIteration -= 2;
        List<Vector3> anchorPoints = new List<Vector3>();
        for (int i = 0; i < 4; i++)
        {
            int currentIndex;
            if (interpolation) {
                currentIndex = Mathf.Min(Mathf.Max(0, currentIteration + i), pointsPosition.Length - 1);
            } else {
                currentIndex = (currentIteration + i) % pointsPosition.Length;
                if (currentIndex < 0) {
                    currentIndex += pointsPosition.Length;
                }
            }
            anchorPoints.Add(pointsPosition[currentIndex]);
        }
        float currentTime = timeFromStart % speed / speed;
        Vector3 nextPoint = bSplineCalculation(anchorPoints, currentTime);
        return nextPoint;
    }

    private Vector3 bSplineCalculation(List<Vector3> points, float t)
    {
        Vector3 a = points[0], b = points[1], c = points[2], d = points[3];
        return ((-a + 3 * b - 3 * c + d) * Mathf.Pow(t, 3) +
                (3 * a - 6 * b + 3 * c) * Mathf.Pow(t, 2) +
                (-3 * a + 3 * c) * t +
                (a + 4 * b + c)) / 6;
    }
}

using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(LineRenderer))]
public class SignatureDrawer : MonoBehaviour
{
    [Header("Drawing Settings")]
    public float minDistance = 0.01f;
    public SignatureArea drawArea;

    private LineRenderer line;
    private List<Vector3> points = new List<Vector3>();
    private bool isDrawing;

    void Awake()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = 0;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (drawArea != null && drawArea.IsInside(mousePos))
            {
                StartDrawing(mousePos);
            }
        }

        if (Input.GetMouseButton(0) && isDrawing)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (drawArea.IsInside(mousePos))
                AddPoint(mousePos);
        }

        if (Input.GetMouseButtonUp(0) && isDrawing)
        {
            EndDrawing();
        }
    }

    void StartDrawing(Vector2 startPos)
    {
        isDrawing = true;
        points.Clear();
        line.positionCount = 0;
        AddPoint(startPos);
    }

    void AddPoint(Vector2 point)
    {
        if (points.Count == 0 || Vector2.Distance(points[^1], point) > minDistance)
        {
            points.Add(point);
            line.positionCount = points.Count;
            line.SetPosition(points.Count - 1, point);
        }
    }

    void EndDrawing()
    {
        isDrawing = false;
        SignatureEvaluator.Instance.Evaluate(points);
    }

    public List<Vector3> GetDrawnPoints() => points;
}
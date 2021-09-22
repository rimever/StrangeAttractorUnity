using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LorenzAttractor : MonoBehaviour
{
    private LineRenderer _lineRenderer;

    private int _verticesNum = 4000;

    private float p;

    private float r;

    private float b;

    private float dt;

    public Vector3 start;

    // Start is called before the first frame update
    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        InitializeParameters();
        _lineRenderer.positionCount = _verticesNum;
        _lineRenderer.SetPositions(CreateVertices());
    }

    private Vector3[] CreateVertices()
    {
        var v = new Vector3[_verticesNum];
        v[0] = start;
        for (int i = 0; i < v.Length - 1; i++)
        {
            v[i + 1] = SolveSimultaneousEquation(v[i]);
            if (v[i + 1].magnitude > 10000f) return v;
        }

        return v;
    }

    private Vector3 SolveSimultaneousEquation(Vector3 v)
    {
        Vector3 next;
        next.x = dt * (-p * v.x + p * v.y) + v.x;
        next.y = dt * (-v.x * v.z + r * v.x - v.y) + v.y;
        next.z = dt * (v.x * v.y - b * v.z) + v.z;
        return next;
    }

    private void InitializeParameters()
    {
        p = 10f;
        r = 28f;
        b = 8f / 3f;
        dt = 0.01f;
        start = new Vector3(1, 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnPValueChanged(float v)
    {
        this.p = v;
        _lineRenderer.SetPositions(CreateVertices());
    }

    public void OnRValueChanged(float v)
    {
        this.r = v;
        _lineRenderer.SetPositions(CreateVertices());
    }

    public void OnBValueChanged(float v)
    {
        this.b = v;
        _lineRenderer.SetPositions(CreateVertices());
    }

    public void ResetParameter()
    {
        InitializeParameters();
        _lineRenderer.SetPositions(CreateVertices());
    }
}
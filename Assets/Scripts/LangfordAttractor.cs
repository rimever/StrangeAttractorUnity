using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LangfordAttractor : MonoBehaviour
{
    /// <summary>
    /// 計算精度を上げるため、Decimalで扱うVector
    /// </summary>
    struct DecimalVector
    {
        public decimal x;
        public decimal y;
        public decimal z;
    }

    private LineRenderer _lineRenderer;

    private int _verticesNum = 100000;

    private decimal d = 1;
    private decimal u = 0.7m;
    private decimal n = 0.6m;
    private decimal w = 3.5m;
    private decimal l = 0.25m;
    private decimal e = 0;
    private decimal dt = 0.01m;


    // Start is called before the first frame update
    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        var vertices = CreateVertices();
        _lineRenderer.positionCount = vertices.Length;
        _lineRenderer.SetPositions(vertices);
    }


    private Vector3[] CreateVertices()
    {
        return SolveDecimalVertices().Select(v => new Vector3((float) v.x, (float) v.y, (float) v.z)).ToArray();
    }

    private IEnumerable<DecimalVector> SolveDecimalVertices()
    {
        DecimalVector now = new DecimalVector() {x = 1, y = 1, z = 1};
        foreach (var i in Enumerable.Range(0, _verticesNum))
        {
            if (i != 0)
            {
                now = SolveSimultaneousEquation(now);
            }

            Debug.Log($"{i}:{now.x},{now.y},{now.z}");

            yield return now;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// dx2 <- z2*x2 - u*x2 - w*y2
    /// dy2 <- w*x2 + z2*y2 - u*y2
    /// dz2 <- n + d*z2 - z2^3/3 - x2^2 - y2^2 - l*z2*x2^2  - l*z2*y2^2+ e*z2*x2^3
    /// </remarks>
    /// <param name="v"></param>
    /// <returns></returns>
    private DecimalVector SolveSimultaneousEquation(DecimalVector v)
    {
        DecimalVector next;
        next.x = dt * (v.z * v.x - u * v.x - w * v.y) + v.x;
        next.y = dt * (w * v.x + v.z * v.y - u * v.y) + v.y;
        next.z = dt * (n + d * v.z - v.z * v.z * v.z / 3 - v.x * v.x - v.y * v.y -
            l * v.z * v.x * v.x - l * v.z * v.y * v.y + e * v.z * v.x * v.x * v.x) + v.z;
        return next;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Timer : MonoBehaviour
{


    [SerializeField]
    private Vector3 Axis;

    [Header("Line")]
    private GradientAlphaKey[] startalphaKeys; 
    private GradientAlphaKey[] alphaKeys;
    private GradientColorKey[] colorKeys;
    [SerializeField]
    private Color LineColor = Color.white;
    [SerializeField]
    [Range(0f, 1f)]
    private float StartAlpha;
    [SerializeField]
    private float Radius;
    [SerializeField]
    private int Edges;
    [SerializeField]
    private float EdgeWidth;
    [SerializeField]
    private List<Vector3> EdgePoints = new List<Vector3>();
    private LineRenderer lineRenderer;
    private Gradient gradient;

    public UnityEvent OnTimerStart;
    public UnityEvent OnTimerEnd;

    private bool timerRunning = false;
    
    

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        startalphaKeys = new GradientAlphaKey[3];
        startalphaKeys[0].alpha = StartAlpha;
        startalphaKeys[0].time = 0f;
        startalphaKeys[1].alpha = StartAlpha;
        startalphaKeys[1].time = 0.999f;
        startalphaKeys[2].alpha = 0f;
        startalphaKeys[2].time = 1f;
        colorKeys = new GradientColorKey[2];
        colorKeys[0].color = LineColor;
        colorKeys[0].time = 0.0f;
        colorKeys[1].color = LineColor;
        colorKeys[1].time = 1.0f;
        alphaKeys = new GradientAlphaKey[3];
        gradient = new Gradient();
        CreateEdges();
        HideGradient();
    }

    public void StartTimer(float time)
    {
        StartCoroutine(EventInvoker(time));
    }

    private IEnumerator EventInvoker(float time)
    {
        timerRunning = true;
        float t = Time.time + time;
        float tn = (t - Time.time) /time;
        ResetGradient();
        if (OnTimerStart != null)
            OnTimerStart.Invoke();
        while (t > Time.time)
        {
            tn = (t - Time.time) / time;
            alphaKeys[1].time = Mathf.Clamp(startalphaKeys[1].time * tn, 0.005f, 1f);
            alphaKeys[2].time = Mathf.Clamp(startalphaKeys[2].time * tn, 0.01f, 1f);
            gradient.SetKeys(lineRenderer.colorGradient.colorKeys, alphaKeys);
            lineRenderer.colorGradient = gradient;
            yield return null;
        }
        HideGradient();
        if (OnTimerEnd != null)
            OnTimerEnd.Invoke();
        timerRunning = false;
        
    }

    public bool TimerIsRunning()
    {
        return timerRunning;
    }

    private void ResetGradient()
    {
        for (int i = 0; i < startalphaKeys.Length; i++)
        {
            alphaKeys[i].time = startalphaKeys[i].time;
            alphaKeys[i].alpha = startalphaKeys[i].alpha;
        }

        lineRenderer.colorGradient.alphaKeys = alphaKeys;
    }

    private void HideGradient()
    {
        for (int i = 0; i < alphaKeys.Length; i++)
        {
            alphaKeys[i].alpha = 0f;
        }
        gradient.SetKeys(lineRenderer.colorGradient.colorKeys, alphaKeys);
        lineRenderer.colorGradient = gradient;
    }

    private void CreateEdges()
    {
        lineRenderer.positionCount = Edges;
        for (int i = 0; i < Edges; i++)
        {
            EdgePoints.Add(GetPointByAngle(transform.position, (360 / Edges * i) - (360 / Edges) / 2, Radius));
        }
        lineRenderer.loop = true;
        lineRenderer.startWidth = EdgeWidth;
        lineRenderer.endWidth = EdgeWidth;
        lineRenderer.SetPositions(EdgePoints.ToArray());
    }

    public Vector3 GetPointByAngle(Vector3 origin, float angle, float distance)
    {
        float x = distance * Mathf.Cos(angle * Mathf.Deg2Rad);
        float z = distance * Mathf.Sin(angle * Mathf.Deg2Rad);
        Vector3 p = origin;
        p.x += x;
        p.y += z;
        return p;
    }

}

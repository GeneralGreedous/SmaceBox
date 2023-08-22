using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ShipController : MonoBehaviour
{
    

    public static ShipController Instance;

    [SerializeField] private float _speed = 1;
    [SerializeField] private GameObject _MyTarget;
    [SerializeField] private float _flyingRange = 10;
    public float FlyingRange
    {
        get { return _flyingRange; }
        set { _flyingRange = value; }
    }


    [SerializeField] private GameObject _shipModel;
    public Vector3 ShipPosition { get { return _shipModel.transform.position; } }


    [SerializeField] private AnimationCurve _flightCurveStart;
    [SerializeField] private AnimationCurve _flightCurveFlying;

    [SerializeField] private LineRenderer _circleRenderer;
    [SerializeField] private LineRenderer _lineToTargetRenderer;


    private Coroutine _Coroutine;

    private bool _CoroutineIsRunning = false;

    private Material _LineToTargetMaterial;


    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("GalaxyManager already on scene");
        }
        Instance = this;
        _LineToTargetMaterial = _lineToTargetRenderer.material;
    }
    private void Start()
    {
        DrawCircle(100, _flyingRange);
    }


    public void SetTarget(GameObject target)
    {
        if (target != _MyTarget)
        {
            float asd = Vector3.Distance(transform.position, target.transform.position);

            if (Vector3.Distance(transform.position, target.transform.position) < FlyingRange)
            {
                _MyTarget = target;
                MoveMe();
            }
        }

    }

    IEnumerator FlyToTarget(AnimationCurve curve)
    {
        _CoroutineIsRunning = true;
        float time = 0;
        float time2 = 0;
        Vector3 startPos = transform.position;
        Vector3 endPos = _MyTarget.transform.position;
        float distance = Vector3.Distance(startPos, endPos);
        float distanceModifier = Mathf.Lerp(distance / FlyingRange, 1, 0.3f);


        while (transform.position != endPos)
        {
            time2 += Time.deltaTime;
            time += (Time.deltaTime / distanceModifier) * _speed;
            transform.position = Vector3.Lerp(startPos, _MyTarget.transform.position, curve.Evaluate(time));
            //transform.position = Vector3.MoveTowards(transform.position, _MyTarget.transform.position, Time.deltaTime * _speed);



            DrawCircle(100, _flyingRange);
            CameraController.Instance.SetCameraTarget(transform.position);
            yield return null;
        }
        DrawCircle(100, _flyingRange);
        _CoroutineIsRunning = false;
        Debug.Log(time2);
        yield return null;

    }

    private void MoveMe()
    {
        if (_CoroutineIsRunning)
        {
            StopCoroutine(_Coroutine);
            _Coroutine = StartCoroutine(FlyToTarget(_flightCurveFlying));
        }
        else
        {
            _Coroutine = StartCoroutine(FlyToTarget(_flightCurveStart));
        }

    }

    private void DrawCircle(int steps, float radius)
    {
        _circleRenderer.positionCount = steps;
        radius -= 0.25f;
        for (int i = 0; i < steps; i++)
        {
            float cirleProgres = (float)i / steps;
            float currentRadian = cirleProgres * 2 * Mathf.PI;

            float xScladed = Mathf.Cos(currentRadian);
            float yScladed = Mathf.Sin(currentRadian);

            float x = xScladed * radius;
            float y = yScladed * radius;

            Vector3 currentPosition = new Vector3(x, y, -0.1f) + transform.position;

            _circleRenderer.SetPosition(i, currentPosition);



        }
    }

    public void LineToTarget(bool turnOnOff)
    {

        _lineToTargetRenderer.enabled = turnOnOff;
    }

    public void LineToTarget(Vector3 position)
    {
        _LineToTargetMaterial.SetInt("_InRange", FlyingRange < Vector3.Distance(transform.position, position) ? 0 : 1);
        _lineToTargetRenderer.SetPosition(0, transform.position);
        _lineToTargetRenderer.SetPosition(1, position);
    }


    [Button]
    private void RunMove()
    {
        MoveMe();
    }

    [Button]
    private void ReDrawCircle()
    {
        DrawCircle(100, _flyingRange);
    }

}

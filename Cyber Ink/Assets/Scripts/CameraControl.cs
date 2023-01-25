using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private Transform target;
    private Vector3 offset = new Vector3(0f, 0f, -10f);
    private float smoothTime = 0.1f;
    private Vector3 velocity = Vector3.zero;

    //private Camera camera;
    //private BoxCollider2D cameraBox;
    //private float sizeX, sizeY, ratio;

    public AnimationCurve curve;
    private float duration = 0.2f;

    //private void Start()
    //{
    //    camera = GetComponent<Camera>();
    //    cameraBox = GetComponent<BoxCollider2D>();
    //}

    private void Update()
    {
        Vector3 targetposition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetposition, ref velocity, smoothTime);

        //sizeY = camera.orthographicSize * 2;
        //ratio = (float)Screen.width / (float)Screen.height;
        //sizeX = sizeY * ratio;
        //cameraBox.size = new Vector2(sizeX, sizeY);
    }
    
    public IEnumerator ScreenShake()
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float strength = curve.Evaluate(elapsedTime / duration);
            transform.position = startPosition + Random.insideUnitSphere * strength;
            yield return null;
        }

        transform.position = startPosition;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;

    //public Vector3 offset;
    [SerializeField] Vector3 offset;

    [SerializeField] float zoomSpeed = 4f;
    float minZoom = 5f;
    float maxZoom = 15f;
    private float currentZoom = 10f;
    [SerializeField] float pitch = 2f;

    private float yawSpeed = 100f;
    private float currentYaw = 0f;


    private void Update()
    {
        GetZoomScroll();
        GetRotation();
    }


    private void LateUpdate()
    {
        transform.position = target.position - offset * currentZoom;
        transform.LookAt(target.position + Vector3.up * pitch);

        transform.RotateAround(target.position, Vector3.up, currentYaw);
    }

    private void GetZoomScroll()
    {
        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
    }

    private void GetRotation()
    {
        currentYaw -= Input.GetAxis("Horizontal") * yawSpeed * Time.deltaTime;
    }
}

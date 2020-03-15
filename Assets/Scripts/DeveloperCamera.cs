﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DeveloperCamera : MonoBehaviour
{
    public float stickMinZoom = -45, stickMaxZoom = -250;
    public float swivelMinZoom = 45, swivelMaxZoom = 90;
    public float moveSpeedMinZoom = 100, moveSpeedMaxZoom = 400;
    public float rotationSpeed = 180;


    private Transform swivel, stick;
    private float zoom = 1f;
    private float rotationAngle;

    private void Awake()
    {
        swivel = transform.GetChild(0);
        stick = swivel.GetChild(0);
    }

    private void LateUpdate()
    {
        float zoomDelta = -Input.GetAxis("Mouse ScrollWheel")*0.5f;
        if (zoomDelta != 0f)
        {
            AdjustZoom(zoomDelta);
        }

        float rotationDelta = Input.GetAxis("Rotation");
        if (rotationDelta != 0f)
        {
            AdjustRotation(rotationDelta);
        }

        float xDelta = Input.GetAxis("Horizontal");
        float zDelta = Input.GetAxis("Vertical");
        if (xDelta != 0f || zDelta != 0f)
        {
            AdjustPosition(xDelta, zDelta);
        }
    }


    private void AdjustZoom(float delta)
    {
        zoom = Mathf.Clamp01(zoom + delta);

        float distance = Mathf.Lerp(stickMinZoom, stickMaxZoom, zoom);
        stick.localPosition = new Vector3(0f, 0f, distance);

        float angle = Mathf.Lerp(swivelMinZoom, swivelMaxZoom, zoom);
        swivel.localRotation = Quaternion.Euler(angle, 0f, 0f);
    }

    IEnumerator ZoomPositionCoroutine(float zoomStart, float zoomEnd, float time)
    {
        float zoom = 0;
        float zoomDiff = (zoomStart - zoomEnd);
        for (float i = 0; i < time; i += Time.deltaTime)
        {
            Debug.Log(zoom);
            float distance = Mathf.Lerp(stickMinZoom, stickMaxZoom, zoom);
            stick.localPosition = new Vector3(0f, 0f, distance);
            zoom += zoomDiff*(Time.deltaTime/time);
            yield return null;
        }
    }

    void AdjustPosition(float xDelta, float zDelta)
    {
        Vector3 direction = transform.localRotation * new Vector3(xDelta, 0f, zDelta).normalized;
        float damping = Mathf.Max(Mathf.Abs(xDelta), Mathf.Abs(zDelta));
        float distance = Mathf.Lerp(moveSpeedMinZoom, moveSpeedMaxZoom, zoom) * damping * Time.deltaTime;

        Vector3 position = transform.localPosition;
        position += direction * distance;
        transform.localPosition = position;
        //transform.localPosition = ClampPosition(position);
    }

    private Vector3 ClampPosition(Vector3 position)
    {
        //float xMax =
        //    (grid.chunkCountX * HexMetrics.chunkSizeX - 0.5f) *
        //    (2f * HexMetrics.innerRadius);
        //position.x = Mathf.Clamp(position.x, 0f, xMax);

        //float zMax =
        //    (grid.chunkCountZ * HexMetrics.chunkSizeZ - 1f) *
        //    (1.5f * HexMetrics.outerRadius);
        //position.z = Mathf.Clamp(position.z, 0f, zMax);

        return position;
    }

    void AdjustRotation(float delta)
    {
        rotationAngle += delta * rotationSpeed * Time.deltaTime;

        if (rotationAngle < 0f)
            rotationAngle += 360f;
        else if (rotationAngle >= 360f)
            rotationAngle -= 360f;

        transform.localRotation = Quaternion.Euler(0f, rotationAngle, 0f);
    }
}

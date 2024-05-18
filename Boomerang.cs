using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : MonoBehaviour
{
    public GameObject boomerangPrefab;
    public Transform firePoint;
    public float throwSpeed = 20f;
    public float throwDistance = 10f;
    public float returnSpeed = 15f;
    public float rotationSpeed = 1000f;

    private Vector3 startPosition;
    private bool isReturning = false;
    private GameObject currentBoomerang;

    void Update()
    {
        if (currentBoomerang != null)
        {
            if (!isReturning)
            {
                // Move the boomerang forward
                currentBoomerang.transform.position += currentBoomerang.transform.up * throwSpeed * Time.deltaTime;

                // Check if the boomerang reached the throw distance
                if (Vector3.Distance(startPosition, currentBoomerang.transform.position) >= throwDistance)
                {
                    isReturning = true;
                }
            }
            else
            {
                // Move the boomerang back to the start position (fire point)
                currentBoomerang.transform.position = Vector3.MoveTowards(currentBoomerang.transform.position, firePoint.position, returnSpeed * Time.deltaTime);

                // Check if the boomerang returned to the start position
                if (Vector3.Distance(firePoint.position, currentBoomerang.transform.position) <= 0.1f)
                {
                    Destroy(currentBoomerang);
                    currentBoomerang = null;
                    isReturning = false;
                }
            }

            // Rotate the boomerang
            currentBoomerang.transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        }
    }

    public void Throw()
    {
        if (currentBoomerang == null)
        {
            startPosition = firePoint.position;
            currentBoomerang = Instantiate(boomerangPrefab, firePoint.position, firePoint.rotation);
            isReturning = false;
        }
    }

}

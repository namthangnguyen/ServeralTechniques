using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StealthEnemy {

public class Enemy : MonoBehaviour
{
    public float rotationSpeed;
    public float distance;
    public LineRenderer lineOfSight;

    public Gradient redColor;
    public Gradient greenColor;

    void Start()
    {
        // Dòng này để raycast không bị mất bởi bản thân
        Physics2D.queriesStartInColliders = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);

        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance);
        if (hitInfo.collider != null) {
            // số 1 ở đầu biểu thị, đây là điểm END của Line, 0 là điểm bắt đầu
            lineOfSight.SetPosition(1, hitInfo.point);
            lineOfSight.colorGradient = redColor;

            if (hitInfo.collider.CompareTag("Player")) {
                Destroy(hitInfo.collider.gameObject);
            }

        } else {
            lineOfSight.SetPosition(1, transform.position + transform.up * distance);
            lineOfSight.colorGradient = greenColor;
        }
    
        lineOfSight.SetPosition(0, transform.position);
    }
}

}

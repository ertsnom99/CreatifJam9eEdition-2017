using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 120.0f;

    [SerializeField]
    private float maxLeftRotation = 70.0f;
    [SerializeField]
    private float maxRightRotation = 70.0f;

    private Vector3 zeroDegreeForward;

    private void Start()
    {
        zeroDegreeForward = transform.forward;
    }

    public void RotateCharacterWithInput(Hashtable inputs, bool debug = false)
    {
        float rotY = (float)inputs["xAxis"] * rotationSpeed * Time.deltaTime;
        RotateWithAngle(rotY);

        LimitRotation();
    }

    private void RotateWithAngle(float angle)
    {
        transform.rotation *= Quaternion.AngleAxis(angle, Vector3.up);
    }

    private void LimitRotation()
    {
        float angle = GetAngleRelatitvetoZero();

        if (angle < 0 && angle < -maxLeftRotation)
        {
            RotateWithAngle(maxLeftRotation + angle);
        }
        else if (angle >= 0 && angle > maxRightRotation)
        {
            RotateWithAngle(angle - maxRightRotation);
        }
    }

    // Returns the angle relative to the "0 foward axis"
    private float GetAngleRelatitvetoZero()
    {
        float rot = Vector3.Angle(transform.forward, zeroDegreeForward);
        float dir = Mathf.Sign(Vector3.Cross(transform.forward, zeroDegreeForward).y);
        
        return rot * dir;
    }
}

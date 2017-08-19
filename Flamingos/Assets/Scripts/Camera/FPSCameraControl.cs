using System.Collections;
using UnityEngine;

public class FPSCameraControl : MonoBehaviour
{
    // The camera will turn around it's local X axis. 

    // The angle that is calculated and used to limit the rotation his calculated as follow:
    // Starting from the "0 degree forward", turn clockwise around the local X axis. 
    // This gives a range of possible angles of 0 to 360 with 360 exclusive.

    // 0 degres, for the angle, means that the camera is aligned with the "0 degree forward", but that
    // axis is defined differently according to the parenting of the camera:
    // -if the camera has no parent, the "0 degree forward" will be the world space forward Vector3
    // -if the camera has a parent, the "0 degree forward" will be the parent forward Vector3 and will be updated
    // during each rotation

    // The rotation can be limited by changing the fromAngle and toAngle. 

    // The limitation of the camera is done like follow:
    // The zone where the camera is allow to rotate is the one define by taking the fromAngle than rotating
    // anticlockwise until reaching the toAngle.
    // The value of fromAngle and toAngle should be from 0 to 360 with 360 exclusive. 360 degrees actually is
    // the same as 0 degrees, but 0 degrees should be used insted.

    // If the zone which defines where the camera can't rotate (which is called "dead zone") to is to small, it might
    // skip it. To prevent that change the angleCorrection variable. This represente to how much decimals the angle
    // is rounded. The smaller it is, to less likely it will be that the camera go over the "dead zone", but it must
    // likely going to cause some glitching effect. A value of one allow a good tolerance to a "dead zone" of 1 degrees
    // but will create a subtil glitching effect. A value of 2 will give a good tolerance to a "dead zone" of 5 degrees
    // with no glitching effect. 

    // To allow the camera to do a full turns, use the same minAngle and maxAngle

    // To prevent the camera from rotating, set the frozenRotation variable to true

    // It is highly recommanded to place the camera in a way that will make his forward Vector3 aligned with the "0 degree
    // forward" it will use. Not doing so might create some problems. If you want the "0 degree forward" used to be a
    // specifique one, put the camera in an empty gameobject and rotate it to have his forward Vector3 in the direction you
    // want your "0 degree forward".

    private Vector3 zeroDegreeForward;

    private float turnSpeed;
    private float fromAngle;
    private float toAngle;
    private float lockOnSpeed;

    private int angleCorrection;
    
    // Accessor of the frozen rotation state
    public bool FrozenRotation { get; private set; }

    private void Awake()
    {
        InitializeVariables();
    }

    private void InitializeVariables()
    {
        if (transform.parent == null) zeroDegreeForward = Vector3.forward;

        turnSpeed = 135.0f;
        fromAngle = 50f;
        toAngle = 310f;
        lockOnSpeed = 100.0f;

        angleCorrection = 2;
        FrozenRotation = false;
    }

    private void Start()
    {
        // If the camera has a parent, the "0 degree forward" must stored again in case that the parent rotated
        if (transform.parent != null) zeroDegreeForward = transform.parent.forward;

        float angle = GetAngle();

        if ((fromAngle > toAngle && !(angle >= toAngle && angle <= fromAngle)) ||
            (fromAngle < toAngle && angle < toAngle && angle > fromAngle))
        {
            float adjustRot = fromAngle - angle;
            transform.rotation *= Quaternion.AngleAxis(adjustRot, Vector3.right);
        }
    }
    
    public void RotateCameraWithInput(Hashtable inputs, bool debug = false)
    {
        // If the camera has a parent, the "0 degree forward" must stored again in case that the parent rotated
        if (transform.parent != null) zeroDegreeForward = transform.parent.forward;

        if (!FrozenRotation)
        {
            float previousAngle = GetAngle();
            float rotDoneAngle = 0;

            if (inputs["lockOnTargetInput"] != null)
            {
                rotDoneAngle = RotateTowardTarget((Vector3)inputs["lockOnTargetInput"], debug);
            }
            else
            {
                rotDoneAngle = RotateCamera(inputs);
            }

            if (fromAngle != toAngle) LimitRotation(previousAngle, rotDoneAngle);
        }

        if (debug)
        {
            Debug.DrawLine(transform.position, transform.position + zeroDegreeForward, Color.blue);
            Debug.DrawLine(transform.position, transform.position + transform.forward, Color.green);
        }
    }
    
    // Returns the angle relative to the "0 foward axis"
    private float GetAngle()
    {
        float rot = Vector3.Angle(transform.forward, zeroDegreeForward);
        rot = Mathf.Round(rot * Mathf.Pow(10f, angleCorrection)) / Mathf.Pow(10f, angleCorrection);

        float rotDir;

        if (transform.parent != null)
        {
            rotDir = Mathf.Sign(transform.parent.InverseTransformDirection(transform.forward).y);
        }
        else
        {
            rotDir = Mathf.Sign(transform.forward.y);
        }

        if (rotDir == 1 && rot != 0) rot = 360 - rot;
        
        return rot;
    }

    private float RotateCamera(Hashtable inputs)
    {
        float rotX = (float)inputs["yAxis"] * turnSpeed * Time.deltaTime;

        transform.rotation *= Quaternion.AngleAxis(rotX, Vector3.right);

        return rotX;
    }

    private float RotateTowardTarget(Vector3 target, bool debug = false)
    {
        Vector3 cameraToTarget = target - transform.position;

        Quaternion lookRotation = Quaternion.LookRotation(cameraToTarget);
        float pitchAngle = lookRotation.eulerAngles.x;

        // Calculate the correct necessary angle for the camera to have to be looking at the target
        if (transform.parent != null)
        {
            Vector3 localCameraToTarget = transform.parent.InverseTransformDirection(cameraToTarget);
            if (Mathf.Sign(localCameraToTarget.y) < 0 && Mathf.Sign(localCameraToTarget.z) < 0) pitchAngle = 180 - pitchAngle;
            if (Mathf.Sign(localCameraToTarget.y) > 0 && Mathf.Sign(localCameraToTarget.z) < 0) pitchAngle = 540 - pitchAngle;
        }
        else
        {
            if (Mathf.Sign(cameraToTarget.y) < 0 && Mathf.Sign(cameraToTarget.z) < 0) pitchAngle = 180 - pitchAngle;
            if (Mathf.Sign(cameraToTarget.y) > 0 && Mathf.Sign(cameraToTarget.z) < 0) pitchAngle = 540 - pitchAngle;
        }

        float angle = GetAngle();

        float rotation = Mathf.MoveTowardsAngle(angle, pitchAngle, lockOnSpeed * Time.deltaTime);
        
        transform.localRotation = Quaternion.Euler(rotation, 0, 0);

        if (debug) Debug.DrawLine(transform.position, transform.position + cameraToTarget, Color.red);

        return calculateRotationDone(angle, rotation);
    }
    
    private float calculateRotationDone(float previousAngle, float angle)
    {
        // Calculate if the rotation was clockwise or counterclockwise
        float angleDif = angle - previousAngle;
        if (angleDif < 0) angleDif += 360;
        
        float direction = -1f;
        if (angleDif > 0 && angleDif < 180) direction = 1f;

        float rotation = 0;

        // Calculate how much rotation was done
        if (direction == 1f)
        {
            if (previousAngle <= angle)
            {
                rotation = direction * (angle - previousAngle);
            }
            else
            {
                rotation = direction * (360 - previousAngle + angle);
            }
        }
        else
        {
            if (previousAngle <= angle)
            {
                rotation = direction * (360 - angle + previousAngle);
            }
            else
            {
                rotation = direction * (previousAngle - angle);
            }
        }

        return rotation;
    }

    // Adjuste the rotation if out of bounds
    private void LimitRotation(float previousAngle, float lastRotAngle)
    {
        float angle = GetAngle();

        // Check if the camera rotation is in the "dead zone"
        if (fromAngle < toAngle && angle < toAngle && angle > fromAngle)
        {
            if (lastRotAngle > 0)
            {
                float adjustRot = fromAngle - angle;
                transform.rotation *= Quaternion.AngleAxis(adjustRot, Vector3.right);
            }
            else if (lastRotAngle < 0)
            {
                float adjustRot = toAngle - angle;
                transform.rotation *= Quaternion.AngleAxis(adjustRot, Vector3.right);
            }

            return;
        }
        else if (fromAngle > toAngle && !(angle >= toAngle && angle <= fromAngle))
        {
            if (lastRotAngle > 0)
            {
                float adjustRot = fromAngle - angle;
                transform.rotation *= Quaternion.AngleAxis(adjustRot, Vector3.right);
            }
            else if (lastRotAngle < 0)
            {
                float adjustRot = toAngle - angle;
                transform.rotation *= Quaternion.AngleAxis(adjustRot, Vector3.right);
            }

            return;
        }

        // Check if the camera went over the limit
        float maxRot = maxRotationAuthorized(previousAngle, lastRotAngle);
        
        if (Mathf.Abs(lastRotAngle) > maxRot)
        {
            if (lastRotAngle > 0)
            {
                float adjustRot = fromAngle - angle;
                transform.rotation *= Quaternion.AngleAxis(adjustRot, Vector3.right);
            }
            else if (lastRotAngle < 0)
            {
                float adjustRot = toAngle - angle;
                transform.rotation *= Quaternion.AngleAxis(adjustRot, Vector3.right);
            }

            return;
        }
    }

    // Return how much rotation the camera can do before reaching the limit
    private float maxRotationAuthorized(float previousAngle, float lastRotAngle)
    {
        float maxRot = 0;

        if (fromAngle > toAngle)
        {
            if (lastRotAngle > 0)
            {
                if (previousAngle > fromAngle)
                {
                    maxRot = (360 - previousAngle) + fromAngle;
                }
                else
                {
                    maxRot = fromAngle - previousAngle;
                }
            }
            else if (lastRotAngle < 0)
            {
                if (previousAngle > fromAngle || (previousAngle <= fromAngle && previousAngle >= toAngle))
                {
                    maxRot = previousAngle - toAngle;
                }
                else
                {
                    maxRot = (360 - toAngle) + previousAngle;
                }
            }
        }
        else if (fromAngle < toAngle)
        {
            if (lastRotAngle > 0)
            {
                if (previousAngle <= fromAngle)
                {
                    maxRot = fromAngle - previousAngle;
                }
                else
                {
                    maxRot = (360 - previousAngle) + fromAngle;
                }
            }
            else if (lastRotAngle < 0)
            {
                if (previousAngle >= toAngle)
                {
                    maxRot = previousAngle - toAngle;
                }
                else
                {
                    maxRot = 360 - toAngle + previousAngle;
                }
            }
        }

        return maxRot;
    }

    public void RotateCameraWithAngle(float angle)
    {
        if (!FrozenRotation) transform.localRotation = Quaternion.Euler(angle, 0, 0);
    }
}

using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target to follow")]
    public Transform target;
    [Header("Distance target can move before camera follows")]
    public Vector2 margin = new Vector2(0.0f, 2.0f);
    public float lookAheadFactor = 2f;
    [Header("How smooth it follows")]
    public Vector2 smooth = new Vector2(3.0f, 3.0f);
    [Header("Max x and y coords camera can have")]
    public Vector2 borderMax = new Vector2(200f, 200f);
    [Header("Min x and y coords camera can have")]
    public Vector2 borderMin = new Vector2(-200f, -200f);

    private void Update()
    {
        if (target != null)
        {
            TrackTarget();
        }
    }

    private bool CheckXMargin()
    {
        // Returns true if the distance between the camera and the target in the x axis is greater than the x margin.
        return Mathf.Abs(transform.position.x - target.position.x) > margin.x;
    }


    private bool CheckYMargin()
    {
        // Returns true if the distance between the camera and the target in the y axis is greater than the y margin.
        return Mathf.Abs(transform.position.y - target.position.y) > margin.y;
    }

    private void TrackTarget()
    {
        // By default the target x and y coordinates of the camera are it's current x and y coordinates.
        float targetX = transform.position.x;
        float targetY = transform.position.y;

        // If the target has moved beyond the x margin...
        if (CheckXMargin())
        {
            // ... the target x coordinate should be a Lerp between the camera's current x position and the target's current x position.
            float lookAhead = lookAheadFactor * target.localScale.x;
            targetX = Mathf.Lerp(transform.position.x, target.position.x + lookAhead, smooth.x * Time.deltaTime);
        }

        // If the target has moved beyond the y margin...
        if (CheckYMargin())
        {
            // ... the target y coordinate should be a Lerp between the camera's current y position and the target's current y position.
            targetY = Mathf.Lerp(transform.position.y, target.position.y, smooth.y * Time.deltaTime);
        }

        // The target x and y coordinates should not be larger than the maximum or smaller than the minimum.
        targetX = Mathf.Clamp(targetX, borderMin.x, borderMax.x);
        targetY = Mathf.Clamp(targetY, borderMin.y, borderMax.y);

        // Set the camera's position to the target position with the same z component.
        transform.position = new Vector3(targetX, targetY, transform.position.z);
    }
}

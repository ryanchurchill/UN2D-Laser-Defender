using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    // config
    [SerializeField] List<Transform> waypoints;
    [SerializeField] float moveSpeed = 2;

    // other
    int targetWaypointIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        targetWaypointIndex = 0;
        jumpToWayPointAtIndex(targetWaypointIndex);
        targetWaypointIndex++;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessWaypointNavigationForFrame();
    }

    private void ProcessWaypointNavigationForFrame()
    {
        if (!targetWaypointExceedsLastWaypoint())
        {
            if (moveToWaypointAtIndex(targetWaypointIndex))
            {
                targetWaypointIndex++;
            }
        }
    }

    void jumpToWayPointAtIndex(int index)
    {
        transform.position = waypoints[index].transform.position;
    }

    // return true once we reach destination
    bool moveToWaypointAtIndex(int index)
    {
        Vector3 targetPosition = waypoints[index].transform.position;
        float movementThisFrame = moveSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);
        return (transform.position == targetPosition);
    }

    bool targetWaypointExceedsLastWaypoint()
    {
        return targetWaypointIndex >= waypoints.Count;
    }
}

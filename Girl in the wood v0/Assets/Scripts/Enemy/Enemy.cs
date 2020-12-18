using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 1f;
    public float waitTime = 3f;
    public float rotateSpeed = 90f;

    public Transform pathHolder;

    Vector3[] waypoints;

    void Start()
    {
        waypoints = new Vector3[pathHolder.childCount];
        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = pathHolder.GetChild(i).position;
            waypoints[i].y = transform.position.y;
        }

        StartCoroutine(FollowPath(waypoints));
    }

    IEnumerator FollowPath(Vector3[] waypoints)
    {
        transform.position = waypoints[0];

        int targetIndex = 1;
        Vector3 targetWaypoint = waypoints[targetIndex];
        Vector3 toTarget = targetWaypoint - transform.position;
        Quaternion targetDirection = Quaternion.LookRotation(new Vector3(toTarget.x, 0, toTarget.z));
        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetWaypoint, speed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetDirection, 0.4f * Time.deltaTime * rotateSpeed);
            if (transform.position == targetWaypoint)
            {
                targetIndex = (targetIndex + 1) % waypoints.Length;
                targetWaypoint = waypoints[targetIndex];
                targetDirection = Quaternion.LookRotation(new Vector3(toTarget.x, 0, toTarget.z));
                yield return new WaitForSeconds(waitTime);
            }
            yield return null;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;

        Vector3 startPoint = pathHolder.GetChild(0).position;
        Vector3 previousPoint = startPoint;
        foreach (Transform waypoint in pathHolder)
        {
            Gizmos.DrawSphere(waypoint.position, .3f);
            Gizmos.DrawLine(previousPoint, waypoint.position);
            previousPoint = waypoint.position;
        }
        Gizmos.DrawLine(previousPoint, startPoint);
    }
}

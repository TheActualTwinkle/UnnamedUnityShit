using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ViewRaycaster
{
    public static bool InView(Transform origin, Transform target, float targetColliderRadius)
    {
        List<RaycastHit2D> hitsTop = new List<RaycastHit2D>();
        List<RaycastHit2D> hitsBot = new List<RaycastHit2D>();
        List<Vector3> tangents = GetTangentPoints
        (
            origin.position.x,
            origin.position.y,
            target.position.x,
            target.position.y,            
            targetColliderRadius * Mathf.Max(target.localScale.x, target.localScale.y) * 0.985f
        );

        Physics2D.Raycast(origin.position, tangents[0] - origin.position, new ContactFilter2D().NoFilter(), hitsTop);
        Physics2D.Raycast(origin.position, tangents[1] - origin.position, new ContactFilter2D().NoFilter(), hitsBot);

        if (hitsTop.Count > 1 || hitsBot.Count > 1)
        {
            return true;
        }

        return false;
    }

    private static List<Vector3> GetTangentPoints(float fromX, float fromY, float toX, float toY, float radius)
    {
        var points = new List<Vector3>();

        var distanceX = toX - fromX;
        var distanceY = toY - fromY;

        var distanceToCenter = Mathf.Sqrt(distanceX * distanceX + distanceY * distanceY);

        var r2 = distanceToCenter * distanceToCenter - radius * radius;

        var d = r2 / distanceToCenter;

        var h = Mathf.Sqrt(r2 - d * d);

        points.Add(new Vector3(fromX + (distanceX * d - distanceY * h) / distanceToCenter, fromY + (distanceY * d + distanceX * h) / distanceToCenter, 0f));
        points.Add(new Vector3(fromX + (distanceX * d + distanceY * h) / distanceToCenter, fromY + (distanceY * d - distanceX * h) / distanceToCenter, 0f));

        return points;
    }
}

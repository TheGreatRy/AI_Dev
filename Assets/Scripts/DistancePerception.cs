using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DistancePerception : Perception
{
    public override GameObject[] getGameObjects()
    {
        List<GameObject> result = new List<GameObject>();

        var colliders = Physics.OverlapSphere(transform.position, maxDistance);

        foreach (var collider in colliders)
        {
            //do not include ourselves
            if (collider.gameObject == gameObject) continue;
            //check for matching tag
            if (tagName == "" || collider.tag == tagName)
            {
                //check within max angle range
                Vector3 direction = collider.transform.position - transform.position;
                float angle = Vector3.Angle(direction, transform.forward);
                if (angle <= maxAngle)
                {
                    result.Add(collider.gameObject);

                }
            }


        }
        return result.ToArray();
    }
}

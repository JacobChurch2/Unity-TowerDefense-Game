using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    public List<Transform> _waypoints;
    public static PathManager Instance;

    private void Awake()
    {
        Instance = this;

        if (_waypoints.Count==0)
        {
            _waypoints= new List<Transform>();
            for (int i = 0; i < transform.childCount; i++)
            {
                _waypoints.Add(transform.GetChild(i));
            }
        }
    }

    private void OnDrawGizmos()
    {
        //just to visually see the path which we are drawing ..
        Gizmos.color = Color.red;
        for (int i = 0; i < _waypoints.Count; i++)
        {
            if (i != _waypoints.Count - 1)
            {
                Gizmos.DrawLine(_waypoints[i].position, _waypoints[i + 1].position);
            }
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneScript : MonoBehaviour
{
    Vector3 point_on_plane, normal_to_plane;

    public Vector3 normal { get { return normal_to_plane; } }

    // Start is called before the first frame update
    void Start()
    {
        define_existing_planes();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void define_plane(Vector3 point, Vector3 normal)
    {
        point_on_plane = point;
        normal_to_plane = normal.normalized;
        transform.position = point_on_plane;
        transform.up = normal_to_plane;
    }

    public float distance_to(Vector3 point)
    {
        Vector3 point_on_plane_to_point = point - point_on_plane;

        return Vector3.Dot(point_on_plane_to_point, normal_to_plane);
    }

    public void define_existing_planes()
    {
        switch (transform.name)
        {
            case "PlaneA":
                define_plane(new Vector3(0, -1, 0), new Vector3(0.03f, 1, 0));
                break;

            case "PlaneB":
                define_plane(new Vector3(0, -1, 0), new Vector3(-0.03f, 1, 0));
                break;

            case "PlaneC":
                define_plane(new Vector3(-5f, -1, 0), new Vector3(2f, 1, 0));
                break;

            case "PlaneD":
                define_plane(new Vector3(5f, -1, 0), new Vector3(-2f, 1, 0));
                break;

            case "PlaneE":
                define_plane(new Vector3(0, -1, 6f), new Vector3(0, 1, -2f));
                break;

            default:
                define_plane(new Vector3(0, -1, 0), new Vector3(0, 1, 0));
                break;
        }
    }
}

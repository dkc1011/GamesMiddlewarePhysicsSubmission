using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Sphere : MonoBehaviour
{
    private Vector3 velocity = new Vector3(0, 0, 0);
    private Vector3 acceleration = new Vector3(0, -9.8f, 0);
    public float Coefficient_Of_Restitution = 0.6f;
    private float sphere_radius;
    private Vector3 velocity_perpendicular;
    private Vector3 velocity_parallel;
    public float mass;
    private Mesh mesh;


    public float Radius
    {
        get { return transform.localScale.x / 2.0f; }
        set { transform.localScale = value * 2.0f * Vector3.one;
            sphere_radius = value;
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void set_velocity(Vector3 velocity)
    {
        this.velocity = velocity;
    }
    public Vector3 get_velocity()
    {
        return velocity;
    }

    public float get_radius()
    {
        return sphere_radius;
    }

    public float get_mass()
    {
        return mass;
    }

    public float distance_to(Vector3 point)
    {
        return Vector3.Distance(transform.position, point);
    }

    public void addVelocity(Vector3 increaseValue)
    {
        velocity += increaseValue;
    }

    public Vector3 get_acceleration()
    {
        return acceleration;
    }
    public Vector3 perpendicular_component(Vector3 vec, Vector3 normal)
    {
        return vec - parallell_component(vec, normal);
    }

    public Vector3 parallell_component(Vector3 vec, Vector3 normal)
    {
        return Vector3.Dot(vec, normal) * normal;
    }

}

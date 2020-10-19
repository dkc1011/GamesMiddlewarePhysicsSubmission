using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public List<Sphere> AllSpheres = new List<Sphere>();
    public List<PlaneScript> AllPlanes = new List<PlaneScript>();
    public GameObject spherePrefab;


    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject sphere in GameObject.FindGameObjectsWithTag("Sphere"))
        {
            AllSpheres.Add(sphere.GetComponent<Sphere>());
        }

        foreach (GameObject plane in GameObject.FindGameObjectsWithTag("Plane"))
        {
            AllPlanes.Add(plane.GetComponent<PlaneScript>());
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            spawnBall();
        }

        for (int i = 0; i < AllSpheres.Count - 1; i++)
            for (int j = i + 1; j < AllSpheres.Count; j++)
            {
                Sphere sphere = AllSpheres[i];
                Sphere otherSphere = AllSpheres[j];

                float d1 = otherSphere.distance_to(sphere.transform.position) - sphere.get_radius() - otherSphere.get_radius();
                Vector3 proposedPositionofSphere = sphere.transform.position + sphere.get_velocity()*Time.deltaTime;
                Vector3 proposedPositionofotherSphere = otherSphere.transform.position + otherSphere.get_velocity()*Time.deltaTime;
                float d2 = Vector3.Distance(proposedPositionofSphere,proposedPositionofotherSphere) - sphere.get_radius() - otherSphere.get_radius();

                if (sphere.distance_to(otherSphere.transform.position) <= sphere.get_radius() + otherSphere.get_radius())
                    {
                        print("SphereCollide");
                        Vector3 s1Normal = (sphere.transform.position - otherSphere.transform.position).normalized;
                        Vector3 s2Normal = -s1Normal;

                        Vector3 V1Parallell = sphere.parallell_component(sphere.get_velocity(), s1Normal);
                        Vector3 V2Parallell = otherSphere.parallell_component(otherSphere.get_velocity(), s2Normal);

                        Vector3 V1Perp = sphere.perpendicular_component(sphere.get_velocity(), s1Normal);
                        Vector3 V2Perp = otherSphere.perpendicular_component(sphere.get_velocity(), s2Normal);

                        float time_of_impact = Time.deltaTime * d1 / (d1 - d2);
                        sphere.transform.position -= sphere.get_velocity() * (Time.deltaTime - time_of_impact);
                        otherSphere.transform.position -= otherSphere.get_velocity() * (Time.deltaTime - time_of_impact);

                        float m1 = sphere.get_mass();
                        float m2 = otherSphere.get_mass();
                        Vector3 u1 = V1Parallell;
                        Vector3 u2 = V2Parallell;
                        
                        Vector3 newV1 = (((m1 - m2) / (m1 + m2) * u1) + (((2 * m2) / (m1 + m2)) * u2));
                        sphere.set_velocity(V1Perp + sphere.Coefficient_Of_Restitution * newV1);

                        Vector3 newV2 = (((m2 - m1) / (m1 + m2) * u2) + (((2 * m1) / (m1 + m2))*u1));
                        otherSphere.set_velocity(V2Perp + otherSphere.Coefficient_Of_Restitution * newV2);

                        sphere.transform.position += sphere.get_velocity() * (Time.deltaTime - time_of_impact);
                        otherSphere.transform.position += otherSphere.get_velocity() * (Time.deltaTime - time_of_impact);

                    }
            }    

        foreach (Sphere sphere in AllSpheres)
        {
            sphere.addVelocity(sphere.get_acceleration() * Time.deltaTime);
            sphere.transform.position += sphere.get_velocity() * Time.deltaTime;

            foreach (PlaneScript plane in AllPlanes)
            {
                float d1 = plane.distance_to(sphere.transform.position) - sphere.get_radius();

                Vector3 proposedPositionofSphere = sphere.transform.position + sphere.get_velocity() * Time.deltaTime;

                float distance_of_center_to_plane = plane.distance_to(proposedPositionofSphere);
                float d2 = distance_of_center_to_plane - sphere.get_radius();


                if(d2 <= 0)
                {
                    Vector3 para = sphere.parallell_component(sphere.get_velocity(), plane.normal);
                    Vector3 perp = sphere.perpendicular_component(sphere.get_velocity(), plane.normal);
                    float time_of_impact = Time.deltaTime * d1 / (d1 - d2);
                    sphere.transform.position -= sphere.get_velocity() * (Time.deltaTime - time_of_impact);

                    sphere.set_velocity(perp - sphere.Coefficient_Of_Restitution * para);

                    sphere.transform.position += sphere.get_velocity() * (Time.deltaTime - time_of_impact);
                }
            }
        }
    }

    private void spawnBall()
    {
        GameObject sphereObject = Instantiate(spherePrefab, new Vector3(UnityEngine.Random.Range(10.0f, -10.0f), UnityEngine.Random.Range(1.0f, 12.0f), UnityEngine.Random.Range(-7.0f, 0f)), Quaternion.identity);
        Sphere newSphere = sphereObject.GetComponent<Sphere>();
        newSphere.Radius = UnityEngine.Random.Range(0.5f,1f);
        newSphere.mass = UnityEngine.Random.Range(0.5f, 3f);
        AllSpheres.Add(newSphere);

    }

}

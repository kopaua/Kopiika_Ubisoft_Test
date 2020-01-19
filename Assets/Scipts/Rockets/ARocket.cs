using UnityEngine;
using System;

[Serializable]
public struct RocketSaveInfo
{
    public Vector3 Position;
    public Vector3 Rotation;
    public Vector3 Velocity;
    public eRocketType RocketType;
}

[Serializable]
public struct RocketData
{
    public eRocketType RocketType;
    public float Acceleration;
    public float Weight;
    public float Cooldown;
    public float Damage;
}

public enum eRocketType
{
    Big =0,
    Normal,
    Fast
}

[RequireComponent(typeof(Rigidbody))] 
public class ARocket : MonoBehaviour
{
    public RocketData rocketData;

    private Rigidbody myRigidbody;
    private GravityData[] gravityData;
    private Vector3 sunPosition;
    private const float distanceToDeath = 400;

    private void FixedUpdate()
    {
        foreach (GravityData planets in gravityData)
        {
            if (planets.planetTransform == null)
                continue;
            Vector3 direction = planets.planetTransform.position - transform.position;
            float distance = Vector3.Distance(transform.position, planets.planetTransform.position);
            float gravity = (myRigidbody.mass * planets.Mass) / (float)Math.Pow(distance, 2);
            Vector3 gravityForce = direction.normalized * gravity;
            myRigidbody.AddForce(gravityForce);
        }       
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, sunPosition) > distanceToDeath)
            Destroy(gameObject);
    }

    /// <summary>
    /// On collision with any objects rocket will be destroyed
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        ISetDamage planet = collision.gameObject.GetComponent<ISetDamage>();
        if (planet != null)
        {
            planet.SetDamage(rocketData.Damage);
        }
        Destroy(gameObject);
    }

    public void InitRocket(GravityData[] _gravity,Vector3 _sunPosition, bool isStartFromPlanet, Vector3 startVelocity)
    {
        gravityData = _gravity;
        sunPosition = _sunPosition;
        myRigidbody = GetComponent<Rigidbody>();
        myRigidbody.mass = rocketData.Weight;
        myRigidbody.velocity = startVelocity;
        if (isStartFromPlanet)
            myRigidbody.AddForce(transform.up * rocketData.Acceleration, ForceMode.Impulse);
    } 

    public RocketSaveInfo GetUpdatedInfoToSave()
    {
        RocketSaveInfo info = new RocketSaveInfo()
        {
            Position = transform.position,
            Rotation = transform.eulerAngles,
            Velocity = myRigidbody.velocity,
            RocketType = rocketData.RocketType
        };
        return info;
    }
  
}

using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public struct PlanetData
{
    public Vector3 CurrentPosition;
    public float Size;
    public float SpeedMove;
    public float SpeedRotation;
    public float CurrentRotation;
    public float HpMax;
    public float HpCurrent;
    public float FireRate;
    public float FireRateTimer;
    public eRocketType RocketType;   
    public Color MyColor;
}

[Serializable]
public struct GravityData
{
    public Transform planetTransform;
    public float Mass;
}

public abstract class APlanet : MonoBehaviour , ISetDamage
{
    private Vector3 sunPostion;
    private PlanetData planetData;   
    private GameObject currentRocket;    
    private bool isReloading;
    private PlanetHud planetHud;
    private RocketSaveInfo rockets;
    private PlanetTransfHelper planetTransfHelper;

    protected abstract void ReadyToFire();

    public void InitPlanet(PlanetData _planetData, GameObject rocket, Vector3 _sun)
    {
        planetHud = GetComponent<PlanetHud>();
        planetTransfHelper = GetComponent<PlanetTransfHelper>();
        planetData = _planetData;
        currentRocket = rocket;
        sunPostion = _sun;
        transform.position = planetData.CurrentPosition;
        planetTransfHelper.PlanetTransform.eulerAngles = new Vector3(0, 0, planetData.CurrentRotation);
        planetTransfHelper.PlanetTransform.GetComponent<MeshRenderer>().material.SetColor("_Color", planetData.MyColor);
        transform.localScale = new Vector3(planetData.Size, planetData.Size, planetData.Size);
        isReloading = true;
        OnHpChange();
    }

    public PlanetData GetUpdatedPlanetData()
    {
        planetData.CurrentPosition = transform.position;
        planetData.CurrentRotation = planetTransfHelper.PlanetTransform.eulerAngles.z;
        return planetData;       
    }

    public void SetDamage(float damage)
    {
        planetData.HpCurrent -= damage;
        OnHpChange();
        if (planetData.HpCurrent <= 0)
        {
            PlanetsManager.Singelton.DestroyPlanet(transform);
            Destroy(gameObject);
        }      
    }

    protected virtual void Update()
    {
        if (isReloading)
        {
            planetData.FireRateTimer -= Time.deltaTime;
            planetHud.SetReload(planetData.FireRateTimer, planetData.FireRate);
            if (planetData.FireRateTimer < 0)
            {
                isReloading = false;
                ReadyToFire();
            }
        }
    
        transform.RotateAround(sunPostion, Vector3.forward, planetData.SpeedMove * Time.deltaTime);
        transform.eulerAngles = Vector3.zero;
        planetTransfHelper.PlanetTransform.Rotate(0, 0, planetData.SpeedRotation * Time.deltaTime);
    }

    protected void FireRocket()
    {
        if (planetData.FireRateTimer <= 0)
        {
            isReloading = true;
            planetData.FireRateTimer = planetData.FireRate;
            List<GravityData> gravityData = PlanetsManager.Singelton.GetGravityData();
            Instantiate(currentRocket, planetTransfHelper.AimPoint.position, planetTransfHelper.PlanetTransform.rotation).GetComponent<ARocket>().InitRocket(gravityData.ToArray(),sunPostion ,true,Vector3.zero);
        }
    }

    private void OnHpChange()
    {
        planetHud.SetHp(planetData.HpCurrent, planetData.HpMax);
    }
   
 
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlanet : APlanet
{
    private void Awake()
    {
        ReadyToFire();
    }

    protected override void ReadyToFire()
    {
        StartCoroutine(ShootingLogic());
    }

    private IEnumerator ShootingLogic()
    {
        yield return new WaitForSeconds(Random.Range(0.5f, 3f));
        base.FireRocket();
    }
}

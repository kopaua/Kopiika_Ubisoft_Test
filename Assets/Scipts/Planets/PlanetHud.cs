using UnityEngine;

public class PlanetHud : MonoBehaviour
{

    [SerializeField]
    private Transform hpSlider, reloadSlider;

    public void SetHp(float current, float max)
    {      
        hpSlider.localScale = new Vector3(CalculateKof(current, max), 1,1);
    } 

    public void SetReload(float current, float max)
    {       
        reloadSlider.localScale = new Vector3(CalculateKof(current, max), 1, 1);
    }

    private float CalculateKof(float current, float max)
    {
        float kof = current / max;
        if (kof < 0)
            kof = 0;
        return kof;
    }
}

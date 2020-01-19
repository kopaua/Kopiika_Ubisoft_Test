using UnityEngine;
using System.Collections.Generic;

public class PlanetsManager : Singleton<PlanetsManager>
{
    [SerializeField]
    private Transform sun;
    [SerializeField]
    private GameObject planetePrefab;
    [SerializeField]
    private GameObject[] rocketPrefabs;

    #region Custom variables
    [Header("Custom variables")]
    [Range(2, 12)]
    public int PlanetsCount;
    /// <summary>
    /// Multiplier for planet scale to simulate mass
    /// </summary>
    [Tooltip("Multiplier for planet size to simulate mass")]
    public float GravityForce;
    [Range(.5f, 8f)]
    public float DistanceBetweenPlanets;

  
    private const float planetsSizeMin = 0.1f; 
    private const float planetsSizeMax = 0.3f;// 1 means planets will have sun size
    #endregion

    /// <summary>
    /// Planets size and distance are dependent on sun size
    /// </summary>    
    private float sunSize;
    private List<APlanet> planetsList = new List<APlanet>();
    private List<GravityData> gravityData = new List<GravityData>();   

    // Start is called before the first frame update
    void Awake()
    {
        sunSize = sun.transform.localScale.x;        
    }

    private void OnEnable()
    {
        MainMenuPanel.OnClickStart += CreatesPlanets;
        MainMenuPanel.OnClickLoad += LoadPlanetsFromSave;    
    }

    private void OnDisable()
    {
        MainMenuPanel.OnClickStart -= CreatesPlanets;
        MainMenuPanel.OnClickLoad -= LoadPlanetsFromSave;   
    }

    public List<GravityData> GetGravityData()
    {
        return gravityData;
    }

    public void DestroyPlanet(Transform _planet)
    {
        planetsList.Remove(planetsList.Find(x => x.transform == _planet));
        gravityData.Remove(gravityData.Find(x => x.planetTransform == _planet));       
    } 

    public List<APlanet> GetPlanetsList()
    {
        return planetsList;
    }

    public ARocket[] GetRocketsList()
    {
        return FindObjectsOfType<ARocket>();
    } 

    private void LoadPlanetsFromSave()
    {
        SaveInfo saveInfo = Utility.DeserializeData<SaveInfo>(Utility.SaveKey);
        int playerNum = saveInfo.playerNum;      
        for (int i = 0; i < saveInfo.Info.Length; i++)
        {
            GameObject _planetClone = Instantiate(planetePrefab);        
            AddPlanetComponets(i, playerNum, saveInfo.Info[i], _planetClone);
        }
        foreach (RocketSaveInfo item in saveInfo.Rockets)
        {
            GameObject rocketClone = Instantiate(GetRocketPrefabByType(item.RocketType), item.Position, Quaternion.Euler(item.Rotation));
            ARocket rocket = rocketClone.GetComponent<ARocket>();
            rocket.InitRocket(GetGravityData().ToArray(),sun.position ,false, item.Velocity);
        }
        AddSunToGravityList();
    }

    private void CreatesPlanets()
    {
        ClearSaves();
        int playerNum = Random.Range(0, PlanetsCount);
        float planetDistance = sunSize / 2f;
        for (int i = 0; i < PlanetsCount; i++)
        {
            GameObject _planetClone = Instantiate(planetePrefab);
            float randomSize = Random.Range(sunSize * planetsSizeMin, sunSize * planetsSizeMax);          
            planetDistance += randomSize + DistanceBetweenPlanets;
            PlanetData newData = new PlanetData()
            {
                CurrentPosition = Random.insideUnitCircle.normalized * planetDistance,
                Size = randomSize,
                SpeedMove = Random.Range(5, 25),
                SpeedRotation = Random.Range(35, 60),
                CurrentRotation = Random.Range(0, 360),
                HpMax = 100,
                HpCurrent = 100,
                FireRate = 3,
                RocketType = (eRocketType)Random.Range(0, System.Enum.GetValues(typeof(eRocketType)).Length),
                MyColor = Random.ColorHSV()
            };
            AddPlanetComponets(i, playerNum, newData, _planetClone);
        }
        AddSunToGravityList();      
    }

    private void AddPlanetComponets(int i, int playerNum, PlanetData planetData, GameObject _planetClone)
    {
        if (i == playerNum)
        {
            _planetClone.AddComponent<PlayerPlanet>().InitPlanet(planetData, GetRocketPrefabByType(planetData.RocketType), sun.position);
            Camera mainCamera = Camera.main;
            mainCamera.GetComponent<CameraMovement>().SetPlayer(_planetClone.transform);
        }
        else
        {
            _planetClone.AddComponent<AIPlanet>().InitPlanet(planetData, GetRocketPrefabByType(planetData.RocketType), sun.position);
        }
        planetsList.Add(_planetClone.GetComponent<APlanet>());
        gravityData.Add(new GravityData()
        {
            planetTransform = _planetClone.transform,
            Mass = _planetClone.transform.localScale.x * GravityForce
        });
    }

    private void AddSunToGravityList()
    {
        gravityData.Add(new GravityData()
        {
            planetTransform = sun.transform,
            Mass = sun.transform.localScale.x * GravityForce
        });
    }

    private GameObject GetRocketPrefabByType(eRocketType roketType)
    {
        switch (roketType)
        {
            case eRocketType.Big:
                return rocketPrefabs[0];
            case eRocketType.Fast:
                return rocketPrefabs[1];
            case eRocketType.Normal:
                return rocketPrefabs[2];
        }
        return null;      
    }

    private void ClearSaves()
    {
        planetsList = new List<APlanet>();
        gravityData = new List<GravityData>();
        Utility.DeleteSaveKey(Utility.SaveKey);      
    }
}

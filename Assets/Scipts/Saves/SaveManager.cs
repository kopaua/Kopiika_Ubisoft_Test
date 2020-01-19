using System.Collections.Generic;

public class SaveManager
{ 
    public static void SaveGame()
    {
        List<APlanet> planetsList = PlanetsManager.Singelton.GetPlanetsList();
        SaveInfo planetInfoSave = new SaveInfo()
        {
            Info = new PlanetData[planetsList.Count]
        };
        planetInfoSave.playerNum = -1; // -1 means player is death 
        for (int i = 0; i < planetsList.Count; i++)
        {
            if (planetsList[i].GetComponent<PlayerPlanet>() != null)
                planetInfoSave.playerNum = i;
            planetInfoSave.Info[i] = planetsList[i].GetUpdatedPlanetData();
        }

        ARocket[] rockets = PlanetsManager.Singelton.GetRocketsList();
        planetInfoSave.Rockets = new RocketSaveInfo[rockets.Length];
        for (int i = 0; i < rockets.Length; i++)
        {
            planetInfoSave.Rockets[i] = rockets[i].GetUpdatedInfoToSave();
        }
        Utility.SerializeData(planetInfoSave, Utility.SaveKey);        
    }
}

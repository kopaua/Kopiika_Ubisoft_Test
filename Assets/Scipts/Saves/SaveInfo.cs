using System;

[Serializable]
public struct SaveInfo
{
    public int playerNum;
    public PlanetData[] Info;
    public RocketSaveInfo[] Rockets;
}


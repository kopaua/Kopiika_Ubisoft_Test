
public class PlayerPlanet : APlanet
{
    private void OnEnable()
    {
        MyInputManager.OnClickFire += base.FireRocket;
    }

    private void OnDisable()
    {
        MyInputManager.OnClickFire -= base.FireRocket;
    }

    protected override void ReadyToFire()
    {       
    }   
}

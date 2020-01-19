using UnityEngine;
using UnityEngine.UI;

public class GameplayPanel : APanelViewer
{  
    [SerializeField]
    private Button buttonPause, buttonResume, buttonSave, buttonMainMenu;
    [SerializeField]
    private GameObject panelPause;

    private void Awake()
    {
        buttonPause.onClick.AddListener(OnPause);
        buttonResume.onClick.AddListener(OnResume);
        buttonSave.onClick.AddListener(OnSave);
        buttonMainMenu.onClick.AddListener(OnMenu);
    }

    public override void Enter()
    {       
        OnResume();
    }  

    private void OnPause( )
    {
        Time.timeScale = 0;
        buttonPause.gameObject.SetActive(false);
        buttonResume.gameObject.SetActive(true);
        panelPause.SetActive(true);
    }

    private void OnResume()
    {
        Time.timeScale = 1;
        buttonPause.gameObject.SetActive(true);
        buttonResume.gameObject.SetActive(false);
        panelPause.SetActive(false);
    }

    private void OnSave()
    {
        SaveManager.SaveGame();
        OnResume();
    }
    private void OnMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

}

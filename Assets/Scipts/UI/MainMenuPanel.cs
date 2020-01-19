using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuPanel : APanelViewer
{
    public static Action OnClickStart;
    public static Action OnClickLoad;

    [SerializeField]
    private Button buttonStart, buttonLoad;
   

    private void Awake()
    {
        buttonStart.onClick.AddListener(OnStart);     
        buttonLoad.onClick.AddListener(OnContinue);      
    }   

    public override void Enter()
    {
        gameObject.SetActive(true);
        buttonLoad.gameObject.SetActive(!Utility.IsSaveEmpty(Utility.SaveKey));     
    }  

    private void OnStart()
    {
        OnClickStart?.Invoke();
        canvasContainer.ActivatePanel(eStateUI.Gameplay);
    }

    private void OnContinue()
    {
        OnClickLoad?.Invoke();
        canvasContainer.ActivatePanel(eStateUI.Gameplay);
    }
}

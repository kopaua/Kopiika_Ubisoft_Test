using UnityEngine;
using System.Linq;

public class CanvasContainer : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenuPrefab;
    private APanelViewer[] panels;
    private APanelViewer currentPanel;

    private void Awake()
    {
        GameObject _cloneUI = Instantiate(mainMenuPrefab, transform);
        panels = _cloneUI.transform.GetComponentsInChildren<APanelViewer>();
        foreach (APanelViewer item in panels)
        {
            item.gameObject.SetActive(false);
        } 
        ActivatePanel(eStateUI.Menu);
    } 

    public void ActivatePanel(eStateUI _panelState)
    {
        if (currentPanel != null)
        {
            currentPanel.Exit();
        }
        currentPanel = panels.First(x => x.currentState == _panelState);
        currentPanel.gameObject.SetActive(true);
        currentPanel.Enter();
    }


}







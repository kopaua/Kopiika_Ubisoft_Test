using UnityEngine;
using System.Linq;
using System;

public class CanvasContainer : MonoBehaviour
{   
    private APanelViewer[] panels;
    private APanelViewer currentPanel;

    private void Awake()
    {
        int lenght = Enum.GetValues(typeof(eStateUI)).Length;
        panels = new APanelViewer[lenght];
        for (int i = 0; i < lenght; i++)
        {

            GameObject prefab = Resources.Load<GameObject>("PanelsUI/" + ((eStateUI)i).ToString());
            GameObject _cloneUI = Instantiate(prefab, transform);
            panels[i] = _cloneUI.transform.GetComponent<APanelViewer>();
            panels[i].gameObject.SetActive(false);
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







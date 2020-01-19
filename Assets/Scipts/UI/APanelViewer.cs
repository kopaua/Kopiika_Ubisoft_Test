using UnityEngine;

public enum eStateUI
{
    Menu,
    Gameplay
}

public abstract class APanelViewer : MonoBehaviour, IPanelUI
{
    public eStateUI currentState;
    protected CanvasContainer canvasContainer;

    void Start()
    {
        canvasContainer = transform.GetComponentInParent<CanvasContainer>();
    }

    public abstract void Enter();

    public void Exit()
    {
        gameObject.SetActive(false);
    }
}







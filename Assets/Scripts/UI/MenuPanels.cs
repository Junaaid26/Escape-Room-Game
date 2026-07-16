using UnityEngine;

public class MenuPanels : MonoBehaviour
{
    public GameObject controlsPanel;
    public GameObject aboutPanel;

    public void OpenControls()
    {
        controlsPanel.SetActive(true);
    }

    public void CloseControls()
    {
        controlsPanel.SetActive(false);
    }

    public void OpenAbout()
    {
        aboutPanel.SetActive(true);
    }

    public void CloseAbout()
    {
        aboutPanel.SetActive(false);
    }
}
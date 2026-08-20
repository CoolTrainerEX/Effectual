using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

[RequireComponent(typeof(PanelRenderer))]
[RequireComponent(typeof(UIInput))]
public class MenuManager : MonoBehaviour
{
    private UIInput input;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        input = GetComponent<UIInput>();

        GetComponent<PanelRenderer>().RegisterUIReloadCallback(OnUIReload);
    }

    private static void OnUIReload(PanelRenderer renderer, VisualElement rootElement)
    {
        rootElement.Q<Button>("Play").clicked += OnPlay;
        rootElement.Q<Button>("Exit").clicked += OnExit;
    }

    private static void OnPlay()
    {
        SceneManager.LoadScene("Game");
    }

    private static void OnExit()
    {
        Application.Quit();
    }
}

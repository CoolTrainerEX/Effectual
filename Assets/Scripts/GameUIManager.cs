using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

[RequireComponent(typeof(PanelRenderer))]
[RequireComponent(typeof(ControlsManager))]
[RequireComponent(typeof(PauseInput))]
public class GameUIManager : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;

    private ControlsManager controls;
    private PauseInput pauseInput;
    private TemplateContainer controlsElement;
    private TemplateContainer pauseElement;
    private bool paused = false;
    private int uiVersion = -1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        controls = GetComponent<ControlsManager>();
        pauseInput = GetComponent<PauseInput>();

        GetComponent<PanelRenderer>().RegisterUIReloadCallback(OnUIReload);

        controls.MouseLocked = true;
    }

    // Update is called once per frame
    private void Update()
    {
        if (pauseElement == null) return;

        if (pauseInput.IsPaused && !paused)
        {
            Time.timeScale = 0;
            pauseElement.style.display = DisplayStyle.Flex;
            controls.MouseLocked = false;

            mixer.SetFloat("MasterVolume", -80);
        }
        else if (!pauseInput.IsPaused && paused)
        {
            Time.timeScale = 1;
            pauseElement.style.display = DisplayStyle.None;
            controls.MouseLocked = true;

            mixer.SetFloat("MasterVolume", 0);
        }

        paused = pauseInput.IsPaused;

        if (controls.IsTouchscreen && !paused) controlsElement.style.display = DisplayStyle.Flex;
        else controlsElement.style.display = DisplayStyle.None;
    }

    private void OnUIReload(PanelRenderer panelRenderer, VisualElement rootElement, int version)
    {
        if (uiVersion == version) return;

        uiVersion = version;
        controlsElement = rootElement.Q<TemplateContainer>("Controls");
        pauseElement = rootElement.Q<TemplateContainer>("Pause");

        rootElement.Q<Button>("Back").clicked += OnPlay;
        rootElement.Q<Button>("Exit").clicked += OnExit;
    }

    private void OnPlay()
    {
        pauseInput.TogglePause();
    }

    private void OnExit()
    {
        Time.timeScale = 1;

        mixer.SetFloat("MasterVolume", 0);
        SceneManager.LoadScene("Menu");
    }
}

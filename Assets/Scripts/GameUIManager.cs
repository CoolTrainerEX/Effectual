using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

[RequireComponent(typeof(PanelRenderer))]
[RequireComponent(typeof(ControlsManager))]
[RequireComponent(typeof(PauseInput))]
[RequireComponent(typeof(UIInput))]
public class GameUIManager : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;

    private ControlsManager controls;
    private PauseInput pauseInput;
    private UIInput uiInput;
    private TemplateContainer controlsElement;
    private TemplateContainer pauseElement;
    private bool paused = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        controls = GetComponent<ControlsManager>();
        pauseInput = GetComponent<PauseInput>();
        uiInput = GetComponent<UIInput>();

        GetComponent<PanelRenderer>().RegisterUIReloadCallback(OnUIReload);
    }

    // Update is called once per frame
    private void Update()
    {
        if (pauseElement == null) return;

        if (pauseInput.IsPaused && !paused)
        {
            Time.timeScale = 0;
            pauseElement.style.display = DisplayStyle.Flex;

            mixer.SetFloat("MasterVolume", -80);
        }
        else if (!pauseInput.IsPaused && paused)
        {
            Time.timeScale = 1;
            pauseElement.style.display = DisplayStyle.None;

            mixer.SetFloat("MasterVolume", 0);
        }

        paused = pauseInput.IsPaused;

        if (controls.IsTouchscreen && !paused) controlsElement.style.display = DisplayStyle.Flex;
        else controlsElement.style.display = DisplayStyle.None;
    }

    private void OnUIReload(PanelRenderer renderer, VisualElement rootElement)
    {
        controlsElement = rootElement.Q<TemplateContainer>("Controls");
        pauseElement = rootElement.Q<TemplateContainer>("Pause");

        rootElement.Q<Button>("Back").clicked += OnPlay;
        rootElement.Q<Button>("Exit").clicked += OnExit;
    }

    private void OnPlay()
    {
        pauseInput.TogglePause();
    }

    private static void OnExit()
    {
        SceneManager.LoadScene("Menu");
    }
}

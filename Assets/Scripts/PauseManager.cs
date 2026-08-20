using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

[RequireComponent(typeof(PanelRenderer))]
[RequireComponent(typeof(PauseInput))]
[RequireComponent(typeof(UIInput))]
public class PauseManager : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;

    private PauseInput pauseInput;
    private UIInput uiInput;
    private VisualElement rootElement;
    private bool paused = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        pauseInput = GetComponent<PauseInput>();
        uiInput = GetComponent<UIInput>();

        GetComponent<PanelRenderer>().RegisterUIReloadCallback(OnUIReload);
    }

    // Update is called once per frame
    private void Update()
    {
        try
        {
            if (pauseInput.IsPaused && !paused)
            {
                Time.timeScale = 0;
                rootElement.style.display = DisplayStyle.Flex;

                mixer.SetFloat("MasterVolume", -80);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else if (!pauseInput.IsPaused && paused)
            {
                Time.timeScale = 1;
                rootElement.style.display = DisplayStyle.None;

                mixer.SetFloat("MasterVolume", 0);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }

            paused = pauseInput.IsPaused;
        }
        catch (NullReferenceException)
        {
            // Pass
        }
    }

    private void OnUIReload(PanelRenderer renderer, VisualElement rootElement)
    {
        this.rootElement = rootElement;

        rootElement.Q<Button>("Back").clicked += OnPlay;
        rootElement.Q<Button>("Exit").clicked += OnExit;
    }

    private void OnPlay()
    {
        pauseInput.TogglePause();
    }

    private void OnExit()
    {
        SceneManager.LoadScene("Menu");
    }
}

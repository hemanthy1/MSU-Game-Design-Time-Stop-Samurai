using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ReloadScene : MonoBehaviour
{
    private PlayerActions playControls;
    private InputAction reload;

    private void Awake()
    {
        playControls = new PlayerActions();
    }

    private void Update()
    {
        if (reload.ReadValue<float>() != 0)
        {
            Refresh();
        }
    }

    private void Refresh()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    private void OnEnable()
    {
        reload = playControls.PlayerControls.Reload;
        playControls.PlayerControls.Enable();
    }

    private void OnDisable()
    {
        playControls.PlayerControls.Disable();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    private bool clickOnce = false;
    [SerializeField] private GameObject pause;

    private void Update()
    {
        if(Input.GetButtonDown("Pause"))
        {
            if(!clickOnce)
            {
                pause.SetActive(true);
                Time.timeScale = 0f;
                clickOnce = true;
            }
            else
            {
                pause.SetActive(false);
                Time.timeScale = 1f;
                clickOnce = false;
            }
        }
    }
}

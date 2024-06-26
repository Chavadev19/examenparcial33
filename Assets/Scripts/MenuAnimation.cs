using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAnimation : MonoBehaviour
{
    [SerializeField] private GameObject title;
    [SerializeField] private GameObject gif;
    [SerializeField] private GameObject[] button;
    [SerializeField] private GameObject options;
    

    private void Start()
    {
        Time.timeScale = 1;
        //Accede a la clase, determina el objeto, la pos. y la duracion. Luego declara que animacion se usara y el delay de inicio
        LeanTween.moveX(gif, 4, 1).setEase(LeanTweenType.easeInExpo).setDelay(1);

        LeanTween.scale(title, new Vector3(1, 1, 1), 2).setLoopType(LeanTweenType.pingPong);

        LeanTween.scale(button[0], new Vector3(1, 1, 1), 2).setLoopType(LeanTweenType.clamp);

        LeanTween.moveX(button[1], -5, 1).setEase(LeanTweenType.easeInExpo).setDelay(.5f);

        LeanTween.moveY(button[2], -2, 1).setEase(LeanTweenType.easeInExpo).setDelay(.5f);

    }

    public void EnableOptions()
    {
        LeanTween.scale(options, new Vector3(1, 1, 1), 1).setEase(LeanTweenType.easeInElastic);
    }

    public void DisableOptions()
    {
        StartCoroutine(DisableOptionsPanel());
    }

    public IEnumerator DisableOptionsPanel()
    {
        LeanTween.scale(options, new Vector3(0, 0, 0), 1).setEase(LeanTweenType.easeInElastic);
        yield return new WaitForSeconds(1f);
        options.SetActive(false);
    }

    

}

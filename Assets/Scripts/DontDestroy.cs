using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    private static DontDestroy instance = null;

    void Awake()
    {
        // Verifica si ya existe una instancia del objeto
        if (instance != null && instance != this)
        {
            // Si existe otra instancia, destruye la nueva
            Destroy(gameObject);
        }
        else
        {
            // Si no existe otra instancia, asigna esta instancia y no la destruyas
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}

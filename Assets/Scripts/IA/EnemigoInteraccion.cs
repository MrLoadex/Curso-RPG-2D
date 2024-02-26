using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoInteraccion : MonoBehaviour
{
    [SerializeField] private GameObject seleccionFX;

    public void MostrarEnemigoSeleccionado(bool estado)
    {
        seleccionFX.SetActive(estado);
    }
}

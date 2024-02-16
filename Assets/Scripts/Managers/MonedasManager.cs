using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonedasManager : Singleton<MonedasManager>
{
    [SerializeField] int monedasTest;
    public int MonedasTotales { get; set; }

    private string KEY_MONEDAS = "MYJUEGO_MONEDAS";

    private void Start() 
    {
        MonedasTotales = monedasTest;//BORRAR LUEGO DE TESTEAR
        CargarMonedas();
    }

    private void CargarMonedas()
    {
        //MonedasTotales = PlayerPrefs.GetInt(KEY_MONEDAS);
    }

    public void AñadirMonedas(int cantidad)
    {
        MonedasTotales += cantidad;
        GuardarMonedas();
    }

    public void RemoverMonedas(int cantidad)
    {
        if(cantidad > MonedasTotales)
        {
            return;
        }

        MonedasTotales -= cantidad;
        GuardarMonedas();
    }

    private void GuardarMonedas()
    {
        PlayerPrefs.SetInt(KEY_MONEDAS, MonedasTotales);
        PlayerPrefs.Save();
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class PersonajeAtaque : MonoBehaviour
{
    [Header("Pooler")]
    [SerializeField] private ObjectPooler pooler;
    
    private PersonajeStats stats;

    public Arma ArmaEquipada{get; private set;}

    public EnemigoInteraccion EnemigoObjetivo { get; private set; }

    private void Awake() 
    {   
        stats = GetComponent<Personaje>().PersonajeStats;
    }


    public void EquiparArma(ItemArma armaPorEquipar)
    {
        ArmaEquipada = armaPorEquipar.Arma;
        if (ArmaEquipada.Tipo == TipoArma.Magia)
        {
            pooler.CrearPooler(ArmaEquipada.ProyectilPrefab.gameObject);
        }

        stats.AñadirBonusPorArma(ArmaEquipada);
    }

    public void RemoverArma()
    {
        if(ArmaEquipada == null)
        {
            return;
        }
        
        if(ArmaEquipada.Tipo == TipoArma.Magia)
        {
            pooler.DestruirPooler();
        }

        stats.RemoverBonusPorArma(ArmaEquipada);
        ArmaEquipada = null;
    }

    // Seleccionar a un enemigo
    private void EnemigoRangoSeleccionado(EnemigoInteraccion enemigoSeleccionado)
    {
        // Si no hay arma equipada, o el arma no es magia, o el enemigo seleccionado es el mismo que el que estaba antes entonces retornar.
        if (ArmaEquipada == null || ArmaEquipada.Tipo != TipoArma.Magia || EnemigoObjetivo == enemigoSeleccionado)
        {
            return;
        }

        EnemigoObjetivo = enemigoSeleccionado;
        EnemigoObjetivo.MostrarEnemigoSeleccionado(true);

    }

    //Deseleccionar a un enemigo
    private void EnemigoNoSeleccionado()
    {
        if (EnemigoObjetivo == null)
        {
            return;
        }

        EnemigoObjetivo.MostrarEnemigoSeleccionado(false);
        EnemigoObjetivo = null;

    }

    private void OnEnable() 
    {
        SeleccionManager.EventoEnemigoSeleccionado += EnemigoRangoSeleccionado;   
        SeleccionManager.EventoObjetoNoSeleccionado += EnemigoNoSeleccionado;   
    }

    private void OnDisable() 
    {
        SeleccionManager.EventoEnemigoSeleccionado -= EnemigoRangoSeleccionado;   
        SeleccionManager.EventoObjetoNoSeleccionado -= EnemigoNoSeleccionado;   
    }

}

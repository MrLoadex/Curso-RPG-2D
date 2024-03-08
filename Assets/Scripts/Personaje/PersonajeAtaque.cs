using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        EnemigoObjetivo.MostrarEnemigoSeleccionado(true, TipoDeteccion.Rango);

    }

    //Deseleccionar a un enemigo
    private void EnemigoNoSeleccionado()
    {
        if (EnemigoObjetivo == null)
        {
            return;
        }

        EnemigoObjetivo.MostrarEnemigoSeleccionado(false, TipoDeteccion.Rango);
        //EnemigoObjetivo.MostrarEnemigoSeleccionado(false, TipoDeteccion.Melee);
        EnemigoObjetivo = null;

    }

    private void EnemigoMeleeDetectado(EnemigoInteraccion enemigoDetectado)
    {
        if (ArmaEquipada == null) {return;}
        if (ArmaEquipada.Tipo != TipoArma.Melee) { return; }
        EnemigoObjetivo = enemigoDetectado;
        EnemigoObjetivo.MostrarEnemigoSeleccionado(true, TipoDeteccion.Melee);

    }  

    private void EnemigoMeleePerdido()
    {
        if (EnemigoObjetivo == null) { return; }
        if (ArmaEquipada == null) { return; } 
        if (ArmaEquipada.Tipo != TipoArma.Melee) {return; }
        
        EnemigoObjetivo.MostrarEnemigoSeleccionado(false, TipoDeteccion.Melee);
        EnemigoObjetivo = null;
    }

    private void OnEnable() 
    {
        SeleccionManager.EventoEnemigoSeleccionado += EnemigoRangoSeleccionado;   
        SeleccionManager.EventoObjetoNoSeleccionado += EnemigoNoSeleccionado;   
        PersonajeDetector.EventoEnemigoDetectado += EnemigoMeleeDetectado;
        PersonajeDetector.EventoEnemigoPerdido += EnemigoMeleePerdido;
    }

    private void OnDisable() 
    {
        SeleccionManager.EventoEnemigoSeleccionado -= EnemigoRangoSeleccionado;   
        SeleccionManager.EventoObjetoNoSeleccionado -= EnemigoNoSeleccionado;   
        PersonajeDetector.EventoEnemigoDetectado -= EnemigoMeleeDetectado;
        PersonajeDetector.EventoEnemigoPerdido -= EnemigoMeleePerdido;
    }

}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class PersonajeExperiencia : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] PersonajeStats stats;

    [Header("Config")]
    [SerializeField] private int nivelMax;
    [SerializeField] private int expBase;
    [SerializeField] private int valorIncremental;


    private float expActual;
    private float expActualTemp;
    private float expRequeridaSiguienteNivel;

    // Start is called before the first frame update
    void Start()
    {
        if (stats.Nivel!= 1)
        {
            expActual = stats.ExpActual;
            expRequeridaSiguienteNivel = stats.ExpRequeridaSiguienteNivel;
            expActualTemp = stats.ExpActualTemp;
            ActualizarBarraExp();
            return;
        }
        stats.Nivel = 1;
        expRequeridaSiguienteNivel = expBase;
        stats.ExpRequeridaSiguienteNivel = expRequeridaSiguienteNivel;
        ActualizarBarraExp();
    }

    private void Update() 
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            AñadirExperiencia(2f);
        }
    }

    public void AñadirExperiencia(float expObtenida)
    {
        // Comprobacion
        if (expObtenida <= 0)
        {
            return;
        }
        // Experiencia para el siguiente nivel
        float expRestanteNuevoNivel = expRequeridaSiguienteNivel - expActualTemp;
        
        if (expObtenida >= expRestanteNuevoNivel)
        {
            expObtenida -= expRequeridaSiguienteNivel;
            ActualizarNivel();
            AñadirExperiencia(expObtenida);
        }
        else
        {
            expActualTemp += expObtenida;
            stats.ExpActualTemp = expActualTemp;
            expActual += expObtenida;
            if (Mathf.Approximately(expActualTemp, expRequeridaSiguienteNivel))
            {
                ActualizarNivel();
            }
        }
        stats.ExpActual = expActual;
        ActualizarBarraExp();
    }
    
    private void ActualizarNivel()
    {
        if (stats.Nivel >= nivelMax)
        {
            return;
        }

        stats.Nivel++;
        expActualTemp = 0f;
        stats.ExpActualTemp = expActualTemp;
        expRequeridaSiguienteNivel *= valorIncremental;
        stats.ExpRequeridaSiguienteNivel = expRequeridaSiguienteNivel;
        stats.PuntosDisponibles += 3;
    }

    private void ActualizarBarraExp()
    {
        UIManager.Instance.ActualizarExpPersonaje(expActualTemp,expRequeridaSiguienteNivel);
    }

    private void RespuestaEnemigoDerrotado(float exp)
    {
        AñadirExperiencia(exp);
    }
    
    private void OnEnable() 
    {
        EnemigoVida.EventoEnemigoDerrotado += RespuestaEnemigoDerrotado;
    }

    private void OnDisable() 
    {
        EnemigoVida.EventoEnemigoDerrotado -= RespuestaEnemigoDerrotado;
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

[CreateAssetMenu(menuName = "Stats")]

public class PersonajeStats : ScriptableObject
{

    [Header("Stats")]
    public float Daño = 5f;
    public float Defensa = 2f;
    public float Velocidad = 5f;
    public int Nivel;
    public float ExpActual;
    public float ExpActualTemp;
    public float ExpRequeridaSiguienteNivel;
    [Range(0f,100)] public float PorcentajeCritico;
    [Range(0f,100)] public float PorcentajeBloqueo;

    [Header("Atributos")]
    public int Fuerza;
    public int Inteligencia;
    public int Destreza;

    [HideInInspector] public int PuntosDisponibles;

    public void AñadirBonusPorAtributoFuerza()
    {
        Daño += 2f;
        Defensa += 1f;
        PorcentajeBloqueo += 0.03f;
    }

    public void AñadirBonusPorAtributoInteligencia()
    {
        Daño += 3f;
        PorcentajeCritico += 0.2f;
    }

    public void AñadirBonusPorAtributoDestreza()
    {
        Velocidad += 0.1f;
        PorcentajeBloqueo += 0.05f;
    }

    public void AñadirBonusPorArma(Arma arma)
    {
        Daño += arma.Daño;
        PorcentajeCritico += arma.ChanceCritico;
        PorcentajeBloqueo += arma.ChanceDeBloqueo;
    }

    public void RemoverBonusPorArma(Arma arma)
    {
        Daño -= arma.Daño;
        PorcentajeCritico -= arma.ChanceCritico;
        PorcentajeBloqueo -= arma.ChanceDeBloqueo;
    }

    public void ResetearValores()
    {
        Daño = 5f;
        Defensa = 2f;
        Velocidad = 5f;
        Nivel = 1;
        ExpActual = 0f;
        ExpActualTemp = 0f;
        ExpRequeridaSiguienteNivel = 0f;
        PorcentajeBloqueo = 0f;
        PorcentajeCritico = 0f;

        Fuerza = 0;
        Inteligencia = 0;
        Destreza = 0;
        PuntosDisponibles = 0;
    }
}

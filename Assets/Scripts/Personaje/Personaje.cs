using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personaje : MonoBehaviour
{
    [SerializeField] private PersonajeStats stats;

    public PersonajeVida PersonajeVida {get; private set; }
    public PersonajeMana PersonajeMana { get; private set; }
    public PersonajeExperiencia PersonajeExperiencia {get; private set;}
    public PersonajeMovimiento PersonajeMovimiento {get; private set;}
    public PersonajeAnimaciones PersonajeAnimaciones { get; private set; }

    private void Awake() 
    {
        PersonajeVida = GetComponent<PersonajeVida>();   
        PersonajeMana = GetComponent<PersonajeMana>();   
        PersonajeExperiencia = GetComponent<PersonajeExperiencia>();
        PersonajeMovimiento = GetComponent<PersonajeMovimiento>();
        PersonajeAnimaciones = GetComponent<PersonajeAnimaciones>(); 
    }

    public void RestaurarPersonaje()
    {
        PersonajeVida.RestaurarPersoanje();
        PersonajeAnimaciones.RevivirPersonaje();
        PersonajeMana.RestablecerMana();
        PersonajeMovimiento.enabled = true;
    }

    private void AtributoRespuesta(TipoAtributo tipo)
    {

        if(stats.PuntosDisponibles <= 0)
        {
            return;
        }

        switch (tipo)
        {
            
            case TipoAtributo.Fuerza:
                stats.Fuerza ++; 
                stats.AñadirBonusPorAtributoFuerza();
                break;
            case TipoAtributo.Inteligencia:
                stats.Inteligencia ++;
                stats.AñadirBonusPorAtributoInteligencia();
                break;
            case TipoAtributo.Destreza:
                stats.Destreza ++;
                stats.AñadirBonusPorAtributoDestreza();
                break;
        }
        stats.PuntosDisponibles --;
    }

    private void OnEnable() 
    {
        AtributoButton.EventoAgregarAtributo += AtributoRespuesta;
    }

    private void OnDisable() 
    {
        AtributoButton.EventoAgregarAtributo -= AtributoRespuesta;
    }
}

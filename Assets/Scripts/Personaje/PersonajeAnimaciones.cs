using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonajeAnimaciones : MonoBehaviour
{
    [SerializeField] private string layerIdle;
    [SerializeField] private string layerCaminar;
    [SerializeField] private string layerAtacar;

    public Animator _animator;
    private PersonajeMovimiento _personajeMovimiento;
    private PersonajeAtaque _personajeAtaque;

    private readonly int direccionX = Animator.StringToHash("X");
    private readonly int direccionY = Animator.StringToHash("Y");
    private readonly int derrotado = Animator.StringToHash("Derrotado");

    private void Awake() 
    {
        _animator = GetComponent<Animator>();
        _personajeMovimiento = GetComponent<PersonajeMovimiento>();
        _personajeAtaque = GetComponent<PersonajeAtaque>();
    } 

    // Update is called once per frame
    void Update()
    {
        ActualizarLayers();
        
        //Si no esta en movimiento deja de moverse no se actualiza la animacion
        if(_personajeMovimiento.EnMovimiento == false)
        {
            return;
        }

        _animator.SetFloat(direccionX, _personajeMovimiento.DireccionMovimiento.x);
        _animator.SetFloat(direccionY, _personajeMovimiento.DireccionMovimiento.y);
    }

    private void ActivarLayer(string nombreLayer)
    {
        // Desactivar todos los Layers
        for (int i = 0; i < _animator.layerCount; i++)
        {
            _animator.SetLayerWeight(i,0);
        }

        // Activar el layer
        _animator.SetLayerWeight(_animator.GetLayerIndex(nombreLayer),1);
    }

    private void ActualizarLayers()
    {   
        if(_personajeAtaque.Atacando)
        {
            ActivarLayer(layerAtacar);
        }
        else if (_personajeMovimiento.EnMovimiento)
        {
            ActivarLayer(layerCaminar);
        }
        else
        {
            ActivarLayer(layerIdle);
        }

    }

    private void PersonajeDerrotadoRespuesta()
    {
        if(_animator.GetLayerWeight(_animator.GetLayerIndex(layerIdle)) == 1)
        {
            _animator.SetBool(derrotado, true);
        }
    }

    public void RevivirPersonaje()
    {
        ActivarLayer(layerIdle);
        _animator.SetBool(derrotado, false);
    }

    private void OnEnable() 
    {
        //Suscribirse al evento
        PersonajeVida.EventoPersonajeDerrotado += PersonajeDerrotadoRespuesta;
    }

    private void OnDisable() 
    {
        PersonajeVida.EventoPersonajeDerrotado -= PersonajeDerrotadoRespuesta;
    }
}

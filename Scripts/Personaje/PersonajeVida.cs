using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;


public class PersonajeVida : VidaBase
{
    public static Action EventoPersonajeDerrotado;//Action = Evento
    
    public bool Derrotado { get; private set; }
    public bool PuedeSerCurado => Salud < saludMax && !Derrotado;
    
    private BoxCollider2D _boxCillider2D;

    private void Awake() 
    {
        _boxCillider2D = GetComponent<BoxCollider2D>();
    }

    protected override void Start() 
    {
        base.Start();
        ActualizarBarraVida(Salud, saludMax);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            RecibirDaño(10);
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            RestaurarSalud(10);
        }
    }

    public void RestaurarSalud(float cantidad)
    {
        if(Derrotado)
        {
            return;
        }

        if (PuedeSerCurado)
        {
            Salud += cantidad;
            if (Salud > saludMax)
            {
                Salud = saludMax;
            }

            ActualizarBarraVida(Salud, saludMax);
        }
    }

    protected override void PersonajeDerrotado()
    {
        _boxCillider2D.enabled = false;
        Derrotado = true;
        //Si el EventoPerosnajeDerrotado != nulo lo invoca
        EventoPersonajeDerrotado?.Invoke();

    }

    public void RestaurarPersoanje()
    {
        _boxCillider2D.enabled = true;
        Derrotado = false;
        Salud = saludInicial;
        ActualizarBarraVida(Salud, saludInicial);
    }

    protected override void ActualizarBarraVida(float vidaActual, float vidaMax)
    {
        UIManager.Instance.ActualizarVidaPersonaje(vidaActual,vidaMax);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "IA/Acciones/Atacar Personaje")]
public class AccionAtacarPersonaje : IAAccion
{
    public override void Ejecutar(IAController controller)
    {
        Atacar(controller);
    }

    private void Atacar(IAController controller)
    {
        if (controller.PersonajeReferencia == null)
        {
            return;
        }

        if (!controller.EsTiempoDeAtacar())
        {
            return;
        }

        if (controller.PersonajeEnRangoDeAtaque(controller.RangoDeAtaque))
        {
            //Atacar al enemigo (Player)
            controller.AtaqueMele(controller.Daño);

            //Actualizar el tiempo entre ataques
            controller.ActualizarTiempoEntreAtaques();
        }


    }
}

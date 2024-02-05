using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="IA/Decisiones/Personaje En Rango De Ataque")]
public class DesicionPersonajeRangoAtaque : IADecision
{
    public override bool Decidir(IAController controller)
    {
        return EnRangoDeAtaque(controller);
    }

    private bool EnRangoDeAtaque(IAController controller)
    {
        // Control
        if (controller.PersonajeReferencia ==  null)
        {
            return false;
        }

        // Obtener distancia de la IA  y el enemigo (Player)
        float distancia = (controller.PersonajeReferencia.position - 
                           controller.transform.position).sqrMagnitude;

        // Verificar si esta en el rango de ataque
        if (distancia < Mathf.Pow(controller.RangoDeAtaqueDeterminado, 2))
        {
            return true;
        }
        return false;
    }
}

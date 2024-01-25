using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovimiento : WayPointMovimiento
{
    private readonly int caminarAbajo = Animator.StringToHash("Caminar_Abajo");

    protected override void RotarHorizontal()
    {
        if (direccion != DirecionMovimiento.Horizontal)
        {
            return;
        }
        
        if (PuntoPorMoverse.x > ultimaPosicion.x)
        {
            transform.localScale = new Vector3(1,1,1);
        }
        else
        {
            transform.localScale = new Vector3(-1,1,1);
        }
    }

    protected override void RotarVertical()
    {
        if (direccion != DirecionMovimiento.Vertical)
        {
            return;
        }
        
        // Si se esta moviendo hacia arriba
        if (PuntoPorMoverse.y > ultimaPosicion.y)
        {
            _animator.SetBool(caminarAbajo, false);
        }
        else
        {
            _animator.SetBool(caminarAbajo, true);
        }
    }
}

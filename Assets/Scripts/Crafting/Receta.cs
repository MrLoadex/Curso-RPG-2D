using System;
using UnityEngine;


[Serializable]
public class Receta
{
    public string nombre;
    [Header("1er Material")]
    public InventarioItem Item1;
    public int item1CantidadRequerida;

    [Header("2do Material")]
    public InventarioItem Item2;
    public int item2CantidadRequerida;

    [Header("Resultado")]
    public InventarioItem ItemResultado;
    public int itemResultadoCantidad;

}

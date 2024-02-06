
using UnityEngine;

[CreateAssetMenu(menuName ="Item/Pocion Vida")]
public class ItemPocionVida : InventarioItem
{
    [Header("PocionInfo")]
    public float HPRestauracion;

    public override bool UsarItem()
    {
        if(Inventario.Instance.Personaje.PersonajeVida.PuedeSerCurado)
        {
            Inventario.Instance.Personaje.PersonajeVida.RestaurarSalud(HPRestauracion);
            return true;
        }
        return false;
    }
}

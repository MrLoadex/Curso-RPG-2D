
using UnityEngine;

[CreateAssetMenu(menuName ="Item/Pocion Mana")]

public class ItemPocionMana : InventarioItem
{
    [Header("PocionInfo")]
    public float MPRestauracion;

    public override bool UsarItem()
    {
        if(Inventario.Instance.Personaje.PersonajeMana.SePuedeRestaurar)
        {
            Inventario.Instance.Personaje.PersonajeMana.RestaurarMana(MPRestauracion);
            return true;
        }
        return false;
    }
}

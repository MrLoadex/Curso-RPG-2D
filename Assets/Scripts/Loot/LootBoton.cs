using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LootBoton : MonoBehaviour
{
    [SerializeField] private Image itemIcono;
    [SerializeField] private TextMeshProUGUI itemNombre;
    
    public DropItem ItemPorRecoger { get; set; }

    public void ConfigurarLootItem(DropItem dropItem)
    {
        ItemPorRecoger = dropItem;
        itemIcono.sprite = dropItem.Item.Icono;
        itemNombre.text = $"{dropItem.Item.Nombre} x {dropItem.Cantidad}";
    }
}

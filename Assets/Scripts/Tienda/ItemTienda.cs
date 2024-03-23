using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemTienda : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private Image itemIcono;
    [SerializeField] private TextMeshProUGUI itemNombreTMP;
    [SerializeField] private TextMeshProUGUI itemCostoTMP;
    [SerializeField] private TextMeshProUGUI cantidadPorComprarTMP;

    public ItemVenta ItemCargado { get; set; }

    public void ConfigurarItemVenta(ItemVenta itemVenta)
    {
        ItemCargado = itemVenta;
        itemIcono.sprite = itemVenta.Item.Icono;
        itemNombreTMP.text= itemVenta.Item.Nombre;
        itemCostoTMP.text= itemVenta.CostoBase.ToString();
    }
}

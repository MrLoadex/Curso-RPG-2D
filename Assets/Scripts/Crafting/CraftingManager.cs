

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftingManager : Singleton<CraftingManager>
{
    [Header("Config")]
    [SerializeField] private RecetaTarjeta recetaTarjetaPrefab;
    [SerializeField] private Transform recetaContenedor;

    [Header("Receta Info")]
    [SerializeField] private Image primerMaterialIcono;
    [SerializeField] private Image segundoMaterialIcono;
    [SerializeField] private TextMeshProUGUI primerMaterialNombreTMP;
    [SerializeField] private TextMeshProUGUI segundoMaterialNombreTMP;
    [SerializeField] private TextMeshProUGUI primerMaterialCantidadTMP;
    [SerializeField] private TextMeshProUGUI segundoMaterialCantidadTMP;
    [SerializeField] private TextMeshProUGUI recetaMensajeTMP;
    [SerializeField] private  Button buttonCraftear;

    [Header("Recetas")]
    [SerializeField] private RecetaLista recetas;

    public Receta RecetaSeleccionada { get; private set; }

    private void Start()
    {
        CargarRecetas();
    }

    private void CargarRecetas()
    {
        for (int i = 0; i < recetas.Recetas.Length; i++)
        {
            RecetaTarjeta receta = Instantiate(recetaTarjetaPrefab, recetaContenedor);
            receta.ConfigurarRecetaTarjeta(recetas.Recetas[i]);
        }
    }

    public void MostrarReceta(Receta receta)
    {
        // Cargar la receta
        RecetaSeleccionada = receta;
        // Cargar los iconos
        primerMaterialIcono.sprite = receta.Item1.Icono;
        segundoMaterialIcono.sprite = receta.Item2.Icono;
        // Cargar los nombres
        primerMaterialNombreTMP.text = receta.Item1.Nombre;
        segundoMaterialNombreTMP.text = receta.Item2.Nombre;

        primerMaterialCantidadTMP.text = 
        $"{Inventario.Instance.ObtenerCantidadDeItems(receta.Item1.ID)}/{receta.item1CantidadRequerida}";

        segundoMaterialCantidadTMP.text = 
        $"{Inventario.Instance.ObtenerCantidadDeItems(receta.Item2.ID)}/{receta.item2CantidadRequerida}";
    }
}

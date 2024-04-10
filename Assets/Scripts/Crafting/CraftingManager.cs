

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

    [Header("Item Resultado")]

    [SerializeField] private Image itemResultadoIcono;
    [SerializeField] private TextMeshProUGUI itemResultadoNombreTMP;
    [SerializeField] private TextMeshProUGUI itemResultadoDescripcionTMP;


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
    
        if (SePuedeCraftear(receta))
        {
            recetaMensajeTMP.text = "Receta Disponible";
            buttonCraftear.interactable = true;
        }
        else
        {
            recetaMensajeTMP.text = "Necesitas mas Materiales";
            buttonCraftear.interactable = false;
        }

        itemResultadoIcono.sprite = receta.ItemResultado.Icono;
        itemResultadoNombreTMP.text = receta.ItemResultado.Nombre;

        itemResultadoDescripcionTMP.text = receta.ItemResultado.DescripcionItemCrafting();
    }

    public bool SePuedeCraftear(Receta receta)
    {
        // Guardar cantidades actuales de los items requeridos.
        int cantidadItem1_EnInv = Inventario.Instance.ObtenerCantidadDeItems(receta.Item1.ID);
        int cantidadItem2_EnInv = Inventario.Instance.ObtenerCantidadDeItems(receta.Item2.ID);
        
        // Comprobar si son suficientes para craftear el item
        if (cantidadItem1_EnInv >= receta.item1CantidadRequerida 
        && cantidadItem2_EnInv >= receta.item2CantidadRequerida)
        {
            return true;
        }
    
        return false;
    }

    public void Craftear()
    {
        // Consumir item 1
        for (int i = 0; i < RecetaSeleccionada.item1CantidadRequerida; i++)
        {
            Inventario.Instance.ConsumirItem(RecetaSeleccionada.Item1.ID);
        }

        // Consumir item 2
        for (int i = 0; i < RecetaSeleccionada.item2CantidadRequerida; i++)
        {
            Inventario.Instance.ConsumirItem(RecetaSeleccionada.Item2.ID);
        }

        // Añadir Item resultado al Inventario
        Inventario.Instance.AñadirItem(RecetaSeleccionada.ItemResultado, RecetaSeleccionada.itemResultadoCantidad);
        MostrarReceta(RecetaSeleccionada);
    }   
}

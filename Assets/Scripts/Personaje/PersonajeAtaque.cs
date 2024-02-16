using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class PersonajeAtaque : MonoBehaviour
{
    [Header("Pooler")]
    [SerializeField] private ObjectPooler pooler;
    
    private PersonajeStats stats;

    public Arma ArmaEquipada{get; private set;}

    private void Awake() 
    {   
        stats = GetComponent<Personaje>().PersonajeStats;
    }


    public void EquiparArma(ItemArma armaPorEquipar)
    {
        ArmaEquipada = armaPorEquipar.Arma;
        if (ArmaEquipada.Tipo == TipoArma.Magia)
        {
            pooler.CrearPooler(ArmaEquipada.ProyectilPrefab.gameObject);
        }

        stats.AñadirBonusPorArma(ArmaEquipada);
    }

    public void RemoverArma()
    {
        if(ArmaEquipada == null)
        {
            return;
        }
        
        if(ArmaEquipada.Tipo == TipoArma.Magia)
        {
            pooler.DestruirPooler();
        }

        stats.RemoverBonusPorArma(ArmaEquipada);
        ArmaEquipada = null;
    }


}

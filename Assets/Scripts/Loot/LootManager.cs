using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootManager : Singleton<LootManager>
{
    [Header("Configuracion")]
    [SerializeField] private GameObject panelLoot;

    public void MostrarLoot()
    {
        panelLoot.SetActive(true);
    }


}

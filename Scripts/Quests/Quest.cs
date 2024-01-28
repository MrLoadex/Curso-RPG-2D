using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Quest : ScriptableObject
{
    [Header("Info")]
    public string Nombre;
    public string ID;
    public int CantidadObjetivo;
    public string NPCNombre;

    [Header("Descripcion")]
    [TextArea] public string Descripcion;
    
    [Header("Recompensas")]
    public int RecompensaOro;
    public float RecompensaExp;
    public QuestRecompensaItem RecompensaItem;

    [HideInInspector] public int CantidadActual;
    [HideInInspector] public bool QuestCompletado;

}

[Serializable]
public class QuestRecompensaItem
{
    public InventarioItem item;
    public int Cantidad;
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Quest : ScriptableObject
{

    public static Action<Quest> EventoQuestCompletado;

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

    public void AñadirProgreso(int cantidad)
    {
        CantidadActual += cantidad;
        VerificarQuestCompletado();
    }

    private void VerificarQuestCompletado()
    {
        if (CantidadActual >= CantidadObjetivo)
        {
            CantidadActual = CantidadObjetivo;
            CompletarQuest();
        }
    }

    private void CompletarQuest()
    {
        if (QuestCompletado)
        {
            return;
        }
        QuestCompletado = true;
        EventoQuestCompletado?.Invoke(this);
    }

    private void OnEnable() 
    {
        QuestCompletado = false;
        CantidadActual = 0;
    }

}

[Serializable]
public class QuestRecompensaItem
{
    public InventarioItem item;
    public int Cantidad;
}
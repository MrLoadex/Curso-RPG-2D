using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeleccionManager : MonoBehaviour
{
    public static Action<EnemigoInteraccion> EventoEnemigoSeleccionado;
    public static Action EventoObjetoNoSeleccionado;

    public EnemigoInteraccion EnemigoSeleccionado { get; set; }

    private Camera camara;
    // Start is called before the first frame update
    void Start()
    {
        camara = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        SeleccionarEnemigo();
    }

    private void SeleccionarEnemigo()
    {

        if (!Input.GetMouseButtonDown(0))
        {
            return;
        }

        // Realiza un rayo desde la posición del ratón en la pantalla hacia el mundo en 2D.
        RaycastHit2D hit = Physics2D.Raycast(
            camara.ScreenToWorldPoint(Input.mousePosition),  // Parámetro 1: Origen del rayo en coordenadas de vista.
            Vector2.zero,                                       // Parámetro 2: Dirección del rayo, en este caso, desde el origen hacia adelante.
            Mathf.Infinity,                                     // Parámetro 3: Longitud máxima del rayo, en este caso, infinita.
            LayerMask.GetMask("Enemigo")                        // Parámetro 4: Máscara de capas, solo interactúa con objetos en la capa "Enemigo".
        );

        if (hit.collider != null)
        {
            // Seleccionar al enemigo
            EnemigoSeleccionado = hit.collider.GetComponent<EnemigoInteraccion>();
            
            //
            EnemigoVida enemigoVida = EnemigoSeleccionado.GetComponent<EnemigoVida>();

            if (enemigoVida.Salud > 0)
            {
                EventoEnemigoSeleccionado?.Invoke(EnemigoSeleccionado);
            }
            else
            {
                EnemigoLoot loot = EnemigoSeleccionado.GetComponent<EnemigoLoot>();
                LootManager.Instance.MostrarLoot(loot);
            }

            // Invocar evento
            EventoEnemigoSeleccionado?.Invoke(EnemigoSeleccionado);
        }
        else 
        {
            EventoObjetoNoSeleccionado?.Invoke();
        }
    }
}

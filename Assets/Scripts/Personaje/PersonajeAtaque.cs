using System;
using System.Collections;
using UnityEngine;

public class PersonajeAtaque : MonoBehaviour
{
    public static Action<float> EventoEnemigoDañado;

    [Header("Pooler")]
    [SerializeField] private ObjectPooler pooler;
    
    private PersonajeStats stats;

    [Header("Ataque")]
    [SerializeField] private float tiempoEntreAtaques;
    [SerializeField]
    [Tooltip("0 = Arriba \n 1 = Derecha \n 2 = Abajo \n 3 = Izquierda")]
    Transform[] posicionesDisparo;

    public Arma ArmaEquipada{get; private set;}

    public EnemigoInteraccion EnemigoObjetivo { get; private set; }

    public bool Atacando { get; set; }

    private PersonajeMana _personajeMana;

    // Guarda la ultima posicion de a la que miro el player para disparar en esta misma  
    private int indexDireccionDisparo;
    private float tiempoParaSiguienteAtaque;

    private void Awake() 
    {   
        stats = GetComponent<Personaje>().PersonajeStats;
        _personajeMana = GetComponent<PersonajeMana>();
    }

    private void Update()
    {
        ObtenerDireccionDisparo();

        if(Time.time > tiempoParaSiguienteAtaque)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                if (ArmaEquipada == null || EnemigoObjetivo == null)
                { 
                    return; 
                }
                
                UsarArma();
                tiempoParaSiguienteAtaque = Time.time + tiempoEntreAtaques;
            }
        }
    }

    private void UsarArma()
    {
        // Comprobar tipo
        if (ArmaEquipada.Tipo == TipoArma.Magia)
        {
            // Comprobar mana
            if (_personajeMana.ManaActual < ArmaEquipada.ManaRequerida)
            {
                return;
            }

            // Obtener instancia del Proyectil del Pooler 
            GameObject nuevoProyectil = pooler.ObtenerInstancia();
            nuevoProyectil.transform.localPosition = posicionesDisparo[indexDireccionDisparo].position; 
            
            // Configurar proyectil
            Proyectil proyectil = nuevoProyectil.GetComponent<Proyectil>();
            proyectil.InicializarProyectil(this);
            // Activar proyectil
            nuevoProyectil.SetActive(true);

            // Consumir Mana necesario
            _personajeMana.UsarMana(ArmaEquipada.ManaRequerida);
            StartCoroutine(IEEstablecerCondicionAtaque());
        }
        else if (ArmaEquipada.Tipo == TipoArma.Melee)
        {
            float daño = ObtenerDaño();
            EnemigoVida enemigoVida = EnemigoObjetivo.GetComponent<EnemigoVida>();
            enemigoVida.RecibirDaño(daño);
            EventoEnemigoDañado?.Invoke(daño);
        }
    }

    public float ObtenerDaño()
    {
        float cantidad = stats.Daño;
        if(UnityEngine.Random.value < stats.PorcentajeCritico / 100)
        {
            cantidad *= 2;
        }

        return cantidad;
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

    #region Disparar

    private void ObtenerDireccionDisparo()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")); 
        //Derecha
        if(input.x > 0.1f)
        {
            indexDireccionDisparo = 1;
        }
        //Izquierda
        else if (input.x < 0)
        {
            indexDireccionDisparo = 3;
        }
        //Arriba
        else if (input.y > 0.1f)
        {
            indexDireccionDisparo = 0;
        }
        //Abajo
        else if (input.y < 0)
        {
            indexDireccionDisparo = 2;
        }
    }

    private IEnumerator IEEstablecerCondicionAtaque()
    {
        Atacando = true;
        yield return new WaitForSeconds(0.3f);
        Atacando = false;
    }

    #endregion

    #region Seleccion De Enemigo       
    // Seleccionar a un enemigo
    private void EnemigoRangoSeleccionado(EnemigoInteraccion enemigoSeleccionado)
    {
        // Si no hay arma equipada, o el arma no es magia, o el enemigo seleccionado es el mismo que el que estaba antes entonces retornar.
        if (ArmaEquipada == null || ArmaEquipada.Tipo != TipoArma.Magia || EnemigoObjetivo == enemigoSeleccionado)
        {
            return;
        }

        EnemigoObjetivo = enemigoSeleccionado;
        EnemigoObjetivo.MostrarEnemigoSeleccionado(true, TipoDeteccion.Rango);

    }
    //Deseleccionar a un enemigo
    private void EnemigoNoSeleccionado()
    {
        if (EnemigoObjetivo == null)
        {
            return;
        }

        EnemigoObjetivo.MostrarEnemigoSeleccionado(false, TipoDeteccion.Rango);
        //EnemigoObjetivo.MostrarEnemigoSeleccionado(false, TipoDeteccion.Melee);
        EnemigoObjetivo = null;

    }

    private void EnemigoMeleeDetectado(EnemigoInteraccion enemigoDetectado)
    {
        if (ArmaEquipada == null) {return;}
        if (ArmaEquipada.Tipo != TipoArma.Melee) { return; }
        EnemigoObjetivo = enemigoDetectado;
        EnemigoObjetivo.MostrarEnemigoSeleccionado(true, TipoDeteccion.Melee);

    }  

    private void EnemigoMeleePerdido()
    {
        if (EnemigoObjetivo == null) { return; }
        if (ArmaEquipada == null) { return; } 
        if (ArmaEquipada.Tipo != TipoArma.Melee) {return; }
        
        EnemigoObjetivo.MostrarEnemigoSeleccionado(false, TipoDeteccion.Melee);
        EnemigoObjetivo = null;
    }

    #endregion

    private void OnEnable() 
    {
        SeleccionManager.EventoEnemigoSeleccionado += EnemigoRangoSeleccionado;   
        SeleccionManager.EventoObjetoNoSeleccionado += EnemigoNoSeleccionado;   
        PersonajeDetector.EventoEnemigoDetectado += EnemigoMeleeDetectado;
        PersonajeDetector.EventoEnemigoPerdido += EnemigoMeleePerdido;
    }

    private void OnDisable() 
    {
        SeleccionManager.EventoEnemigoSeleccionado -= EnemigoRangoSeleccionado;   
        SeleccionManager.EventoObjetoNoSeleccionado -= EnemigoNoSeleccionado;   
        PersonajeDetector.EventoEnemigoDetectado -= EnemigoMeleeDetectado;
        PersonajeDetector.EventoEnemigoPerdido -= EnemigoMeleePerdido;
    }

    private IEnumerator IEEstablecerCondicionAtaque()
    {
        Atacando = true;
        yield return new WaitForSeconds(0.3f);
        Atacando = false;
    }

}

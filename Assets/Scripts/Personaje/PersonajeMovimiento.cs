using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonajeMovimiento : MonoBehaviour
{
    [SerializeField] float velocidad;

    public bool EnMovimiento => _direccionMovimiento.magnitude > 0f; //magnitude retorna valor si se esta moviendo
    public Vector2 DireccionMovimiento => _direccionMovimiento;

    private Rigidbody2D _rigidbody2D;
    private Vector2 _direccionMovimiento;
    private Vector2 _input;

    private void Awake() 
    {   
        _rigidbody2D = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
       _input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")); 

        // X
        if (_input.x > 0.1f)
        {
            _direccionMovimiento.x = 1f;
        }
        else if (_input.x < 0f)
        {
            _direccionMovimiento.x = -1f;
        }
        else
        {
            _direccionMovimiento.x = 0f;
        }

        // Y
        if (_input.y > 0.1f)
        {
            _direccionMovimiento.y = 1f;
        }
        else if (_input.y < 0f)
        {
            _direccionMovimiento.y = -1f;
        }
        else
        {
            _direccionMovimiento.y = 0f;
        }

    }

    private void FixedUpdate() 
    {
        _rigidbody2D.MovePosition(_rigidbody2D.position + _direccionMovimiento * velocidad * Time.fixedDeltaTime);
    }

    private void ResponderEventoPersonajeDerrotado()
    {
        enabled = false;
    }

    private void OnEnable() 
    {
        PersonajeVida.EventoPersonajeDerrotado += ResponderEventoPersonajeDerrotado;
    }

    private void OnDisable() 
    {
        PersonajeVida.EventoPersonajeDerrotado += ResponderEventoPersonajeDerrotado;
        
    }
}

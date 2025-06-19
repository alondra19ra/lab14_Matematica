using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;
using static UnityEditor.Searcher.SearcherWindow.Alignment;
using UnityEditor.Experimental.GraphView;

public class PlayerController : MonoBehaviour
{
    // Movimiento y f�sicas
    Rigidbody2D prota;
    float horizontal;
    public float velocidad;
    public float fuerzaSalto;

    // Saltos
    public bool saltar;
    public bool unSalto;
    public bool dosSaltos;

    // Raycast
    RaycastHit2D rayito;
    public LayerMask layer;



   

    #region Unity Functions

    private void Awake()
    {

        prota = GetComponent<Rigidbody2D>();


    }
    private void FixedUpdate()
    {
        prota.velocity = new Vector2(horizontal * velocidad, prota.velocity.y);
        CheckRaycast();

        if (saltar)
        {
            if (unSalto)
            {
                prota.AddForce(new Vector2(0, fuerzaSalto), ForceMode2D.Impulse);
                saltar = false;
            }
            else if (dosSaltos)
            {
                prota.AddForce(new Vector2(0, fuerzaSalto), ForceMode2D.Impulse);
                dosSaltos = false;
            }
            saltar = false;
        }
    }


    private void CheckRaycast()
    {
        rayito = Physics2D.Raycast(transform.position, Vector2.down, 1.03f, layer);
        if (rayito.collider != null)
        {
            unSalto = true;
            dosSaltos = true;
        }
    }




    public void ReadDireccion(InputAction.CallbackContext Context)
    {
        horizontal = Context.ReadValue<float>();
    }

    public void ReadJump(InputAction.CallbackContext Context)
    {
        if (Context.performed)
        {
            unSalto = true;
        }
        if (unSalto || dosSaltos)
        {
            saltar = true;
        }
    }

}
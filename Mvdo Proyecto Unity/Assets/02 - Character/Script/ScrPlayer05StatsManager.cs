using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrPlayer05StatsManager : MonoBehaviour
{
    [Header("Estadisticas de Movimiento")]
    public float walkSpeed;
    public float acceleration; // Añadido para suavizar el arranque y la parada
    public float deceleration; // Añadido para suavizar la desaceleración
    public float turnResistance;  // Nuevo parámetro para controlar la resistencia del giro
}

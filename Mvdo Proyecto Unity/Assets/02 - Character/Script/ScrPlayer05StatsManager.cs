using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrPlayer05StatsManager : MonoBehaviour
{
    [Header("Estadisticas de Movimiento")]
    public float walkSpeed;
    public float acceleration; // A�adido para suavizar el arranque y la parada
    public float deceleration; // A�adido para suavizar la desaceleraci�n
    public float turnResistance;  // Nuevo par�metro para controlar la resistencia del giro
}

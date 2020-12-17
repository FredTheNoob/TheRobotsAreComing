using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Denne float repræsenterer den hastighed spilleren kan bevæge sig med
    public float speed = 12;
    // Denne vector2 fortæller hvilken vej spilleren bevæger sig
    public Vector2 movementInput;
    
    // Update bliver kaldt en gang hver frame
    void Update()
    {
        // Vi flytter transformen på vores spiller der hvor spilleren gerne vil hen, vi multiplicerer med hastigheden
        transform.Translate(new Vector3(movementInput.x, movementInput.y, 0) * speed * Time.deltaTime);
    }
    
    // EVENT BASERET INPUT SYSTEM
    // Nedenstående funktioner er hooked op i unitys inspector, hvor der er fastsat et playerinput component
    // Funktionerne vil affyre et event (hvis der trykkes på en bestemt knap), og sende disse events ud til de korrekte funktioner i dette script
    
    // Denne void er i stand til at aflæse vores movementInput, som fortæller retningen på spilleren,
    // og på denne måde flytte spilleren, hvis der detekteres knaptryk eller andet aktivitet på enten controller eller tastatur
    // Eftersom at koden ikke forstår at vi har at gøre med en vektor2, kalder vi readValue funktionen
    public void OnMove(InputAction.CallbackContext ctx) => movementInput = ctx.ReadValue<Vector2>();
    
}

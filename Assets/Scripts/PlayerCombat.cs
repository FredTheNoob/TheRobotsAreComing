using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    
    // En reference til vores animator, som gør os i stand til at afspille animationer, osv.
    public Animator anim;

    // Disse to positioner er hvor vi slår eller sparker når vores spiller strækker armen eller benet
    public Transform punchPoint;
    public Transform kickPoint;
    
    // En reference til fjendens layer, som laves og sættes i inspectoren
    public LayerMask enemyLayers;

    // Når vi trykker på en slåknap laves der en circlecollider, som vi her sætter radiusen af
    public float attackRange = 0.5f;
    // Hvor meget skade spilleren gør per slag
    public int attackDamage = 40;
    
    public float attackRate = 2f; // Hvor mange gange man kan slå i sekundet - I øjeblikket 2 gange per sekund
    private float nextAttackTime = 0;
    
    // Dette event kører når vi slår, vi tager en bool som parameter, for at sikre os at dette event kun kører en gang
    void OnPunch(bool punching)
    {
        // Hvis vi slår
        if (punching)
        {
            // Så kalder vi damager funktionen, med slåpunktet og navnet på den animation vi vil afspille,
            // hvis vi detekterer at fjenden er indenfor der hvor spilleren slår
            Damager(punchPoint, "IsPunching");
        }
    }

    void OnKick(bool kicking)
    {
        if (kicking)
        {
            Damager(kickPoint, "IsKicking");
        }
    }

    // Denne funktion er ansvarlig for at tjekke om vi rammer en fjende, og derudover påføre skade til fjenden, hvis dette sker
    void Damager(Transform attackPoint, string animName)
    {
        // Først tjekker vi om vi har mulighed for at slå (dette if statement virker som en cooldown)
        // Vi tjekker om den nuværende tid i sekunder er større end nextAttackTime
        if (Time.time >= nextAttackTime)
        {
            // Afspil animationen
            anim.SetTrigger(animName);
            
            // Lav en cirkel ved slå/sparkepunktet med vores radius (attackRange), og tjek om vi ramte en fjende
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, 
                attackRange, 
                enemyLayers);
        
            // Iterer gennem vores array af ramte fjender
            foreach (Collider2D enemy in hitEnemies)
            {
                // Få fat i enemy scriptet på fjenden, og kør funktionen TakeDamage og påfør vores satte skade til fjenden
                enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
            }
            
            // Vi ønsker at sætte nextAttackTime ligmed nuværende tid + 1 / raten
            // Eksempel: AttackRate = 2 og Time.time = 5, 5 + 1 / 2 = 3 | Da nextAttackTime er mindre end Time.time, vil man kunne slå en gang til (altså to gange i sekundet)
            nextAttackTime = Time.time + 1f / attackRate;
        }
    }

    // Når man slår vil vi kalde funktionen OnPunch, og derudover også give ctx.performed videre, dette er en bool der enten er sand eller falsk
    // Denne bool fortæller os om spilleren har udført handlingen (at slå), og vil derfor sørge for at funktionen ikke køres flere gange, og kun en gang
    public void OnPunch(InputAction.CallbackContext ctx) => OnPunch(ctx.performed);
    public void OnKick(InputAction.CallbackContext ctx) => OnKick(ctx.performed);
}

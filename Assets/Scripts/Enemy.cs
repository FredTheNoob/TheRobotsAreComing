using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // En reference til vores animator, så vi kan afspille animationer, osv.
    public Animator anim;

    // Vores maxHealth, default er 100 liv, men kan ændres alt efter hvilken fjendetype/class vi har med at gøre
    public int maxHealth = 100;
    // CurrentHealth, en intern int, som holder styr på vores nuværende liv
    private int currentHealth;
    
    void Start()
    {
        // Vi starter med at sætte vores nuværende liv til max liv
        currentHealth = maxHealth;
    }

    // Når PlayerCombat kalder dette script vil der medfølge et parameter, som fortæller hvor meget skade vi skal fratrække fjenden
    public void TakeDamage(int damage)
    {
        // Vi tager parameteren og trækker fra fjendens liv
        currentHealth -= damage;
        
        // Afspil en såret animation
        anim.SetTrigger("Hurt");
        
        // Hvis fjenden ikke har mere liv => kør Die funktionen
        if (currentHealth <= 0) Die();
    }

    void Die()
    {
        // Die animation
        anim.SetBool("IsDead", true);
        
        // Afsluttende slår vi fjendens collider fra når spilleren har dræbt ham, således at man kan komme forbi
        GetComponent<Collider2D>().enabled = false;
        // Vi slår også hele scriptet fra, så spilleren ikke fortsat kan skade fjenden, som i forvejen er død.
        this.enabled = false;
    }
    
}

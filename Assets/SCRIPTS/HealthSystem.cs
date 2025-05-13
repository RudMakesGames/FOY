using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private int MaxHealth;
    public int CurrentHealth;
    public GameObject DamageVignette;

    public static HealthSystem instance;

    private void Awake()
    {
        if(instance == null)
         instance = this;
    }
    void Start()
    {
        CurrentHealth = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        healthSlider.value = CurrentHealth;
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        StartCoroutine(DamageFlash());
        if(CurrentHealth <= 0)
        {
            HandleDeath();
        }
    }
    void HandleDeath()
    {

    }
    public void Heal(int hp)
    {
        CurrentHealth += hp;
    }
    IEnumerator DamageFlash()
    {
       DamageVignette.SetActive(true);
        yield return new WaitForSeconds(0.25f);
        DamageVignette.SetActive(false);
    }
}

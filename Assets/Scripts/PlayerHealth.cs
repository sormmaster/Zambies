using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 100f;
    [SerializeField] Text healthInfo;
    [SerializeField] Image showHit;

    public void Start()
    {
        updateHealth();
    }

    public void gotHit(float damage)
    {
        hitPoints -= damage;
        updateHealth();
        StartCoroutine(splat());
        if (hitPoints <= 0)
        {
            GetComponent<DeathHandler>().die();
        } 
    }

    public void updateHealth()
    {
        healthInfo.text = "hp: " + hitPoints.ToString();
    }

    IEnumerator splat()
    {
        float transparency = 0.0f;
        var temp = showHit.color;
        while(transparency < 0.35f)
        {
            transparency += 0.05f;
            temp.a = transparency;
            showHit.color = temp;
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(1.0f);
        while(transparency > 0.0f)
        {
            transparency -= 0.05f;
            temp.a = transparency;
            showHit.color = temp;
            yield return new WaitForSeconds(0.1f);
        }
        
    }
}

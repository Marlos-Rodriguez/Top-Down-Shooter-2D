using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Controller : MonoBehaviour
{
    HealthController health;    //Create a new class of Health
    private bool life = true;   //Boolean of Life

    private Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        health = new HealthController(100); //Add the life
        startPosition = transform.position; //Possition initial
    }

    //Damage for the Enemy
    public void EnemyDamage()
    {
        int Damage = Random.Range(10, 20);  //A Random number for the damage
        life = health.Damage(Damage);   //Call the Damage Funtion and assing the bool value for the life
        if (!life) Dead();  //If the life is false call the Dead Function
    }

    private void Dead()
    {
        //Work later
        Debug.Log("SE MURIO");
        GetComponent<EnemyIA>().Dead();
    }
}

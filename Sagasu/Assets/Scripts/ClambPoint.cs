using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClambPoint : MonoBehaviour
{
    [SerializeField] private int num;
    [SerializeField] private Staircase staircase;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Character player = collision.GetComponent<Character>();
            if(player.hasFoots == true && SystemCtrl.canCtrl == true)
            {
                staircase.PlayerClamb(num, player);
            }
        }
    }
}

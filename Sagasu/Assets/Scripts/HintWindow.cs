using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintWindow : MonoBehaviour
{
    public enum Category
    {
        Door,
        Cyclone
    }
    public Category category;

    [SerializeField] GameObject[] hints;
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
            switch (category)
            {
                case Category.Cyclone:
                    if (!player.hasWing) hints[0].SetActive(true);
                    break;
                case Category.Door:
                    if (player.hasHands) hints[1].SetActive(true);
                    else hints[0].SetActive(true);
                    break;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            for(int i = 0; i < hints.Length; i++)
            {
                hints[i].SetActive(false);
            }
        }
    }
}

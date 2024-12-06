using System;
using UnityEngine;

public class coinManager : MonoBehaviour
{
   [SerializeField] private gameManagerScript gameManager;

   private void Start()
   {
      gameManager = FindFirstObjectByType<gameManagerScript>();
   }

   private void OnTriggerEnter(Collider other)
   {
      if (other.gameObject.CompareTag("Player"))
      {
         gameManager.addCoin();
         Destroy(gameObject);
      }
   }
}
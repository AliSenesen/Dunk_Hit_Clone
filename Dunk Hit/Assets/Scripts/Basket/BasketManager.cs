using System;
using Ball;
using Enums;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Basket
{
    public class BasketManager : MonoBehaviour
    {
        [SerializeField] private GameObject rightBasketPrefab;
        [SerializeField] private GameObject leftBasketPrefab;
        [SerializeField] private BallController ballController;
        [SerializeField] private GameObject particlePrefab;
       
        
        private void Awake()
        {
            ballController.Direction = 1;
            rightBasketPrefab.SetActive(true);
            leftBasketPrefab.SetActive(false);
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void SubscribeEvents()
        {
            BasketSignals.Instance.onChangeBasketDirection += OnChangeBasket;
        }
        private void UnSubscribeEvents()
        {
            BasketSignals.Instance.onChangeBasketDirection -= OnChangeBasket;
        }
        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void OnChangeBasket()
        {
            if (ballController.Direction == 1)
            {
                rightBasketPrefab.SetActive(true);
                RandomSpawnPos();
                Instantiate(particlePrefab,leftBasketPrefab.transform.position,leftBasketPrefab.transform.rotation);
                leftBasketPrefab.SetActive(false);
                
            }
            else if (ballController.Direction == -1)
            {
                leftBasketPrefab.SetActive(true);
                RandomSpawnPos();
                Instantiate(particlePrefab,rightBasketPrefab.transform.position,rightBasketPrefab.transform.rotation);
                rightBasketPrefab.SetActive(false);
                
            }
        }
        private void RandomSpawnPos()
        {
            rightBasketPrefab.transform.position =
                new Vector2(2.7f, Random.Range(-3f, 3f));
            leftBasketPrefab.transform.position =
                new Vector2(-2.7f, Random.Range(-3f, 3f));
        }
    }
}
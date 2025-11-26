using System;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    // Instancia estática para implementar el patrón Singleton
    private static BulletPool _instance;

    // Acceso público al Singleton
    public static BulletPool Instance
    {
        get 
        {
            // Si algo falló y no hay instancia, mostrar error
            if(_instance == null)
                Debug.LogError("BulletPool instance is missing in the scene!");

            return _instance;
        }
    }

    // Prefab de la bala que se clonará en el pool
    [SerializeField] private Bullet _bulletPrefab;

    // Cantidad inicial de balas que se crearán al inicio
    [SerializeField] private int _initialPoolSize = 10;

    // Lista que contiene todas las balas del pool
    private List<Bullet> _bulletPool = new List<Bullet>();


    private void Awake()
    {
        // -------- SINGLETON SETUP --------
        if (_instance != null && _instance != this)
        {
            // Si ya existe otro BulletPool, destruir este para evitar duplicados
            Destroy(gameObject);
            return;
        }
        else
        {
            // Asignar esta instancia como la global
            _instance = this;
        }

        // -------- POOL SETUP --------
        AddBulletsPool(_initialPoolSize); // Crear las balas iniciales
    }


    // Crea la cantidad indicada de balas y las agrega al pool
    private void AddBulletsPool(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Bullet bullet = Instantiate(_bulletPrefab); // Crear nueva bala
            bullet.gameObject.SetActive(false);         // Desactivarla para el pool
            _bulletPool.Add(bullet);                    // Guardarla en la lista
            bullet.transform.parent = transform;        // Organizar en la jerarquía
        }
    }

    // Método para obtener una bala disponible
    public Bullet RequestBullet()
    {
        // Buscar una bala que esté desactivada
        for (int i = 0; i < _bulletPool.Count; i++)
        {
            if (!_bulletPool[i].gameObject.activeSelf)
            {
                _bulletPool[i].gameObject.SetActive(true);
                return _bulletPool[i]; // Devolver bala libre
            }
        }

        // Si no había balas disponibles, crear otra
        AddBulletsPool(1);

        // Activar y devolver la nueva bala creada
        _bulletPool[_bulletPool.Count - 1].gameObject.SetActive(true);
        return _bulletPool[_bulletPool.Count - 1];
    }
}

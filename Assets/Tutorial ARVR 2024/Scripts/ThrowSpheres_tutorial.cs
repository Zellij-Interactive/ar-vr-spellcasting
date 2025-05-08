using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ThrowSpheres_tutorial : MonoBehaviour
{
    public GameObject spherePrefab; // Das Prefab der Kugel
    public float bulletSpeed = 10f; // Die Geschwindigkeit der Kugel

    void Update()
    {
        // Wenn die linke Maustaste gedrückt wird
        if (Input.GetMouseButtonDown(0))
        {
            // Erzeuge einen Raycast vom Bildschirm in die Szene
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // Berechne die Mausposition im Spiel
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = ray.direction.z;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            // Berechne die Richtung von der aktuellen Position zur Mausposition
            Vector3 shootDirection = worldPosition - transform.position;
            shootInDirection(shootDirection);
        }
    }

    public void shootInDirection(Vector3 shootDirection)
    {

        // Erzeuge eine Kugel an der aktuellen Position
        GameObject bullet = Instantiate(spherePrefab, transform.position, Quaternion.identity);

        // Setze die Geschwindigkeit der Kugel entsprechend der Richtung
        bullet.GetComponent<Rigidbody>().velocity = shootDirection.normalized * bulletSpeed;
    }

}


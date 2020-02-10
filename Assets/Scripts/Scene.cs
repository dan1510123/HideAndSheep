using System;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

public class Scene : MonoBehaviour
{
    GameObject projectile;
    GameObject player;
    [SerializeField] Mesh playerMesh;
    [SerializeField] Mesh projectileMesh;
    [SerializeField] Material material;
    [SerializeField] public ProjectileBehaviour projectilePrefab;

    private void Start()
    {
        StaticStuff.projectile = projectilePrefab;
        // Create Player
        player = new GameObject();
        player.transform.position = new Vector3(0,0,0);
        player.AddComponent<PlayerBehaviour>().material = this.material;
        player.GetComponent<PlayerBehaviour>().mesh = this.playerMesh;

        // Create Projectile
        //projectile = new GameObject();
        //projectile.transform.position = new Vector3(0,0,0);
        //projectile.AddComponent<ProjectileBehaviour>().material = this.material;
        //projectile.GetComponent<ProjectileBehaviour>().mesh = this.projectileMesh;
    }
}
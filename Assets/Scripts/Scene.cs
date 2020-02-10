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
    

    private void Start()
    {
        player = new GameObject();
        player.transform.position = new Vector3(0,0,0);
        // player.AddComponent<MeshRenderer> ().material = this.material;
        // player.AddComponent<MeshFilter> ().mesh = this.playerMesh;
        player.AddComponent<PlayerBehaviour>().material = this.material;
        player.GetComponent<PlayerBehaviour>().mesh = this.playerMesh;
        projectile = new GameObject();
        projectile.transform.position = new Vector3(0,0,0);
        // projectile.AddComponent<MeshRenderer> ().material = this.material;
        // projectile.AddComponent<MeshFilter> ().mesh = this.projectileMesh;
        projectile.AddComponent<ProjectileBehaviour>().material = this.material;
        projectile.GetComponent<ProjectileBehaviour>().mesh = this.projectileMesh;
    }
}
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MazeContoller : MonoBehaviour
{
    public List<GameObject> goPieces;
     private List<Vector3> v3Spawns;
     private List<Quaternion> qnSpawns;
     private int iRooms;

	void Start ()
    {
        iRooms = goPieces.Count;

        v3Spawns = new List<Vector3>();
        v3Spawns.Add(new Vector3(45, 0, 350));
        v3Spawns.Add(new Vector3(150, 0, 255));
        v3Spawns.Add(new Vector3(45, 0, 150));

        qnSpawns = new List<Quaternion>();
        int q;
        for (q = 0; q<iRooms; q++)
            qnSpawns.Add(Quaternion.Euler(0, 90*q, 0));
        
        while (iRooms > 0)
        {
            int[] rand = { Random.Range(0, iRooms), Random.Range(0, iRooms) };
            Instantiate(goPieces[rand[0]], v3Spawns[rand[1]], qnSpawns[rand[1]]);

            goPieces.Remove(goPieces[rand[0]]);
            v3Spawns.Remove(v3Spawns[rand[1]]);
            qnSpawns.Remove(qnSpawns[rand[1]]);
            
            iRooms = goPieces.Count;
        }
    }

	void Update ()
    {
	
	}
}

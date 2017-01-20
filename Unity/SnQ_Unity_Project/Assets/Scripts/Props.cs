using UnityEngine;
using System.Collections;

public class Props : MonoBehaviour
{
    public Mesh[] mPossibleSpawns;
    public bool[] bSpinnableAxis = { true, true, true };

	void Start ()
    {
        if (mPossibleSpawns.Length > 0)
            GetComponent<MeshFilter>().mesh = mPossibleSpawns[Random.Range(0, mPossibleSpawns.Length)];

        gameObject.AddComponent<MeshCollider>();
        GetComponent<MeshCollider>().convex = true;

        int[] axis = { 0, 0, 0 };
        for (int s=0; s<3; s++)
        {
            if (bSpinnableAxis[s])
                axis[s] = Random.Range(0, 359);
        }
        transform.Rotate(axis[0], axis[1], axis[2]);
	}
}

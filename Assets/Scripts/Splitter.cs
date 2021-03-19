using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;

public class Splitter : MonoBehaviour
{
    // layers to be cut
    public LayerMask maskLayer;
    public int maskLayerNumber;
    // material
    public Material matCross;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float mx = Input.GetAxis("Mouse X");

        transform.Rotate(0, 0, -mx);
        // cutting operation
        if(Input.GetMouseButtonDown(0)){
            // get the colliders
            Collider[] colliders = Physics.OverlapBox(transform.position, new Vector3(transform.localScale.x / 2, transform.localScale.y / 2, transform.localScale.z / 2), transform.rotation, maskLayer);

            foreach (Collider c in colliders)
            {
                Destroy(c.gameObject);

                // GameObject[] objs = c.gameObject.SliceInstantiate(transform.position, transform.up); // Justice, the direction to top
                SlicedHull hull = c.gameObject.Slice(transform.position, transform.up);
                if (hull != null)
                {
                    GameObject lower = hull.CreateLowerHull(c.gameObject, matCross);
                    GameObject upper = hull.CreateUpperHull(c.gameObject, matCross);
                    GameObject[] objs = new GameObject[] {lower, upper};

                    foreach (GameObject obj in objs)
                    {
                        obj.AddComponent<Rigidbody>();
                        obj.AddComponent<MeshCollider>().convex = true; // only convex can be rigid
                        obj.layer = maskLayerNumber;
                    }
                }
                
            }
        }
        
    }
}

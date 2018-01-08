using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceDemoSpawn : MonoBehaviour {
    public GameObject Item;
    
    public bool Preparing;
    public float RespawnLeft = -2000;
    public float RespawnRight = 1300;
    public float Respawnheight = 900;

	public float RefreshLeft;
	public float RefreshRight;
	public float RefreshHeight;
	public float RefreshTime;
    public void RespawnLocation()
    {
        float x = Random.Range(RespawnLeft, RespawnRight);
        float y = Respawnheight;
        GetComponent<GameMode_base>().RespawnLocation = new Vector3(x, y, 0);
    }
    IEnumerator Refresh(GameObject Item)
    {
        yield return new WaitForSeconds(RefreshTime);
		Instantiate(Item, new Vector3(Random.Range(RefreshLeft, RefreshRight), RefreshHeight, 0), new Quaternion());
        Preparing = false;
    }
    void Update()
    {
        if (!Preparing)
            if (GameObject.FindGameObjectWithTag(Item.tag) == null)
            {
                Preparing = true;
                StartCoroutine(Refresh(Item));
            }
    }
}

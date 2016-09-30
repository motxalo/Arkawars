using UnityEngine;
using System.Collections;

public class powerupNormalMovement : MonoBehaviour {

    public Transform objetive;
    private Vector3 dir;
    private float speed = 1f;
	private GameObject player;
	public enum powerup {big,small,life,glue} ;
	public powerup lista;

    // Use this for initialization
    void Start()
    {
		player = GameObject.Find ("Player");
        objetive = GameObject.Find("Planet").transform;
        dir = (objetive.position - transform.position).normalized;
        transform.LookAt(objetive.position);
        powerupController.AddPoweup(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += dir * speed * Time.deltaTime;
    }

	void OnDestroy () {
		powerupController.KillPowerup (gameObject);

		switch (lista)
		{
		case powerup.big:			
			if(player.GetComponent<powerUpBig>())
			{
				player.GetComponent<powerUpBig>().RemoveNoLean();
			}
			player.AddComponent<powerUpBig>();
			break;
		case powerup.small:			
			if(player.GetComponent<powerUpSmall>())
			{
				player.GetComponent<powerUpSmall>().RemoveNoLean();
			}
			player.AddComponent<powerUpSmall>();
			break;
        case powerup.glue:			
			if (player.GetComponent<powerUpGlue>())
            {
                player.GetComponent<powerUpGlue>().Remove();
            }
            player.AddComponent<powerUpGlue>();
            break;
        }
    }
}

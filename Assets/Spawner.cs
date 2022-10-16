using UnityEngine;

using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public GameObject[] spawns;
    public float cooldown_min,cooldown_max;
    private float timer;


    // Start is called before the first frame update
    void Start()
    {
        timer = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > timer)
        {
            timer = Time.time + Random.Range(cooldown_min,cooldown_max);
            SpawnObject();
        }
    }

    private void SpawnObject()
    {


        var metalObject = Instantiate(spawns[Random.Range(0,spawns.Length-1)]);
        metalObject.transform.position = transform.position;


        var rb = metalObject.GetComponent<Rigidbody2D>();
        rb.AddRelativeForce(new Vector2(Random.Range(-0.2f, 0.2f), 0));
        rb.AddTorque(100f);


    }


}

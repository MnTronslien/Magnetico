using UnityEngine;

using Random = UnityEngine.Random;

public class SpeacialSpawner
    : MonoBehaviour
{
    public SpecialObject[] spawns;
    public float cooldown_min, cooldown_max;
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
            timer = Time.time + Random.Range(cooldown_min, cooldown_max);
            SpawnObject();
        }
    }

    private void SpawnObject()
    {

        //Make Selection
        var selection = Random.Range(0, spawns.Length - 1);

        //Play Sound
        var a = spawns[selection].anouncment;
        if (a.Length > 0)
        {
            var s = a[Random.Range(0, a.Length - 1)];
            GetComponent<AudioSource>().PlayOneShot(s);
        }


        //SPAWN OBJECT
        var special = Instantiate(spawns[selection].prefab);
        special.transform.position = transform.position;


        var rb = special.GetComponent<Rigidbody2D>();
        rb.AddRelativeForce(new Vector2(Random.Range(-0.2f, 0.2f), 0));
        rb.AddTorque(100f);


    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatEnemy : MonoBehaviour
{
    public int damageAmount = 20;
    Vector2 m_centerPosition;
    float m_degrees;

    [SerializeField]
    float m_speed = 1.0f;

    [SerializeField] private GameObject pickUp;
    [SerializeField]
    float m_amplitude = 1.0f;

    [SerializeField]
    float m_period = 1.0f;

    [SerializeField]
    float m_radius = 1.0f;

    [SerializeField]
    bool sinWave = true;
    [SerializeField]
    bool circle = false;
    // Start is called before the first frame update
    void Start()
    {
        m_centerPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (sinWave)
        {
            float deltaTime = Time.deltaTime;

            // Move center along x axis
            m_centerPosition.x += deltaTime * -m_speed;

            // Update degrees
            float degreesPerSecond = 360.0f / m_period;
            m_degrees = Mathf.Repeat(m_degrees + (deltaTime * degreesPerSecond), 360.0f);
            float radians = m_degrees * Mathf.Deg2Rad;

            // Offset by sin wave
            Vector2 offset = new Vector3(0.0f, m_amplitude * Mathf.Sin(radians), 0.0f);
            transform.position = m_centerPosition + offset;
        }
        if (circle)
        {
            float degreesPerSecond = 360.0f / m_period;
            m_degrees = Mathf.Repeat(m_degrees + (Time.deltaTime * degreesPerSecond), 360.0f);
            float radians = m_degrees * Mathf.Deg2Rad;

            // Offset on circle
            Vector2 offset = new Vector3(m_radius * Mathf.Cos(radians), m_radius * Mathf.Sin(radians), 0.0f);
            transform.position = m_centerPosition + offset;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Hitting the player");
            other.GetComponent<PlayerHP>().currentHP -= damageAmount;
        }
        if(other.tag == "Weapon")
        {
            Instantiate(pickUp, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
        if(other.tag == "Wall")
        {
            m_speed *= -1;
        }
    }
    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            Debug.Log("hitExit");
            collider.gameObject.GetComponent<PlayerHP>().TakeDamage(10);
        }
    }
    
}

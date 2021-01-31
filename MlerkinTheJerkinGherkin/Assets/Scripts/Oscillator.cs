using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPos;
    [SerializeField] Vector3 movementVector;
    [SerializeField] [Range (0,1)] float movementFactor = 2f;
    [SerializeField] float period = 2f;

    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon) {return;}
        
        float cycles = Time.time / period; // continually growing over time

        const float tau = Mathf.PI * 2; // constant value of 6.283xxx
        float rawSinWave = Mathf.Sin(cycles * tau); // going from 01 to 1

        
        // movementfactor will be cycling between -1 and 1, so we add 1 to get to
        // 0 -> 2 then divide by 2 to get to 0 -> 1 which can be used with our
        // Serialized Field

        movementFactor = (rawSinWave +1f) / 2;
        
        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPos + offset;

    }
}

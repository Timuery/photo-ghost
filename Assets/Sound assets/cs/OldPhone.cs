using UnityEngine;

public class OldPhone : SriptToUse
{
    public AudioClip ringSound;  
    public float minTimeBetweenRings = 10f; 
    public float maxTimeBetweenRings = 30f; 
    public float ringDuration = 5f; 

    private AudioSource audioSource; 
    private float nextRingTime; 
    private bool isPlayerNear = false; 
    private bool isRinging = false;
    private bool First = true;

    private void Start()
    {
        
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        
        audioSource.clip = ringSound;
        audioSource.playOnAwake = false; 

       
        SetNextRingTime();
    }

    private void Update()
    {
        
        if (Time.time >= nextRingTime && !isRinging && First)
        {
            StartCoroutine(RingPhone());
            SetNextRingTime(); 
        }
    }

    private void SetNextRingTime()
    {
        
        nextRingTime = Time.time + Random.Range(minTimeBetweenRings, maxTimeBetweenRings);
    }

    private System.Collections.IEnumerator RingPhone()
    {
        First = false;
        isRinging = true; 
        audioSource.Play(); 

        
        yield return new WaitForSeconds(ringDuration);

        
        if (isRinging)
        {
            audioSource.Stop();
            isRinging = false;
        }
    }

    private void StopPhone()
    {
        if (isRinging) 
        {
            audioSource.Stop(); 
            isRinging = false; 
            Debug.Log("Телефон выключен.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
        }
    }

    public override void ToggleMode()
    {
        StopPhone();
    }
}
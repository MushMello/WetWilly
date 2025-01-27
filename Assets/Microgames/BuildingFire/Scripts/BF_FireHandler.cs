using System.Collections;
using UnityEngine;

// BF_FireHandler
// handles the fires that fall from the building in Building Fire

public class BF_FireHandler : MonoBehaviour
{
    [Header("Fire Controls")]
    [SerializeField][Range(0f,1f)] private float soundChanceNormalized;
    [SerializeField] AudioClip[] audioClips;

    private int scatterScalar; // -1 for left, 1 for right, 0 for no scatter
    private Animator animator;
    private Rigidbody2D rb;
    private Collider2D col;
    private AudioSource audioSource;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        audioSource = transform.parent.parent.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        BF_PlayerHandler targetPlayer = collider.gameObject.GetComponent<BF_PlayerHandler>();
        if(targetPlayer)
        {
            targetPlayer.RegisterSave();
            PlayDefeatSound();
            Destroy(gameObject);
        }
    }

    public void PlayDefeatSound()
    {
        float soundChance = Mathf.Min(soundChanceNormalized, 1f);
        if(Random.Range(0f,1f) < soundChance && audioSource && !audioSource.isPlaying)
        {
            SettingsHandler settings = SettingsHandler.GetSettingsHandler();
            if(settings)
            {
                audioSource.volume = settings.MixedEffectVolume;
            }
            audioSource.clip = audioClips[Random.Range(0,audioClips.Length)];
            audioSource.Play();
        }
    }

    private IEnumerator BeginDisposalTimer(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }

    public void StartScattering()
    {
        if (animator)
        {
            animator.SetBool("IsScattering", true);
        }

        if (rb)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY;
        }

        if(col)
        {
            col.enabled = false;
        }
        
        scatterScalar = Random.Range(0, 2);
        
        if(scatterScalar == 0)
        {
            scatterScalar = -1;
        }

        StartCoroutine(BeginDisposalTimer(10));
    }

    private void FixedUpdate()
    {
        if(scatterScalar != 0 && rb)
        {
            rb.AddForce(new Vector2(scatterScalar, 0));
        }
    }
}

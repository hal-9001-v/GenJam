using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BateriaScript : MonoBehaviour
{
    public BassGyalScript myBadGyal;
    public GameObject bigBattery;
    public GameObject smolBattery;
    private GameObject myParticles;
    private bool destroyed;
    private void Awake()
    {
        if (myBadGyal == null) myBadGyal = FindObjectOfType<BassGyalScript>();
        destroyed = false;

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Bolso" && !bigBattery.activeSelf )
        {
            if (!destroyed)
            {
                StartCoroutine(MassDestruction(col));
            }
        }
    }

    private IEnumerator MassDestruction(Collider col) {

        yield return new WaitForSeconds(0.3f);
        myParticles = Instantiate(GameAssets.i.particles[3], gameObject.transform.position, col.transform.rotation);
        myParticles = Instantiate(GameAssets.i.particles[6], gameObject.transform.position, col.transform.rotation);
        myParticles = Instantiate(GameAssets.i.particles[2], gameObject.transform.position, col.transform.rotation);
        destroyed = true;
        SoundManager.PlaySound(SoundManager.Sound.PUNCHHITS, 0.4f);
        SoundManager.PlaySound(SoundManager.Sound.SYNTHGRUNT, 0.4f);
        myBadGyal.shieldHP--;

    }
}

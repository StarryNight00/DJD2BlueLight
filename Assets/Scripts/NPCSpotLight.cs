using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpotLight : MonoBehaviour
{
    [SerializeField]
    private Light _spotlight;
    private Transform _slTransf;
    private float _slRange;
    private IEnumerator slEffect;

    // private Transform _spotlightTransform;

    private void Awake()
    {
        _spotlight = GetComponent<Light>();
        _slTransf = _spotlight.transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        slEffect = spotlightEffect(_spotlight, _slTransf, _slRange);
        StartCoroutine(slEffect);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator spotlightEffect(
        Light sl, Transform slTransf, float range)
    {
        int counter = 0;
        float secsUntilEffectStart = 1;
        WaitForSeconds wfs = new WaitForSeconds(secsUntilEffectStart);

        while (true)
        {
            counter++;
            Debug.Log(counter);
            yield return wfs;
        }
    }
}

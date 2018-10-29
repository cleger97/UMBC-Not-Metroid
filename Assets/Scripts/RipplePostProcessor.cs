using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RipplePostProcessor : MonoBehaviour
{
    public Material RippleMaterial;
    public float MaxAmount = 50f;
    public bool ripple = false;
    [Range(0, 1)]
    public float Friction = .9f;
    [SerializeField]
    private GameObject platform;
    private float Amount = 0f;

    void Update()
    {
        if (ripple)
        {
            this.Amount = this.MaxAmount;
            Vector3 pos = platform.transform.position;
            this.RippleMaterial.SetFloat("_CenterX", pos.x);
            this.RippleMaterial.SetFloat("_CenterY", pos.y);
            ripple = false;
        }

        this.RippleMaterial.SetFloat("_Amount", this.Amount);
        this.Amount *= this.Friction;
    }

    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        Graphics.Blit(src, dst, this.RippleMaterial);
    }
}

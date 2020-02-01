using UnityEngine;

[ExecuteInEditMode]
public class RadialBlur : MonoBehaviour
{
    [SerializeField] private Shader _shader;
    [Tooltip("テクスチャをサンプリングする回数、多いほど重くなる")]
    [Range(4, 32)] public float Samples = 16;
    [Tooltip("ブラーの強度")]
    public float Strength = 1;
    [Tooltip("ブラーの中心")]
    public Vector2 Center = new Vector2(0.5f, 0.5f);
    [Tooltip("ブラーに影響されない範囲の半径")]
    public float UnaffectedRadius = 0.1f;

    private Material _material;

    void OnRenderImage(RenderTexture source, RenderTexture dest)
    {
        var mat = GetMaterial();
        mat.SetFloat("_Samples", Samples);
        mat.SetFloat("_Strength", Strength);
        mat.SetFloat("_CenterX", Center.x);
        mat.SetFloat("_CenterY", Center.y);
        mat.SetFloat("_UnaffectedRadius", UnaffectedRadius);
        Graphics.Blit(source, dest, mat);
    }

    private Material GetMaterial()
    {
        if (_material == null)
        {
            _material = new Material(_shader);
            _material.hideFlags = HideFlags.DontSave;
        }
        return _material;
    }
}
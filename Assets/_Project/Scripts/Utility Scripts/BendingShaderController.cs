using UnityEngine;

public static class BendingShaderController
{
    static readonly int _tiling = Shader.PropertyToID("_Tiling");
    static readonly int _offSet = Shader.PropertyToID("_OffSet");
    static readonly int _texture = Shader.PropertyToID("_Texture");
    static readonly int _color = Shader.PropertyToID("_Color");
    static readonly int _normalMap = Shader.PropertyToID("_NormalMap");
    static readonly int _specularMap = Shader.PropertyToID("_SpecularMap");
    static readonly int _metallic = Shader.PropertyToID("_Metallic");
    static readonly int _smoothness = Shader.PropertyToID("_Smoothness");
    static readonly int _emissionMap = Shader.PropertyToID("_EmissionMap");
    static readonly int _emissionIntensity = Shader.PropertyToID("_EmissionIntensity");
    static readonly int _verticalBendingAmount = Shader.PropertyToID("_VerticalAmount");
    static readonly int _horizontalBendingAmount = Shader.PropertyToID("_HorizontalAmount");
    static readonly int _bendingStartDistanceOffSet = Shader.PropertyToID("_PositionOffSet");
    static readonly int _alphaClip = Shader.PropertyToID("_Alpha_Clip");
    //===============================================================
    public static void SetTiling(Material material, Vector2 vector)
    {
        material.SetVector(_tiling, vector);
    }
    public static void SetTiling(Material material, float x, float y)
    {
        Vector2 vector = new Vector2(x, y);
        SetTiling(material, vector);
    }

    public static void SetTiling(Material material, Vector3 vector)
    {
        Vector2 vector2 = new Vector2(vector.x, vector.y);
        SetTiling(material, vector2);
    }
    //===============================================================
    public static void SetOffSet(Material material, Vector2 vector)
    {
        material.SetVector(_offSet, vector);
    }
    public static void SetOffSet(Material material, float x, float y)
    {
        Vector2 vector = new Vector2(x, y);
        SetOffSet(material, vector);
    }

    public static void SetOffSet(Material material, Vector3 vector)
    {
        Vector2 vector2 = new Vector2(vector.x, vector.y);
        SetOffSet(material, vector2);
    }
    //===============================================================
    public static void SetTexture(Material material, Texture2D texture)
    {
        material.SetTexture(_texture, texture);
    }
    //===============================================================
    public static void SetNormalMap(Material material, Texture2D texture)
    {
        material.SetTexture(_normalMap, texture);
    }
    //===============================================================
    public static void SetSpecularMap(Material material, Texture2D texture)
    {
        material.SetTexture(_specularMap, texture);
    }
    //===============================================================
    public static void SetEmissionMap(Material material, Texture2D texture)
    {
        material.SetTexture(_emissionMap, texture);
    }
    //===============================================================
    public static void SetColor(Material material, Color color)
    {
        material.SetColor(_color, color);
    }
    public static void SetColor(Material material, Color32 color)
    {
        SetColor(material, color);
    }
    //===============================================================
    public static bool TrySetMetallic(Material material, float value)
    {
        if (value < 0 || value > 1) return false;
        material.SetFloat(_metallic, value);
        return true;
    }
    //===============================================================
    public static bool TrySetSmoothness(Material material, float value)
    {
        if (value < 0 || value > 1) return false;
        material.SetFloat(_smoothness, value);
        return true;
    }
    //===============================================================
    public static bool TrySetAlphaClip(Material material, float value)
    {
        if (value < 0 || value > 1) return false;
        material.SetFloat(_alphaClip, value);
        return true;
    }
    //===============================================================
    public static bool TrySetVerticalBending(Material material, float value)
    {
        if (value < -0.08f || value > 0.08f) return false;
        material.SetFloat(_verticalBendingAmount, value);
        return true;
    }
    //===============================================================
    public static bool TrySetHorizontalBending(Material material, float value)
    {
        if (value < -0.08f || value > 0.08f) return false;
        material.SetFloat(_horizontalBendingAmount, value);
        return true;
    }
    //===============================================================
    public static void SetBendingStartDistance(Material material, float value)
    {
        material.SetFloat(_bendingStartDistanceOffSet, value);
    }
    //===============================================================
    public static bool SetEmissionIntensity(Material material, float value)
    {
        if (value < 0 || value > 5) return false;
        material.SetFloat(_emissionIntensity, value);
        return true;
    }
    //===============================================================
    public static void SetBendingEnable(Material material, bool value)
    {
        if (value) material.EnableKeyword("_ENABLEBENDING");
        else material.DisableKeyword("_ENABLEBENDING");
    }
}

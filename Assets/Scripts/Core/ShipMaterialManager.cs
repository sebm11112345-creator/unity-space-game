using UnityEngine;

/// <summary>
/// Manages materials and shaders for spaceship visuals
/// Handles metallic hulls, glowing elements, panel details
/// </summary>
public class ShipMaterialManager : MonoBehaviour
{
    [System.Serializable]
    public class ShipMaterialSet
    {
        public Material hullMaterial;
        public Material accentMaterial;
        public Material glowMaterial;
        public Material detailMaterial;
    }

    private static Material hulMetallicMaterial;
    private static Material accentMaterial;
    private static Material glowMaterial;
    private static Material detailMaterial;

    public static void Initialize()
    {
        // Create hull metallic material
        hulMetallicMaterial = new Material(Shader.Find("Standard"));
        hulMetallicMaterial.name = "ShipHull_Metallic";
        hulMetallicMaterial.SetFloat("_Metallic", 0.8f);
        hulMetallicMaterial.SetFloat("_Glossiness", 0.7f);

        // Create accent material (secondary hull color)
        accentMaterial = new Material(Shader.Find("Standard"));
        accentMaterial.name = "ShipAccent";
        accentMaterial.SetFloat("_Metallic", 0.6f);
        accentMaterial.SetFloat("_Glossiness", 0.5f);

        // Create glow material for lights, screens, etc
        glowMaterial = new Material(Shader.Find("Standard"));
        glowMaterial.name = "ShipGlow";
        glowMaterial.SetFloat("_Glossiness", 0.9f);
        glowMaterial.globalIlluminationFlags = MaterialGlobalIlluminationFlags.BakedEmissive;
        glowMaterial.EnableKeyword("_EMISSION");

        // Create detail material for panels, rivets, etc
        detailMaterial = new Material(Shader.Find("Standard"));
        detailMaterial.name = "ShipDetail";
        detailMaterial.SetFloat("_Metallic", 0.5f);
        detailMaterial.SetFloat("_Glossiness", 0.3f);
    }

    /// <summary>
    /// Creates a hull material with specified color
    /// </summary>
    public static Material CreateHullMaterial(Color color)
    {
        Material mat = new Material(hulMetallicMaterial);
        mat.color = color;
        return mat;
    }

    /// <summary>
    /// Creates an accent material with specified color
    /// </summary>
    public static Material CreateAccentMaterial(Color color)
    {
        Material mat = new Material(accentMaterial);
        mat.color = color;
        return mat;
    }

    /// <summary>
    /// Creates a glowing material (for lights, cockpit, engines)
    /// </summary>
    public static Material CreateGlowMaterial(Color color, float intensity = 1f)
    {
        Material mat = new Material(glowMaterial);
        mat.color = color;
        mat.SetColor("_EmissionColor", color * intensity);
        return mat;
    }

    /// <summary>
    /// Creates a detail material (for panel lines, vents, etc)
    /// </summary>
    public static Material CreateDetailMaterial(Color color)
    {
        Material mat = new Material(detailMaterial);
        mat.color = color;
        return mat;
    }

    /// <summary>
    /// Applies panel line texture to a material
    /// </summary>
    public static void ApplyPanelLines(Material material, Color lineColor)
    {
        Texture2D panelTexture = GeneratePanelTexture(lineColor);
        material.SetTexture("_MainTex", panelTexture);
    }

    /// <summary>
    /// Generates a procedural panel line texture
    /// </summary>
    private static Texture2D GeneratePanelTexture(Color lineColor)
    {
        Texture2D tex = new Texture2D(512, 512, TextureFormat.RGBA32, false);
        Color[] pixels = new Color[512 * 512];

        // Fill with base white
        for (int i = 0; i < pixels.Length; i++)
        {
            pixels[i] = Color.white;
        }

        // Add vertical panel lines every 64 pixels
        for (int x = 64; x < 512; x += 64)
        {
            for (int y = 0; y < 512; y++)
            {
                pixels[y * 512 + x] = lineColor;
                if (x + 1 < 512) pixels[y * 512 + x + 1] = lineColor;
            }
        }

        // Add horizontal panel lines every 64 pixels
        for (int y = 64; y < 512; y += 64)
        {
            for (int x = 0; x < 512; x++)
            {
                pixels[y * 512 + x] = lineColor;
                if (y + 1 < 512) pixels[(y + 1) * 512 + x] = lineColor;
            }
        }

        tex.SetPixels(pixels);
        tex.Apply();
        return tex;
    }

    /// <summary>
    /// Creates a complete ship material set with specified colors
    /// </summary>
    public static ShipMaterialSet CreateShipMaterialSet(Color hullColor, Color accentColor, Color glowColor)
    {
        ShipMaterialSet set = new ShipMaterialSet
        {
            hullMaterial = CreateHullMaterial(hullColor),
            accentMaterial = CreateAccentMaterial(accentColor),
            glowMaterial = CreateGlowMaterial(glowColor, 1.5f),
            detailMaterial = CreateDetailMaterial(Color.Lerp(hullColor, Color.black, 0.3f))
        };
        return set;
    }
}

using UnityEngine;

/// <summary>
/// Generates procedural spaceships with three base types:
/// - Scout (Fast, low armor)
/// - Balanced (Medium everything)
/// - Heavy (Slow, high armor)
/// </summary>
public class ProceduralSpaceshipGenerator : MonoBehaviour
{
    public enum ShipType
    {
        Scout,
        Balanced,
        Heavy
    }

    [System.Serializable]
    public class ShipConfig
    {
        public ShipType type;
        public Color hullColor = Color.white;
        public Color accentColor = Color.cyan;
        public Color glowColor = Color.blue;
        public float scale = 1f;
    }

    public static GameObject GenerateShip(ShipConfig config)
    {
        GameObject shipObject = new GameObject($"{config.type}_Ship");
        MeshFilter meshFilter = shipObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = shipObject.AddComponent<MeshRenderer>();
        Collider collider = shipObject.AddComponent<BoxCollider>();

        ProceduralMeshBuilder builder = new ProceduralMeshBuilder();

        // Initialize materials
        ShipMaterialManager.Initialize();
        var materials = ShipMaterialManager.CreateShipMaterialSet(
            config.hullColor,
            config.accentColor,
            config.glowColor
        );

        switch (config.type)
        {
            case ShipType.Scout:
                GenerateScoutShip(builder, config.scale);
                break;
            case ShipType.Balanced:
                GenerateBalancedShip(builder, config.scale);
                break;
            case ShipType.Heavy:
                GenerateHeavyShip(builder, config.scale);
                break;
        }

        meshFilter.mesh = builder.Build();
        meshRenderer.material = materials.hullMaterial;

        // Add lights for engine glow
        AddShipLights(shipObject, config.glowColor);

        return shipObject;
    }

    /// <summary>
    /// Scout Ship - Fast, aerodynamic, low cargo
    /// </summary>
    private static void GenerateScoutShip(ProceduralMeshBuilder builder, float scale)
    {
        Vector3 baseScale = Vector3.one * scale;

        // Main fuselage (sleek narrow body)
        builder.AddCone(Vector3.zero, 0.8f * scale, 3f * scale, 12);
        builder.AddCylinder(Vector3.back * 1.5f * scale, 0.7f * scale, 2f * scale, 16, true);
        builder.AddBox(Vector3.back * 3f * scale, new Vector3(0.6f, 0.6f, 1.2f) * scale, 2, 2, 2);

        // Wings (swept back)
        builder.AddBox(Vector3.back * 1.2f * scale + Vector3.right * 0.4f * scale,
            new Vector3(0.2f, 0.15f, 1.5f) * scale, 1, 1, 2);
        builder.AddBox(Vector3.back * 1.2f * scale + Vector3.left * 0.4f * scale,
            new Vector3(0.2f, 0.15f, 1.5f) * scale, 1, 1, 2);

        // Engine nacelles (small for speed-focused)
        builder.AddCylinder(Vector3.back * 1f * scale + Vector3.right * 0.45f * scale,
            0.25f * scale, 0.8f * scale, 8, true);
        builder.AddCylinder(Vector3.back * 1f * scale + Vector3.left * 0.45f * scale,
            0.25f * scale, 0.8f * scale, 8, true);

        // Cockpit dome
        builder.AddSphere(Vector3.forward * 0.8f * scale, 0.35f * scale, 8, 12);
    }

    /// <summary>
    /// Balanced Ship - Jack-of-all-trades, medium everything
    /// </summary>
    private static void GenerateBalancedShip(ProceduralMeshBuilder builder, float scale)
    {
        // Main fuselage (moderate cone)
        builder.AddCone(Vector3.zero, 1.0f * scale, 2.5f * scale, 14);
        builder.AddCylinder(Vector3.back * 1.2f * scale, 0.9f * scale, 2.5f * scale, 16, true);
        builder.AddBox(Vector3.back * 3f * scale, new Vector3(0.8f, 0.8f, 1.5f) * scale, 2, 2, 2);

        // Wings (balanced angle)
        builder.AddBox(Vector3.back * 1f * scale + Vector3.right * 0.5f * scale,
            new Vector3(0.25f, 0.2f, 1.2f) * scale, 1, 1, 2);
        builder.AddBox(Vector3.back * 1f * scale + Vector3.left * 0.5f * scale,
            new Vector3(0.25f, 0.2f, 1.2f) * scale, 1, 1, 2);

        // Engine nacelles (medium)
        builder.AddCylinder(Vector3.back * 0.8f * scale + Vector3.right * 0.55f * scale,
            0.35f * scale, 1.0f * scale, 10, true);
        builder.AddCylinder(Vector3.back * 0.8f * scale + Vector3.left * 0.55f * scale,
            0.35f * scale, 1.0f * scale, 10, true);

        // Cargo module (visible)
        builder.AddBox(Vector3.back * 2.5f * scale, new Vector3(0.7f, 0.6f, 1.2f) * scale, 2, 2, 2);

        // Cockpit dome
        builder.AddSphere(Vector3.forward * 0.7f * scale, 0.4f * scale, 10, 14);
    }

    /// <summary>
    /// Heavy Ship - Tank, slow, heavily armored
    /// </summary>
    private static void GenerateHeavyShip(ProceduralMeshBuilder builder, float scale)
    {
        // Main fuselage (large blunt nose)
        builder.AddCone(Vector3.zero, 1.3f * scale, 2.0f * scale, 14);
        builder.AddCylinder(Vector3.back * 1f * scale, 1.2f * scale, 3f * scale, 18, true);
        builder.AddBox(Vector3.back * 3.5f * scale, new Vector3(1.0f, 1.0f, 2.0f) * scale, 3, 3, 3);

        // Thick armor plating (boxier)
        builder.AddBox(Vector3.back * 1f * scale + Vector3.right * 0.6f * scale,
            new Vector3(0.3f, 0.4f, 1.5f) * scale, 2, 2, 2);
        builder.AddBox(Vector3.back * 1f * scale + Vector3.left * 0.6f * scale,
            new Vector3(0.3f, 0.4f, 1.5f) * scale, 2, 2, 2);

        // Engine nacelles (large and powerful)
        builder.AddCylinder(Vector3.back * 0.5f * scale + Vector3.right * 0.7f * scale,
            0.45f * scale, 1.3f * scale, 12, true);
        builder.AddCylinder(Vector3.back * 0.5f * scale + Vector3.left * 0.7f * scale,
            0.45f * scale, 1.3f * scale, 12, true);

        // Multiple cargo modules
        builder.AddBox(Vector3.back * 2.2f * scale, new Vector3(0.9f, 0.8f, 1.5f) * scale, 3, 2, 2);
        builder.AddBox(Vector3.back * 4f * scale, new Vector3(0.9f, 0.8f, 1.5f) * scale, 3, 2, 2);

        // Weapon turret mounts
        builder.AddCylinder(Vector3.up * 0.8f * scale + Vector3.back * 1.5f * scale,
            0.25f * scale, 0.5f * scale, 8, true);
        builder.AddCylinder(Vector3.down * 0.8f * scale + Vector3.back * 1.5f * scale,
            0.25f * scale, 0.5f * scale, 8, true);

        // Large cockpit dome
        builder.AddSphere(Vector3.forward * 0.9f * scale, 0.45f * scale, 12, 16);
    }

    /// <summary>
    /// Adds lighting effects to the ship (engine glow, cockpit lights)
    /// </summary>
    private static void AddShipLights(GameObject ship, Color glowColor)
    {
        // Engine glow light
        GameObject engineLight = new GameObject("EngineLight");
        engineLight.transform.parent = ship.transform;
        engineLight.transform.localPosition = new Vector3(0, 0, -3);
        Light light = engineLight.AddComponent<Light>();
        light.type = LightType.Point;
        light.color = glowColor;
        light.intensity = 1.5f;
        light.range = 10f;

        // Cockpit light
        GameObject cockpitLight = new GameObject("CockpitLight");
        cockpitLight.transform.parent = ship.transform;
        cockpitLight.transform.localPosition = new Vector3(0, 0, 1);
        Light cockpitLightComponent = cockpitLight.AddComponent<Light>();
        cockpitLightComponent.type = LightType.Point;
        cockpitLightComponent.color = Color.blue;
        cockpitLightComponent.intensity = 0.8f;
        cockpitLightComponent.range = 5f;
    }
}

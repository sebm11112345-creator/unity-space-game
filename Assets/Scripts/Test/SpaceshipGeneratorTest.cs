using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Test scene for spaceship generator
/// Press 1, 2, 3 to generate different ship types
/// </summary>
public class SpaceshipGeneratorTest : MonoBehaviour
{
    private GameObject currentShip;
    private float shipRotation = 0f;

    void Start()
    {
        // Create test camera
        if (Camera.main == null)
        {
            GameObject cameraObj = new GameObject("Main Camera");
            Camera camera = cameraObj.AddComponent<Camera>();
            cameraObj.tag = "MainCamera";
            cameraObj.transform.position = new Vector3(0, 2, 5);
            cameraObj.transform.LookAt(Vector3.zero);
        }

        // Create lighting
        CreateTestLighting();

        // Generate initial ship
        GenerateScoutShip();
    }

    void Update()
    {
        // Keyboard shortcuts to generate different ships
        if (Keyboard.current.digit1Key.wasPressedThisFrame)
            GenerateScoutShip();
        if (Keyboard.current.digit2Key.wasPressedThisFrame)
            GenerateBalancedShip();
        if (Keyboard.current.digit3Key.wasPressedThisFrame)
            GenerateHeavyShip();

        // Rotate ship for viewing
        if (currentShip != null)
        {
            shipRotation += 30f * Time.deltaTime;
            currentShip.transform.rotation = Quaternion.Euler(0, shipRotation, 0);
        }
    }

    private void GenerateScoutShip()
    {
        if (currentShip != null)
            Destroy(currentShip);

        var config = new ProceduralSpaceshipGenerator.ShipConfig
        {
            type = ProceduralSpaceshipGenerator.ShipType.Scout,
            hullColor = new Color(0.2f, 0.8f, 1f), // Cyan
            accentColor = new Color(0.1f, 1f, 0.8f), // Bright cyan-green
            glowColor = Color.cyan,
            scale = 1f
        };

        currentShip = ProceduralSpaceshipGenerator.GenerateShip(config);
        currentShip.transform.position = Vector3.zero;
        Debug.Log("Generated Scout Ship (Press 1)");
    }

    private void GenerateBalancedShip()
    {
        if (currentShip != null)
            Destroy(currentShip);

        var config = new ProceduralSpaceshipGenerator.ShipConfig
        {
            type = ProceduralSpaceshipGenerator.ShipType.Balanced,
            hullColor = new Color(0.7f, 0.7f, 0.8f), // Light gray-blue
            accentColor = new Color(1f, 0.8f, 0.2f), // Gold
            glowColor = new Color(1f, 0.6f, 0.2f), // Orange
            scale = 1.2f
        };

        currentShip = ProceduralSpaceshipGenerator.GenerateShip(config);
        currentShip.transform.position = Vector3.zero;
        Debug.Log("Generated Balanced Ship (Press 2)");
    }

    private void GenerateHeavyShip()
    {
        if (currentShip != null)
            Destroy(currentShip);

        var config = new ProceduralSpaceshipGenerator.ShipConfig
        {
            type = ProceduralSpaceshipGenerator.ShipType.Heavy,
            hullColor = new Color(0.4f, 0.4f, 0.5f), // Dark gray
            accentColor = new Color(1f, 0.3f, 0.2f), // Red
            glowColor = new Color(1f, 0.2f, 0.1f), // Dark red
            scale = 1.5f
        };

        currentShip = ProceduralSpaceshipGenerator.GenerateShip(config);
        currentShip.transform.position = Vector3.zero;
        Debug.Log("Generated Heavy Ship (Press 3)");
    }

    private void CreateTestLighting()
    {
        // Main directional light
        GameObject mainLightObj = new GameObject("Main Light");
        Light mainLight = mainLightObj.AddComponent<Light>();
        mainLight.type = LightType.Directional;
        mainLight.intensity = 1.2f;
        mainLightObj.transform.rotation = Quaternion.Euler(45, 45, 0);

        // Fill light
        GameObject fillLightObj = new GameObject("Fill Light");
        Light fillLight = fillLightObj.AddComponent<Light>();
        fillLight.type = LightType.Directional;
        fillLight.color = new Color(0.6f, 0.6f, 0.8f);
        fillLight.intensity = 0.5f;
        fillLightObj.transform.rotation = Quaternion.Euler(-30, -45, 0);
    }
}

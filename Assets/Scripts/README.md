# Unity Space Game - Scripts Structure

## Core Systems

### ProceduralMeshBuilder.cs
General-purpose procedural mesh generation system. Supports:
- Cylinders with configurable segments
- Boxes with subdivided faces
- Cones for pointed structures
- Spheres for rounded elements
- Tori for orbital mechanics
- Mesh combining and merging

**Key Methods:**
- `AddCylinder(position, radius, height, segments, capped)`
- `AddBox(position, size, segmentsX, segmentsY, segmentsZ)`
- `AddCone(position, baseRadius, height, segments)`
- `AddSphere(position, radius, latSegments, longSegments)`
- `AddTorus(position, majorRadius, minorRadius, majorSegs, minorSegs)`
- `Mesh Build()` - Finalizes and returns the mesh

### ShipMaterialManager.cs
Handles all material and shader assignments for ships.
- Creates metallic hull materials
- Creates glow materials for lights and screens
- Generates procedural panel line textures
- Manages material sets (hull, accent, glow, detail)

### ProceduralSpaceshipGenerator.cs
Main spaceship generation system with 3 base types:
1. **Scout** - Fast, aerodynamic, small cargo
2. **Balanced** - Medium stats, versatile
3. **Heavy** - Slow, heavily armored, large cargo

**Ship Components Generated:**
- Fuselage (cone or blunt nose)
- Wings/stabilizers
- Engine nacelles
- Cockpit dome
- Cargo modules
- Weapon hardpoints

### ShipUpgradeSystem.cs
Manages ship upgrades and their visual representation.
- Supports 5 upgrade types: Engine, Armor, Cargo, Weapon, Shield
- Max 2 upgrade slots for starting ships
- Tracks upgrade performance bonuses
- Applies visual modifications

### SpaceshipGeneratorTest.cs
Test scene for viewing and testing spaceship generation.
- Press 1: Generate Scout Ship
- Press 2: Generate Balanced Ship
- Press 3: Generate Heavy Ship
- Ships auto-rotate for easy viewing

## Medium-Poly Quality Techniques

1. **Smooth Normals** - Proper vertex normal calculation
2. **Segment Resolution** - Adequate segments for smooth appearance (8-24 depending on shape)
3. **Material Quality** - Metallic shaders with proper parameters
4. **Lighting** - Point lights for engine/cockpit glow
5. **UV Mapping** - Proper texture coordinates for detail
6. **Mesh Optimization** - Efficient vertex/triangle usage

## Next Steps

1. Create Asteroid Generator (procedural terrain)
2. Create Weapon Visual System (particle-based)
3. Create Character Customization System
4. Create Inventory System
5. Create Crafting System
6. Create Mission System
7. Implement Multiplayer Networking

## Performance Targets

- Scout Ship: ~800-1000 vertices
- Balanced Ship: ~1200-1500 vertices
- Heavy Ship: ~1600-2000 vertices
- Average frame time: <16ms at 60fps

## Color Schemes (Default)

- **Scout:** Cyan hull, cyan-green accents, cyan glow
- **Balanced:** Light gray hull, gold accents, orange glow
- **Heavy:** Dark gray hull, red accents, dark red glow

All colors are customizable per ship instance.

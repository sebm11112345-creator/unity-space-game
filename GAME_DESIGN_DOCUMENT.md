# Unity Space Game - Design Document

**Engine:** Unity Editor 6.6  
**Genre:** Space MMO with Ready Player One Aesthetic  
**Platform:** Online Multiplayer

---

## Table of Contents
1. [Core Gameplay Loop](#core-gameplay-loop)
2. [Character & Spaceship Creation](#character--spaceship-creation)
3. [Missions & Progression](#missions--progression)
4. [Damage & Hazards](#damage--hazards)
5. [Crafting System](#crafting-system)
6. [Items & Materials](#items--materials)
7. [Upgrade System](#upgrade-system)

---

## Core Gameplay Loop

### Game Start
1. Player spawns in **Space Station Hub**
2. Character Creation (cosmetics, appearance)
3. Loadout Selection (tools, weapons)
4. Spaceship Selection & Customization
5. First Mission Briefing

### Session Flow
- **Explore → Gather → Craft → Upgrade → Deploy on Missions**
- Missions provide primary progression and material rewards
- Crafting enables customization and power scaling
- Ship integrity management is critical

---

## Character & Spaceship Creation

### Character Creation
- **Customization Options:** Name, appearance (Ready Player One style avatar system)
- **Starting Loadout:** 3 tool slots, 3 weapon slots (configurable)
- **Progression:** Appearance customization unlocked through gameplay achievements

### Spaceship Selection
- **Base Types:** 3 starting archetypes
  1. **Scout** - High speed, low armor, small cargo
  2. **Balanced** - Medium stats across the board
  3. **Heavy** - Low speed, high armor, large cargo

- **Upgrade Slots:** Max 2 upgrades on starting ship
- **Upgrade Types:** 3 possible upgrades per tier
  - Engine Enhancement (speed/efficiency)
  - Armor Plating (durability)
  - Cargo Expansion (storage)
  - Weapon Hardpoint
  - Shield Generator

---

## Missions & Progression

### First Mission Choice

#### Mission A: Safe Moon (Easy)
- **Objective:** Collect Power Gems from nearby moon
- **Location:** Safe zone (within thermal shielding)
- **Difficulty:** ⭐ Low
- **Hazards:** Minor meteorites, no PvP
- **Rewards:**
  - 10 Power Crystals
  - 20 Scrap
  - 100 Coins
  - 50 XP

#### Mission B: Distant Moon (Hard)
- **Objective:** Collect Medium Power Crystals
- **Location:** Beyond thermal shield (exposed to solar radiation)
- **Difficulty:** ⭐⭐⭐ High
- **Hazards:** Solar radiation, player attacks, meteorites, environmental hazards
- **Respawn:** Player respawns at space station on death (mission failed, partial rewards possible)
- **Rewards:**
  - 10 Medium Power Crystals
  - 30 Scrap
  - 200 Coins
  - 150 XP
  - Unique cosmetic: "Distant Traveler" title

### Post-First Mission
- Unlocked content varies by mission choice
- Safe moon route → Focus on crafting and incremental upgrades
- Dangerous moon route → Faster progression but higher risk tolerance

---

## Damage & Hazards

### Ship Integrity System
Ships lose health over time from multiple sources:

| Hazard | Damage/Min | Notes |
|--------|-----------|-------|
| Solar Radiation | 5-10 HP | Outside thermal shielding, stacks over time |
| Meteorite Strike | 25-50 HP | Random encounters, more frequent in asteroid fields |
| PvP Attack | 10-40 HP | Other players attacking your ship |
| Engine Overheating | 3-8 HP | Sustained high-speed travel |
| Shield Degradation | 2-5 HP | If shields equipped and active |

### Repair Systems
- **On-Station:** Full repair (costs Scrap/Coins)
- **In-Mission:** Emergency patches (temporary, limited resources)
- **Crafted Repairs:** Build repair kits from materials

### Ship Destruction
- Loss of all cargo
- Return to space station with penalty (25% coin loss)
- Cooldown before next mission (5-10 minutes)

---

## Crafting System

### Core Mechanics
- **Workbench Stations:** Located on space station and player bases
- **Crafting Time:** Varies by recipe (5 seconds to 10+ minutes)
- **Power Requirements:** Some recipes need power crystals
- **Tool Requirements:** Advanced recipes need specific tools active

### Recipe Structure
```
[RECIPE NAME]
Materials: [Item] x[Qty], [Item] x[Qty], ...
Tools Required: [Tool] (duration), [Tool] (duration)
Power Cost: [X] Power Crystals
Output: [Item] x[Qty]
Craft Time: [X] seconds
```

---

## Items & Materials

### Tier 1 Materials (Easily Found)

| Item | Source | Rarity | Use |
|------|--------|--------|-----|
| **Scrap** | Missions, destroyed ships, salvage | Common | Ship repair, crafting fuel |
| **Power Crystals** | Safe moon missions, enemy drops | Common | Engine fuel, weapon power |
| **Coins** | Missions, trading, achievements | Common | Universal currency |
| **Thin Wires** | Asteroid fields, salvage crates | Common | Small tools, basic components |

### Tier 2 Materials (Crafted or Found)

| Item | Acquisition | Rarity | Use |
|------|-------------|--------|-----|
| **Thick Wires** | 4 Thin Wires (crafting) OR found in loot | Uncommon | Medium tools, advanced components |
| **Copper Plate** | Asteroid mining, refined from ore | Uncommon | Electrical components, plating |
| **Iron Plate** | Asteroid mining, refined from ore | Uncommon | Structural reinforcement, plating |
| **Steel Ingot** | 5 Iron Plates + 4 Scrap + Low Heat Furnace (2 min) | Uncommon | High-durability parts |

### Tier 3 Materials (Rare, Specialized)

| Item | Acquisition | Rarity | Use |
|------|-------------|--------|-----|
| **Ribbon Wires** | 3 Thick Wires (crafting only) | Rare | Large tools, weapons, advanced systems |
| **Medium Power Crystals** | Hard moon missions, high-level enemies | Rare | Engine upgrades, weapon enhancement |
| **Rare Power Crystals** | Dangerous missions, boss enemies | Epic | Advanced ship systems, end-game weapons |
| **Computer Chip** | Crafted or found in derelict ships | Rare | Control systems, automated tools |
| **RAM Module** | 2 Thin Wires + Computer Chip (20 sec) | Rare | Advanced AI systems, scanner upgrades |

### Tier 4 Materials (Exotic/Endgame)

| Item | Acquisition | Rarity | Use |
|------|-------------|--------|-----|
| **Platinum Alloy** | 2 Steel Ingots + 1 Rare Power Crystal + High Heat Furnace | Legendary | Top-tier armor, weapons |
| **Quantum Processor** | Crafted from Rare Power Crystals + RAM + Computer Chips | Legendary | Ship AI, autopilot systems |
| **Void Crystal** | Endgame bosses, deep space exploration | Mythic | Experimental weapons, shields |

---

## Crafting Recipes (Early Game - First Hour)

### Basic Wiring
```
THICK WIRES (Tier 2)
Materials: Thin Wires x4
Tools Required: None
Power Cost: 0
Output: Thick Wires x1
Craft Time: 15 seconds
```

```
RIBBON WIRES (Tier 3)
Materials: Thick Wires x3
Tools Required: Wire Processor
Power Cost: 1 Power Crystal
Output: Ribbon Wires x1
Craft Time: 45 seconds
```

### Components & Circuits
```
COMPUTER CHIP (Tier 2)
Materials: Thin Wires x3, Copper Plate x1
Tools Required: Soldering Iron
Power Cost: 1 Power Crystal
Output: Computer Chip x1
Craft Time: 30 seconds
```

```
RAM MODULE (Tier 3)
Materials: Thin Wires x2, Computer Chip x1
Tools Required: Soldering Iron
Power Cost: 2 Power Crystals
Output: RAM Module x1
Craft Time: 60 seconds
```

### Metalwork
```
COPPER PLATE (Ore Processing)
Materials: Copper Ore x3, Scrap x1
Tools Required: Low Heat Furnace
Power Cost: 1 Power Crystal
Output: Copper Plate x2
Craft Time: 45 seconds
```

```
IRON PLATE (Ore Processing)
Materials: Iron Ore x3, Scrap x1
Tools Required: Low Heat Furnace
Power Cost: 1 Power Crystal
Output: Iron Plate x2
Craft Time: 45 seconds
```

```
STEEL INGOT (Advanced Metalwork)
Materials: Iron Plate x5, Scrap x4
Tools Required: Low Heat Furnace
Power Cost: 2 Power Crystals
Output: Steel Ingot x3
Craft Time: 120 seconds
```

### Tools & Screens
```
SMALL DRILL (Basic Tool)
Materials: Thin Wires x2, Copper Plate x1, Scrap x2
Tools Required: Soldering Iron
Power Cost: 0
Output: Small Drill x1
Craft Time: 45 seconds
Note: Used for mining Tier 1 asteroids
```

```
SMALL SCREEN (Display Component)
Materials: Thin Wires x3, Copper Plate x1, Computer Chip x1, Ribbon Wires x2
Tools Required: Soldering Iron, Precision Tools
Power Cost: 2 Power Crystals
Output: Small Screen x1
Craft Time: 90 seconds
Note: Used in ship dashboard, scanner upgrades
```

```
LOW HEAT FURNACE (Crafting Station)
Materials: Iron Plate x4, Thick Wires x3, Scrap x5, Steel Ingot x1
Tools Required: None
Power Cost: 5 Power Crystals
Output: Low Heat Furnace x1
Craft Time: 180 seconds
Note: Stationary crafting tool, required for advanced metalwork
```

### Advanced Tools
```
MEDIUM DRILL (Tier 2 Tool)
Materials: Thick Wires x4, Copper Plate x3, Steel Ingot x2, Computer Chip x1
Tools Required: Low Heat Furnace, Soldering Iron
Power Cost: 3 Power Crystals
Output: Medium Drill x1
Craft Time: 120 seconds
Note: Mines Tier 2 asteroids faster, consumes 2 Power Crystals per 2 minutes of use
```

```
LASER CUTTER (Precision Tool)
Materials: Ribbon Wires x2, Copper Plate x2, Steel Ingot x1, Computer Chip x2
Tools Required: Low Heat Furnace, Soldering Iron
Power Cost: 4 Power Crystals
Output: Laser Cutter x1
Craft Time: 150 seconds
Note: Cuts through thick materials, used in precision mining
```

```
COMPRESSION CHAMBER (Storage Component)
Materials: Steel Ingot x2, Thick Wires x3, Copper Plate x2, Scrap x3
Tools Required: Low Heat Furnace
Power Cost: 2 Power Crystals
Output: Compression Chamber x1
Craft Time: 90 seconds
Note: Increases ship cargo space by 25%
```

### Weapons & Defense
```
LASER MODULE (Weapon Component)
Materials: Ribbon Wires x3, Copper Plate x2, Rare Power Crystals x1
Tools Required: Soldering Iron
Power Cost: 5 Power Crystals
Output: Laser Module x1
Craft Time: 120 seconds
Note: Used in laser drill, burns out after 60 seconds of continuous use
Duration: 60 seconds before needing replacement
```

```
POWER DRILL (Heavy Tool)
Materials: Ribbon Wires x2, Steel Ingot x3, Computer Chip x2, Thick Wires x2
Tools Required: Low Heat Furnace
Power Cost: 3 Power Crystals
Output: Power Drill x1
Craft Time: 150 seconds
Note: High-power mining tool, uses 2 Power Crystals per 30 seconds of active use
Cargo: Can mine Tier 2 & 3 asteroids
```

### Building Materials
```
PANEL (Structural Component)
Materials: Ribbon Wires x2, Steel Ingot x2, Copper Plate x3
Tools Required: Low Heat Furnace
Power Cost: 2 Power Crystals
Output: Panel x1
Craft Time: 90 seconds
Note: Used in planet/moon bases, includes power port connections
Connected panels can share power and resources
```

```
SHELTER FOUNDATION (Base Building)
Materials: Steel Ingot x5, Panel x4, Scrap x10, Thick Wires x4
Tools Required: Low Heat Furnace
Power Cost: 10 Power Crystals
Output: Shelter Foundation x1
Craft Time: 300 seconds
Note: Builds basic shelter on planetary surfaces
Provides protection from radiation and weather
```

### Repair & Maintenance
```
REPAIR KIT (Emergency Repair)
Materials: Scrap x5, Thin Wires x2, Copper Plate x1
Tools Required: None
Power Cost: 0
Output: Repair Kit x1
Craft Time: 30 seconds
Note: Restores 25 ship integrity when used
Single-use item
```

```
REINFORCED PLATING (Armor Enhancement)
Materials: Steel Ingot x3, Iron Plate x4, Thick Wires x2
Tools Required: Low Heat Furnace
Power Cost: 2 Power Crystals
Output: Reinforced Plating x1
Craft Time: 120 seconds
Note: Can be installed on ship for permanent +15 armor
Upgrade slot required
```

---

## Upgrade System

### Spaceship Upgrades (Max 2 at Start)

#### Engine Upgrades
- **Small Engine Boost** - +10% speed, -5% cargo
- **Medium Engine Optimizer** - +20% speed, +5% efficiency
- **Large Engine Overdrive** - +30% speed, requires 2 upgrade slots

#### Armor Upgrades
- **Light Plating** - +25 armor, no penalty
- **Heavy Plating** - +50 armor, -10% speed
- **Reinforced Hull** - +75 armor, requires 2 upgrade slots

#### Cargo Upgrades
- **Cargo Expansion I** - +50% capacity
- **Cargo Expansion II** - +100% capacity
- **Pressurized Hold** - +150% capacity, maintains item integrity during damage

#### Weapons
- **Laser Turret** - Energy-based damage, requires Power Crystals
- **Missile Pod** - High single-hit damage
- **Shield Disruptor** - Disables enemy shields temporarily

---

## Progression Timeline (First Hour)

| Time | Milestone | Content |
|------|-----------|---------|
| 0:00 - 0:10 | Character & Ship Creation | Tutorial, loadout selection |
| 0:10 - 0:25 | Space Station Tutorial | Crafting intro, item management |
| 0:25 - 0:55 | First Mission (Choice A or B) | Mining, combat, resource gathering |
| 0:55 - 1:00 | Return & Reward | Unload cargo, first crafting attempts |

---

## Next Steps for Development
- [ ] Create character customization system
- [ ] Build spaceship selection & upgrade UI
- [ ] Implement mission generation system
- [ ] Design asteroid field generation
- [ ] Create crafting UI & progression
- [ ] Implement networking for multiplayer
- [ ] Build damage/health system
- [ ] Design PvP encounter mechanics


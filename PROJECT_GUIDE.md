# BASTION LAST STAND — Project Guide for Claude Code

This document is the single source of truth for the development of Bastion Last Stand in Unreal Engine 5. Every time a phase or task is completed, this document must be updated to reflect the current state. Claude Code should read this file at the start of every session before doing anything else.

---

## Project Identity

**Game Name:** Bastion Last Stand
**Engine:** Unreal Engine 5 (latest stable)
**Project Type:** Blueprints Only — no C++
**Main Level:** `Maps/SandboxLevel`
**Character Base:** GASP (Game Animation Sample Pack) migrated into the project
**Development Style:** Phase by phase, one phase fully polished before moving to the next
**Goal:** Polished portfolio prototype demonstrating full game loop

---

## How Claude Code Should Work on This Project

Before touching anything in the engine, Claude Code reads this file. It then reads the current phase section and works only on tasks listed under that phase. It does not jump ahead. When a task is done it marks it with [DONE] and writes a one-line note of what was created or modified. When a full phase is done it updates the Phase Status table below and writes a short summary under the phase's Completion Notes section.

Claude Code communicates through console logs in Blueprints using Print String nodes during development. These are tagged with the system name so they are easy to find, for example `[MOVEMENT] Walking state entered` or `[CRAFTING] Recipe unlocked: Wood Barricade`. All Print String nodes are removed or disabled before a phase is marked polished.

Claude Code never deletes Blueprints or assets without confirming with the developer first. It names every Blueprint, variable, function, and event clearly in English using PascalCase for Blueprint names and snake_case for variables inside them.

---

## Folder Structure

```
Content/
  Bastion/
    Blueprints/
      Character/
      Enemies/
      Items/
      Crafting/
      Core/
      Traps/
      UI/
      GameMode/
    Animations/
      Character/
    Maps/
      SandboxLevel
    Materials/
    Sounds/
      Footsteps/
    UI/
    VFX/
```

All project assets live inside `Content/Bastion/`. Nothing is placed directly in the root Content folder.

---

## Phase Status Overview

| Phase | Name                              | Status      |
|-------|-----------------------------------|-------------|
| 0     | Project Setup                     | COMPLETE    |
| 1     | GASP Migration and Movement       | COMPLETE    |
| 2     | Footstep Sound Surface Detection  | COMPLETE    |
| 3     | Resource Gathering System         | NOT STARTED |
| 4     | Workbench and Crafting System     | NOT STARTED |
| 5     | Base Core and Wave System         | NOT STARTED |
| 6     | Enemy AI and Spawning             | NOT STARTED |
| 7     | Basic Combat                      | NOT STARTED |
| 8     | Day Night Cycle                   | NOT STARTED |
| 9     | UI and Game States                | NOT STARTED |
| 10    | Polish and Portfolio Prep         | NOT STARTED |

---

## PHASE 0 — Project Setup

**Goal:** Starting from an empty level that the developer has already opened in UE5, build the correct folder structure and set up the sandbox level ready for development.

**Note for Claude Code:** The developer creates the UE5 project and opens it manually. When Claude Code connects, an empty default level is already open. Claude Code starts from that point — it does not create the project.

**Tasks:**
- [DONE] Create the full folder structure listed above under Content/Bastion/ — Created 14 directories on disk matching the spec exactly
- [DONE] Create the SandboxLevel map inside Content/Bastion/Maps/ — Saved at /Game/Bastion/Maps/SandboxLevel
- [DONE] Set SandboxLevel as the default map in Project Settings — Updated GameDefaultMap and EditorStartupMap in DefaultEngine.ini
- [DONE] Add a large flat floor plane to the level — Floor_Main actor with Plane mesh scaled 50x50 (5000x5000 units) at origin
- [DONE] Place 4 surface zone floors side by side — Floor_Concrete (X=-750), Floor_Metal (X=-250), Floor_Wood (X=250), Floor_Gravel (X=750), each scaled 5x5 (500x500 units), with distinct colored materials
- [DONE] Label each surface zone using a Text Render actor above each one — Label_Concrete, Label_Metal, Label_Wood, Label_Gravel at Z=100, WorldSize 80
- [DONE] Add a basic skylight and directional light so the level is visible — Sun_DirectionalLight at -45 pitch, Sky_Light with real-time capture, SkyAtmosphere added
- [DONE] Add a PlayerStart actor to the level — Placed at (0, -300, 10) facing the surface zones
- [DONE] Save all and verify the project runs without errors — PIE tested, zero errors, player spawns correctly

**Polish Criteria:** The sandbox level opens, has four labeled surface zones, is lit, and the player can press Play and spawn at the PlayerStart.

**Completion Notes:**
Phase 0 complete. SandboxLevel has a 5000x5000 main floor, four 500x500 colored surface zones (Concrete, Metal, Wood, Gravel) with text labels, directional light and skylight with sky atmosphere for full lighting, and a PlayerStart. Five materials created in Content/Bastion/Materials/. PIE runs clean with no errors.

---

## PHASE 1 — GASP Migration and Movement

**Goal:** Migrate the Game Animation Sample Pack into the project and wire up a fully working third-person character with smooth locomotion.

**Context for Claude Code:** GASP (Game Animation Sample Pack) is a free Unreal Engine sample project. To use it, the developer must have it open in a separate UE5 instance and migrate the necessary assets into this project. Claude Code should guide the migration and then set up the character Blueprint.

**Tasks:**
- [DONE] Identify the core GASP assets needed -- CBP_SandboxCharacter, ABP_SandboxCharacter, SKM_UEFN_Mannequin, animation library, pose search databases, choosers, camera system, input mappings, foley audio
- [DONE] Migrate GASP character assets -- Full GASP migration into Content/ (Blueprints, Characters, Audio, Input, LevelPrototyping folders). Assets kept at original paths to preserve internal references
- [DONE] Create BP_BastionCharacter based on the GASP character Blueprint as parent -- Created as child of CBP_SandboxCharacter at /Game/Bastion/Blueprints/Character/
- [DONE] Assign BP_BastionCharacter as the default pawn in the GameMode Blueprint (BP_BastionGameMode) -- Set in Class Defaults
- [DONE] Create BP_BastionGameMode inside Content/Bastion/Blueprints/GameMode/ and assign it in World Settings -- Also set as GlobalDefaultGameMode in DefaultEngine.ini
- [DONE] Verify walk, run, idle, and directional movement all play correctly with GASP motion matching -- Confirmed working in PIE
- [DONE] Set camera to third-person spring arm setup with reasonable boom length and lag settings -- Inherited from GASP's GameplayCamera system with SpringArm and CameraRig presets
- [DONE] Add Print String logs -- [MOVEMENT] Character spawned fires on BeginPlay
- [DONE] Test on all four surface zones -- Character moves smoothly across all zones

**Polish Criteria:** Character moves fluidly in all directions, animations blend smoothly, no foot sliding, camera feels good, no Blueprint errors in the output log.

**Completion Notes:**
Phase 1 complete. GASP fully migrated with 7 required plugins enabled (PoseSearch, MotionWarping, GameplayCamera, Chooser, AnimationWarping, AnimationLocomotionLibrary, MotionTrajectory). BP_BastionCharacter inherits all GASP locomotion from CBP_SandboxCharacter. BP_BastionGameMode set as world and project GameMode. Character spawns with motion-matched walk, run, idle, and directional movement. Camera system uses GASP's built-in GameplayCamera with multiple rig presets.

---

## PHASE 2 — Footstep Sound Surface Detection

**Goal:** When the player walks, the footstep sound played depends on the physical surface they are walking on.

**Context for Claude Code:** This system uses Unreal's Physical Materials assigned to the floor meshes of each surface zone. The animation Blueprint or the character Blueprint listens to animation notify events (AnimNotify_Footstep) and at that moment casts a line trace downward from the character's foot to detect the Physical Material of the surface. Based on the result it selects and plays the appropriate sound cue.

**Tasks:**
- [DONE] Create 4 Physical Materials: PM_Concrete, PM_Metal, PM_Wood, PM_Gravel -- Created manually in Content/Bastion/Materials/
- [DONE] Assign each Physical Material to the corresponding surface zone floor mesh material -- PM assigned to M_Concrete, M_Metal, M_Wood, M_Gravel via PhysMaterial property. M_Floor_Main also assigned PM_Concrete as default
- [DONE] Import 20 footstep SoundWave assets (5 variants per surface) -- Content/Bastion/Sounds/Footsteps/{Concrete,Metal,Wood,Gravel}/FS_{Surface}_01-05.wav sourced from Footsteps Mini Sound Pack
- [DONE] Hooked into GASP's existing AnimNotify foley system -- Modified BP_AnimNotify_FoleyEvent Received_Notify to cast owner to BP_BastionCharacter and call HandleFootstep, with GASP fallback on cast failure
- [DONE] In BP_BastionCharacter created HandleFootstep function with line trace downward (200 units) from actor location, bTraceComplex=true
- [DONE] Line trace reads Physical Material via GetObjectName + Contains string matching for surface detection (Concrete, Metal, Wood, Gravel)
- [DONE] Each surface branch plays a random sound variant using MakeArray (5 SoundWaves) + GetArrayItem (RandomIntegerInRange 0-4) + PlaySoundAtLocation
- [DONE] Add Print String: `[FOOTSTEP] Surface detected: <surface name>` with debug output for each surface
- [DONE] Test by walking across all 4 surface zones -- All surfaces detected correctly, correct sounds play with randomization

**Design Deviation:** Used direct SoundWave references with MakeArray randomization instead of Sound Cues (SC_Footstep_*). This is simpler and provides equivalent functionality. Sound Cues can be added later during polish if more complex sound design features (layering, modulation) are needed.

**Polish Criteria:** Each surface zone plays a clearly different footstep sound. No incorrect sounds fire. No log errors. Sounds feel timed to the animation.

**Completion Notes:**
Phase 2 complete. Footstep surface detection system fully functional. 4 Physical Materials created and assigned to surface zone materials plus the default floor. 20 SoundWave assets imported (5 per surface: Concrete from PavementTiles, Metal from LowMetal, Wood from Parquet_Floor, Gravel from DirtRoad). HandleFootstep function in BP_BastionCharacter performs line trace, detects Physical Material via string matching, and plays a random sound variant per surface. GASP foley notify system modified to route through our custom handler while preserving fallback to default GASP sounds for non-Bastion characters.

Sound polish applied: VolumeMultiplier tuned per surface (0.25 Concrete/Default, 0.4 Metal/Wood/Gravel), RandomFloatInRange pitch variation (0.92-1.08) on all sounds. GASP default jump/land sounds suppressed via Cast To BP_BastionCharacter checks in both BP_AnimNotify_FoleyEvent Received_Notify (two bail-out paths) and CBP_SandboxCharacter PlayAudioEvent. HandleFootstep includes velocity gate (XY speed > 10 cm/s) to prevent phantom footsteps from landing recovery animations when stationary.

---

## PHASE 3 — Resource Gathering System

**Goal:** Resource nodes are placed in the sandbox. The player can walk up to them and interact to collect resources. Resources are stored in the player's inventory.

**Tasks:**
- [ ] Create an Enum E_ResourceType with values: Wood, Stone, Metal
- [ ] Create a struct S_InventoryItem with fields: ResourceType (E_ResourceType), Amount (Integer)
- [ ] Create BP_ResourceNode as an Actor Blueprint with a StaticMesh component, a collision sphere for proximity detection, and variables: ResourceType, Amount, bHarvested
- [ ] When the player enters the collision sphere, show a world-space prompt "Press E to Gather" using a Widget Component on the node
- [ ] When the player presses E while in range, subtract from the node's Amount, add to the player's inventory array, and if Amount reaches 0 set bHarvested to true and hide the mesh
- [ ] Create an inventory component BP_InventoryComponent and attach it to BP_BastionCharacter — it holds an array of S_InventoryItem and has functions AddItem, RemoveItem, GetAmount
- [ ] Place several resource nodes of each type across the sandbox surface zones
- [ ] Add Print String: `[INVENTORY] Added <amount> <resource> — Total: <total>`
- [ ] Resource nodes respawn after 60 seconds (timer resets bHarvested and restores Amount)

**Polish Criteria:** Player can gather all three resource types. Inventory correctly tracks totals. Interaction prompt appears and disappears correctly. Nodes respawn. No errors in log.

**Completion Notes:**
*(Claude Code fills this in when Phase 3 is complete)*

---

## PHASE 4 — Workbench and Crafting System

**Goal:** A workbench actor exists in the sandbox. The player can interact with it to open a crafting menu and craft items using gathered resources.

**Tasks:**
- [ ] Create BP_Workbench as an Actor Blueprint with a mesh, interaction prompt, and a reference to the crafting widget
- [ ] Create a struct S_CraftingRecipe with fields: RecipeName (String), ResultItem (Enum or String), RequiredResources (Array of S_InventoryItem), Tier (Integer)
- [ ] Create a DataTable DT_CraftingRecipes using S_CraftingRecipe and populate it with at least 6 recipes across Tier 1 and Tier 2
- [ ] Create WBP_CraftingMenu as a UMG widget showing available recipes, their costs, and a Craft button that is only active if the player has enough resources
- [ ] When crafted, call RemoveItem on the inventory component for each required resource and call AddItem for the result
- [ ] Create a crafted items inventory separate from raw resources (or extend the inventory component to handle both types)
- [ ] Place BP_Workbench in the sandbox near the surface zones
- [ ] Add Print String: `[CRAFTING] Opened menu`, `[CRAFTING] Crafted: <item name>`, `[CRAFTING] Not enough resources`

**Polish Criteria:** Crafting menu opens and closes correctly. All 6 recipes are visible. Resources are deducted correctly. Crafted items are tracked. UI is readable.

**Completion Notes:**
*(Claude Code fills this in when Phase 4 is complete)*

---

## PHASE 5 — Base Core and Wave System

**Goal:** A base core actor sits at the center of the sandbox. It has health. A wave manager controls the timing of enemy waves. The game ends if the core reaches zero health.

**Tasks:**
- [ ] Create BP_BaseCore as an Actor Blueprint with a mesh (glowing sphere or pillar), health component, and health bar widget component above it
- [ ] Create BP_HealthComponent as an Actor Component with variables: MaxHealth, CurrentHealth, and functions: TakeDamage, Heal, IsDead — attach it to BP_BaseCore and later to enemies and the player
- [ ] Create BP_WaveManager as an Actor Blueprint placed in the level — it holds wave data and controls the day/night timing loop
- [ ] Create a struct S_WaveData with fields: WaveNumber, EnemyCount, SpawnDelay, EnemyType
- [ ] Create a DataTable DT_Waves using S_WaveData and define at least 5 waves
- [ ] BP_WaveManager fires a delegate/event OnWaveStart and OnWaveEnd that other systems can bind to
- [ ] When the core reaches zero health, BP_WaveManager broadcasts OnGameOver
- [ ] Add Print String: `[WAVE] Wave <number> starting`, `[CORE] Core took damage — HP: <current>/<max>`, `[GAME] Game Over`

**Polish Criteria:** Core health bar displays correctly. Wave manager counts down correctly. Game over event fires when core is destroyed. No errors.

**Completion Notes:**
*(Claude Code fills this in when Phase 5 is complete)*

---

## PHASE 6 — Enemy AI and Spawning

**Goal:** Enemies spawn at the edges of the sandbox when a wave begins and walk toward the base core. If they reach a barricade or the player, they attack.

**Tasks:**
- [ ] Create BP_EnemyBase as a Character Blueprint using a simple humanoid mesh
- [ ] Create BP_EnemyAIController to drive BP_EnemyBase using Unreal's behavior tree system
- [ ] Create BT_EnemyBehavior (Behavior Tree) with a simple loop: move to core, if player nearby move to player and attack, if barricade in way attack barricade
- [ ] Create BB_Enemy (Blackboard) with keys: TargetActor, bCanSeePlayer, bAtCore
- [ ] Attach BP_HealthComponent to BP_EnemyBase
- [ ] Create BP_EnemySpawner as an Actor placed at each corner of the sandbox — it receives spawn commands from BP_WaveManager and spawns the correct number of BP_EnemyBase actors
- [ ] Enemies must navigate using the NavMesh — add a Nav Mesh Bounds Volume to the sandbox level
- [ ] Add Print String: `[ENEMY] Spawned`, `[ENEMY] Moving to core`, `[ENEMY] Attacking`, `[ENEMY] Dead`

**Polish Criteria:** Enemies spawn, navigate around obstacles, reach the core and attack it, die when health reaches zero. NavMesh covers the full sandbox. No pathing errors.

**Completion Notes:**
*(Claude Code fills this in when Phase 6 is complete)*

---

## PHASE 7 — Basic Combat

**Goal:** The player can attack enemies using a melee weapon crafted at the workbench. Hits deal damage using the health component system.

**Tasks:**
- [ ] Create BP_MeleeWeapon as an Actor Blueprint with a mesh, collision box, and damage value variable
- [ ] Add an equip system to BP_BastionCharacter — the player can equip a crafted weapon and it attaches to the hand socket
- [ ] Create a light attack input (left mouse button) that triggers a melee attack animation from GASP or a custom montage
- [ ] During the attack animation swing, enable the weapon collision box and call TakeDamage on any BP_EnemyBase it overlaps
- [ ] After the swing, disable the collision box
- [ ] Enemies also deal damage to the player when they attack — BP_BastionCharacter gets a BP_HealthComponent
- [ ] Add a player health bar to the HUD
- [ ] Create a simple hit react: enemies flash a different color for 0.2 seconds when hit
- [ ] Add Print String: `[COMBAT] Player attacked`, `[COMBAT] Hit enemy — Enemy HP: <hp>`, `[COMBAT] Player took damage — Player HP: <hp>`

**Polish Criteria:** Combat feels responsive. Weapons connect with enemies correctly. Damage numbers are correct. Player health depletes and game over fires if player dies. No ghost hits.

**Completion Notes:**
*(Claude Code fills this in when Phase 7 is complete)*

---

## PHASE 8 — Day Night Cycle

**Goal:** Time passes. The world transitions from day to night visually. Day phase gives the player time to gather and craft. Night phase starts the wave. Morning comes after all enemies are cleared.

**Tasks:**
- [ ] Create a day/night timeline in BP_WaveManager that drives the DirectionalLight rotation from sunrise angle to sunset angle using a Timeline node
- [ ] During Day Phase the DirectionalLight color is warm yellow-orange and the skylight is bright
- [ ] During Night Phase the DirectionalLight color is dim cold blue and the skylight is dark
- [ ] Day Phase duration: 90 seconds. Night Phase duration: lasts until all enemies in the wave are dead
- [ ] At end of Day Phase, BP_WaveManager fires OnNightBegin — this starts enemy spawning and dims the world
- [ ] At end of Night Phase, BP_WaveManager fires OnDayBegin — this brightens the world and starts the day timer again
- [ ] Show a large on-screen message "DAY 1", "NIGHT BEGINS", "WAVE CLEARED" using WBP_HUD at each transition
- [ ] Add Print String: `[TIME] Day phase started — <seconds> seconds`, `[TIME] Night phase started`

**Polish Criteria:** Visual transition between day and night is smooth and noticeable. Timing is correct. Transitions are clearly communicated on screen. No stacking timers or double-firing events.

**Completion Notes:**
*(Claude Code fills this in when Phase 8 is complete)*

---

## PHASE 9 — UI and Game States

**Goal:** The game has a proper HUD during gameplay, a simple Game Over screen, and a Win screen. A pause menu exists. Everything is navigable cleanly.

**Tasks:**
- [ ] Create WBP_HUD showing: player health bar, core health bar, current wave number, current day number, day/night timer, resource counts (Wood / Stone / Metal)
- [ ] Create WBP_GameOver showing: wave reached, enemies killed, a Restart button
- [ ] Create WBP_WaveCleared showing briefly after each night with a summary and a "Next Day" prompt
- [ ] Create a pause menu (Escape key) with Resume and Quit to Desktop options
- [ ] Wire all game state events from BP_WaveManager to show/hide the correct widgets
- [ ] Remove all Print String debug nodes from all Blueprints — or disable them using a global boolean bDebugMode that can be toggled from Project Settings
- [ ] Add ambient sound loop for day and night phases

**Polish Criteria:** HUD is readable and updates in real time. Game Over screen shows correct stats. Restart correctly resets all systems. No leftover debug prints visible during normal play.

**Completion Notes:**
*(Claude Code fills this in when Phase 9 is complete)*

---

## PHASE 10 — Polish and Portfolio Prep

**Goal:** The prototype is clean, stable, and showcasable. All rough edges are addressed.

**Tasks:**
- [ ] Replace placeholder meshes with proper assets where possible (free Epic Marketplace content is acceptable)
- [ ] Add particle effects for death, crafting, resource gathering, and core taking damage
- [ ] Add camera shake on hit received and on core taking damage
- [ ] Tune wave difficulty so the first 3 waves feel fair for a new player and wave 4-5 feel challenging
- [ ] Record a 2-3 minute gameplay video showing the full loop: gathering, crafting, surviving a night
- [ ] Write a short README for the portfolio entry describing the systems built and the tech used
- [ ] Take 4-6 screenshots at key moments for the portfolio
- [ ] Verify the project packages and runs as a standalone build without errors

**Polish Criteria:** A person who has never seen the project before can pick it up, understand the loop within 1 minute, and play for at least 3 rounds without encountering a blocking bug.

**Completion Notes:**
*(Claude Code fills this in when Phase 10 is complete)*

---

## Global Rules Claude Code Must Follow

These rules apply at all times across every phase.

Never modify assets outside the `Content/Bastion/` folder unless the GASP migration requires it, and even then document what was changed.

Every Blueprint function must have a comment node at its entry point explaining what the function does in one sentence.

Every phase must be fully working and polished before the next phase begins. No half-finished systems carried forward.

When the developer reports a bug, fix only that bug. Do not refactor unrelated systems during a bug fix.

If a system needs to be redesigned, note it in the Redesign Log below and explain why before touching anything.

Do not use Tick events for logic that can be driven by events or timers. Tick is only acceptable for the day/night light transition timeline.

---

## Redesign Log

*(Claude Code adds entries here when a system is redesigned mid-development)*

---

## Bug Log

*(Claude Code adds known bugs here and marks them resolved when fixed)*

---

*Document version: 1.4 -- Phase 2 sound polish complete*
*Last updated by: Salah Eddine Boussettah*

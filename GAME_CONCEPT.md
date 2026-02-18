# BASTION LAST STAND — Game Concept Document

**Genre:** Survival Base Defense
**Engine:** Unreal Engine 5
**Type:** Prototype / Portfolio Project
**Developer:** Salah Eddine Boussettah

---

## Overview

Bastion Last Stand is a third-person survival base defense game built in Unreal Engine 5. The player is the last defender of a crumbling outpost. During the day they scavenge the surrounding area for resources, craft tools, weapons, and fortifications at a workbench. When night falls, waves of enemies assault the outpost and the player must hold the line using everything they built. Each night gets harder. Each morning gives them one more chance to prepare.

The game is designed as a compact, polished prototype — every system is tight, every mechanic has a purpose, and the full experience can be demonstrated and understood within a few minutes. It is built to be shown in a portfolio as evidence of end-to-end game development capability.

---

## Core Loop

The game runs on a day/night cycle. During the **Day Phase** the player has a limited amount of time to move around the sandbox environment, gather resources from resource nodes scattered across the floor (wood, stone, metal scraps), and return to the workbench to craft items. During the **Night Phase** waves of enemies spawn from the edges of the map and walk toward the player's base core — a glowing object at the center of the outpost that must survive. If the core is destroyed, the game ends. If the player survives all waves of the night, a new day begins.

---

## The World

The game takes place in an abandoned industrial outpost. The playable area is a flat sandbox environment with four distinct surface zones laid out side by side — concrete, metal grating, wood planks, and gravel — each producing unique footstep sounds as the player walks across them. Resource nodes are distributed across these zones. The workbench sits at the edge of the play area. The core object sits at the center.

Everything is built and tested in this single sandbox level. There is no loading screen, no second level, no menus beyond a simple start and game over screen. The sandbox is the game.

---

## The Player

The player character uses the Unreal Engine Game Animation Sample Pack (GASP) locomotion system as its movement foundation, giving it smooth, responsive, motion-matched movement with natural acceleration, deceleration, and directional blending. The character can walk, run, crouch, and interact with objects in the world. Later phases may introduce a name and brief dialog lines to give the character personality.

---

## Crafting System

Crafting happens at the workbench. The player opens a crafting menu, selects a recipe, and if they have the required materials the item is created instantly or after a short animation. The crafting tree is organized in tiers — early items are cheap and basic, later items cost more and are significantly more powerful. The workbench itself is a craftable object in advanced phases, meaning the player could theoretically place multiple benches.

**Example Crafting Tree:**

Tier 1 — Wood Barricade, Stone Wall, Basic Melee Weapon
Tier 2 — Reinforced Barricade, Trap (spike or fire), Ranged Weapon
Tier 3 — Turret, Fortified Wall, Explosive Trap

---

## Enemy System

Enemies are simple bipedal characters that spawn at set locations around the perimeter of the sandbox and walk toward the core. They have health, a basic attack when they reach a barricade or the player, and a death reaction. Waves scale in enemy count and introduce tougher enemy variants in later rounds.

---

## Win and Lose Conditions

The player wins a run by surviving all planned night waves. The player loses if the core is destroyed. A game over screen shows the wave they reached, resources collected, and enemies defeated. A restart button returns them to Wave 1 Day 1.

---

## Atmosphere and Feel

The game should feel tense but empowering. Building defenses should feel satisfying. Movement should feel smooth and grounded thanks to GASP. Sound design — especially footstep surface detection — should make the world feel alive even in a sandbox. The visual language should be readable at a glance: warm tones during the day, cold blue tones at night, red danger indicators when enemies are near the core.

---

## Future Possibilities

This document describes the prototype scope. If development continues beyond the prototype, possibilities include a named protagonist with a backstory delivered through environmental storytelling, a second map with different surface types and geometry, a simple upgrade tree for the core itself, co-op multiplayer, and a proper main menu with a cutscene opening. None of these are in scope for the current build.

---

*Last updated: Phase 0 — Project Setup*

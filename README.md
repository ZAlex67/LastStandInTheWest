# Last Stand in the West

A fast-paced wave-based third-person shooter set in a Wild West environment.

🎮 **Play the game:**
https://zaagame.itch.io/last-stand-in-the-west

The player must survive increasingly difficult waves of enemies while managing ammo, switching weapons, and maintaining accuracy under pressure.

This project was created as a portfolio piece to demonstrate gameplay programming and system architecture in Unity.

## Gameplay

The player fights waves of bandits that become progressively more difficult.
Enemies spawn in waves, forcing the player to react quickly, manage ammunition, and switch weapons strategically.

Each wave increases the challenge and tests the player's reaction speed and shooting accuracy.

## Features

* Wave-based enemy spawning system
* Multiple weapon types
* Weapon switching system
* Ammo pickup system
* Score system
* Health system
* UI for health, ammo, and weapon slots
* Enemy wave progression

## Architecture

The project is built using a modular gameplay architecture where different systems communicate through events instead of direct references.

### Core Systems

**Player System**
* Handles player movement, shooting, and weapon switching.

**Weapon System**
* Responsible for weapon logic such as shooting, ammo usage, and weapon swapping.

**Wave Manager**
* Controls enemy spawning and wave progression.

**Health System**
* Reusable health component used by both player and enemies.

**UI System**
* Updates UI elements such as health, ammo, and score using event-driven updates.

### Performance

* Object pooling is used for frequently spawned objects such as enemies, ammo pickups, and effects in order to reduce runtime allocations and improve performance.

## Technical Systems

The project focuses on modular gameplay systems and clean architecture.

Key systems implemented in the project:

* Event-driven communication between systems
* Object pooling for performance optimization
* Modular weapon system
* Wave manager controlling enemy spawning
* Decoupled UI update logic
* Health and damage system

## Technologies

* Unity Engine
* C#
* Object Pooling
* Event-based architecture
* Clean code practices

## Controls

* WASD — Movement
* Mouse — Aim
* Left Mouse Button — Shoot
* 1 / 2 — Switch weapon

## Project Structure

The project is organized into several gameplay systems:

* Player systems
* Weapon systems
* Enemy wave management
* UI systems
* Pickup systems

Each system is designed to be modular and easily extendable.

## Project Goal

The goal of this project was to practice gameplay programming in Unity, focusing on clean code, system architecture, and performance-friendly solutions such as object pooling.

## License

MIT License

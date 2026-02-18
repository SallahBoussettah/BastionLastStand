# UnrealMCP - Complete Tool Reference

AI-powered control of Unreal Engine 5.6 through the Model Context Protocol.
**56 tools** across 10 categories for full editor automation.

---

## Table of Contents

1. [Blueprint Management](#1-blueprint-management)
2. [Blueprint Variables](#2-blueprint-variables)
3. [Blueprint Components](#3-blueprint-components)
4. [Blueprint Functions](#4-blueprint-functions)
5. [Node Graph Editing](#5-node-graph-editing)
6. [Actor Management](#6-actor-management)
7. [Property Inspection & Editing](#7-property-inspection--editing)
8. [Material System](#8-material-system)
9. [Level / Map Management](#9-level--map-management)
10. [Content Browser / Asset Operations](#10-content-browser--asset-operations)
11. [Viewport & Screenshots](#11-viewport--screenshots)
12. [Console & Logging](#12-console--logging)
13. [Play-in-Editor (PIE)](#13-play-in-editor-pie)
14. [Batch Operations](#14-batch-operations)

---

## 1. Blueprint Management

### `create_blueprint`
Create a new Blueprint class.

| Parameter      | Type   | Required | Default            | Description                                                                 |
|----------------|--------|----------|--------------------|-----------------------------------------------------------------------------|
| `name`         | string | Yes      | -                  | Blueprint name (e.g., `BP_MyActor`)                                         |
| `parent_class` | string | No       | `Actor`            | Parent class: `Actor`, `Pawn`, `Character`, `GameModeBase`, `PlayerController`, `ActorComponent`, `SceneComponent`, or any custom class |
| `path`         | string | No       | `/Game/Blueprints` | Content folder path                                                         |

### `list_blueprints`
List all Blueprint assets in a directory.

| Parameter   | Type    | Required | Default  | Description                    |
|-------------|---------|----------|----------|--------------------------------|
| `path`      | string  | No       | `/Game`  | Content folder path to search  |
| `recursive` | boolean | No       | `true`   | Search subdirectories          |

### `get_blueprint_info`
Get detailed info about a Blueprint: variables, functions, components, event graphs, parent class.

| Parameter    | Type   | Required | Description                                                        |
|--------------|--------|----------|--------------------------------------------------------------------|
| `asset_path` | string | Yes      | Full asset path (e.g., `/Game/Blueprints/BP_MyActor.BP_MyActor`)   |

### `compile_blueprint`
Compile a Blueprint and return detailed error/warning info with per-node error details (node_id, graph, message, severity, position).

| Parameter    | Type   | Required | Description              |
|--------------|--------|----------|--------------------------|
| `asset_path` | string | Yes      | Full asset path          |

**Returns:** `name`, `status` (Error/UpToDate/Dirty), `has_errors`, `error_count`, `warning_count`, `errors[]`, `warnings[]`

### `delete_blueprint`
Delete a Blueprint asset.

| Parameter    | Type   | Required | Description              |
|--------------|--------|----------|--------------------------|
| `asset_path` | string | Yes      | Full asset path          |

---

## 2. Blueprint Variables

### `add_blueprint_variable`
Add a member variable to a Blueprint.

| Parameter             | Type    | Required | Default   | Description                                                                              |
|-----------------------|---------|----------|-----------|------------------------------------------------------------------------------------------|
| `asset_path`          | string  | Yes      | -         | Blueprint asset path                                                                     |
| `variable_name`       | string  | Yes      | -         | Variable name                                                                            |
| `variable_type`       | string  | Yes      | -         | Type: `Boolean`, `Byte`, `Integer`, `Integer64`, `Float`, `Double`, `String`, `Text`, `Name`, `Vector`, `Rotator`, `Transform`, `Object`, `Class` |
| `default_value`       | string  | No       | `""`      | Default value as string                                                                  |
| `is_instance_editable`| boolean | No       | `true`    | Editable per-instance in Details panel                                                   |
| `category`            | string  | No       | `Default` | Category in Details panel                                                                |

### `remove_blueprint_variable`
Remove a member variable from a Blueprint.

| Parameter       | Type   | Required | Description        |
|-----------------|--------|----------|--------------------|
| `asset_path`    | string | Yes      | Blueprint path     |
| `variable_name` | string | Yes      | Variable to remove |

---

## 3. Blueprint Components

### `add_blueprint_component`
Add a component to a Blueprint's component hierarchy.

| Parameter          | Type       | Required | Default | Description                                                                  |
|--------------------|------------|----------|---------|------------------------------------------------------------------------------|
| `asset_path`       | string     | Yes      | -       | Blueprint path                                                               |
| `component_class`  | string     | Yes      | -       | e.g., `StaticMeshComponent`, `BoxCollisionComponent`, `PointLightComponent`  |
| `component_name`   | string     | Yes      | -       | Name for the component                                                       |
| `parent_component` | string     | No       | `""`    | Parent to attach to (empty = root)                                           |
| `location`         | [x, y, z]  | No       | null    | Relative location                                                            |
| `rotation`         | [p, y, r]  | No       | null    | Relative rotation                                                            |
| `scale`            | [x, y, z]  | No       | null    | Relative scale                                                               |

### `set_blueprint_component_defaults`
Set default property values on a Blueprint's component template (CDO). Sets properties on SCS templates so every spawned instance inherits the value.

| Parameter        | Type   | Required | Description                                                                                  |
|------------------|--------|----------|----------------------------------------------------------------------------------------------|
| `asset_path`     | string | Yes      | Blueprint path                                                                               |
| `component_name` | string | Yes      | Component variable name (e.g., `VisualMesh`, `DamageBox`)                                   |
| `property_name`  | string | Yes      | Property (e.g., `StaticMesh`, `CollisionProfileName`, `RelativeLocation`, `Intensity`)       |
| `property_value` | string | Yes      | Value in UE text format (e.g., `"/Engine/BasicShapes/Cube.Cube"`, `"(X=0,Y=0,Z=50)"`)       |

---

## 4. Blueprint Functions

### `create_function`
Create a new function in a Blueprint with typed inputs and outputs.

| Parameter       | Type   | Required | Default | Description                                                         |
|-----------------|--------|----------|---------|---------------------------------------------------------------------|
| `asset_path`    | string | Yes      | -       | Blueprint path                                                      |
| `function_name` | string | Yes      | -       | Function name                                                       |
| `inputs`        | array  | No       | null    | Input params: `[{"name": "Health", "type": "Float"}, ...]`          |
| `outputs`       | array  | No       | null    | Output params: `[{"name": "Success", "type": "Boolean"}, ...]`      |

**Returns:** Function graph name, entry/exit node IDs.

### `delete_function`
Delete a function from a Blueprint.

| Parameter       | Type   | Required | Description        |
|-----------------|--------|----------|--------------------|
| `asset_path`    | string | Yes      | Blueprint path     |
| `function_name` | string | Yes      | Function to delete |

---

## 5. Node Graph Editing

### `add_node`
Add a node to a Blueprint graph. **Supports 43 node types** organized into categories:

| Parameter       | Type       | Required | Default        | Description                              |
|-----------------|------------|----------|----------------|------------------------------------------|
| `asset_path`    | string     | Yes      | -              | Blueprint path                           |
| `graph_name`    | string     | No       | `EventGraph`   | Graph name (EventGraph, function, macro) |
| `node_type`     | string     | No       | `CallFunction` | Node type (see list below)               |
| `function_name` | string     | No       | `""`           | For CallFunction nodes                   |
| `target_class`  | string     | No       | `""`           | For CallFunction nodes                   |
| `node_position` | [x, y]     | No       | null           | Position in graph editor                 |
| `params`        | object     | No       | null           | Node-specific parameters                 |

#### Supported Node Types (43 total)

**Functions & Events (4):**
- `CallFunction` - Call any UFUNCTION (requires `function_name` + `target_class`)
- `Event` - Built-in event. params: `{"event_name": "ReceiveBeginPlay"}`. Common: `ReceiveBeginPlay`, `ReceiveTick`, `ReceiveDestroyed`, `ReceiveHit`
- `CustomEvent` - Custom event. params: `{"event_name": "MyEvent"}`
- `Self` - Self reference node

**Variables (2):**
- `VariableGet` - Get variable. params: `{"variable_name": "Health"}`
- `VariableSet` - Set variable. params: `{"variable_name": "Health"}`

**Flow Control (7):**
- `Branch` - If/else
- `Sequence` - Execution sequence
- `MultiGate` - Multiple execution outputs
- `Select` - Select value by index
- `DoOnceMultiInput` - Multi-input do once
- `MacroInstance` - Standard macro. params: `{"macro_name": "ForLoop"}`. Available: `ForLoop`, `ForLoopWithBreak`, `WhileLoop`, `DoOnce`, `DoN`, `Gate`, `FlipFlop`, `ForEachLoop`, `ForEachLoopWithBreak`, `IsValid`
- `ForEachElementInEnum` - Loop enum values. params: `{"enum_name": "ECollisionChannel"}`

**Switch (4):**
- `SwitchInteger`, `SwitchString`, `SwitchName` - Switch on type
- `SwitchEnum` - Switch on enum. params: `{"enum_name": "ECollisionChannel"}`

**Casting (2):**
- `DynamicCast` - Cast To. params: `{"target_class": "Character"}`
- `ClassDynamicCast` - Class cast. params: `{"target_class": "Character"}`

**Structs (3):**
- `MakeStruct` - params: `{"struct_type": "Vector"}`. Common: `Vector`, `Rotator`, `Transform`, `LinearColor`, `Vector2D`, `HitResult`
- `BreakStruct` - params: `{"struct_type": "Vector"}`
- `SetFieldsInStruct` - params: `{"struct_type": "Vector"}`

**Containers (4):**
- `MakeArray` - Optional: `{"num_inputs": 3}`
- `MakeMap`, `MakeSet`, `GetArrayItem`

**Spawning & Objects (3):**
- `SpawnActorFromClass`, `GenericCreateObject`, `AddComponentByClass`

**Delegates (5):**
- `CreateDelegate`, `AddDelegate`, `RemoveDelegate`, `CallDelegate`, `ClearDelegate` - params: `{"delegate_name": "OnDamage"}`

**Text & Enums (2):**
- `FormatText`, `EnumLiteral` - params: `{"enum_name": "ECollisionChannel"}`

**Misc (7):**
- `Timeline` - params: `{"timeline_name": "MyTimeline"}`
- `Knot` - Reroute node
- `LoadAsset` - Async load
- `EaseFunction` - Interpolation
- `GetClassDefaults`, `GetDataTableRow`
- `CommutativeAssociativeBinaryOperator` - Expandable math (e.g., `Add_FloatFloat` on `KismetMathLibrary`)

### `get_graph_nodes`
Get all nodes in a Blueprint graph with pins and connections.

| Parameter    | Type   | Required | Default      | Description  |
|--------------|--------|----------|--------------|--------------|
| `asset_path` | string | Yes      | -            | Blueprint path |
| `graph_name` | string | No       | `EventGraph` | Graph name   |

### `connect_pins`
Connect two pins between nodes.

| Parameter         | Type   | Required | Default      | Description             |
|-------------------|--------|----------|--------------|-------------------------|
| `asset_path`      | string | Yes      | -            | Blueprint path          |
| `source_node_id`  | string | Yes      | -            | Output node ID          |
| `source_pin_name` | string | Yes      | -            | Output pin name         |
| `target_node_id`  | string | Yes      | -            | Input node ID           |
| `target_pin_name` | string | Yes      | -            | Input pin name          |
| `graph_name`      | string | No       | `EventGraph` | Graph name              |

### `disconnect_pins`
Disconnect all connections from a specific pin.

| Parameter    | Type   | Required | Default      | Description    |
|--------------|--------|----------|--------------|----------------|
| `asset_path` | string | Yes      | -            | Blueprint path |
| `node_id`    | string | Yes      | -            | Node ID        |
| `pin_name`   | string | Yes      | -            | Pin to disconnect |
| `graph_name` | string | No       | `EventGraph` | Graph name     |

### `set_pin_value`
Set the default value of a pin on a node.

| Parameter    | Type   | Required | Default      | Description                    |
|--------------|--------|----------|--------------|--------------------------------|
| `asset_path` | string | Yes      | -            | Blueprint path                 |
| `node_id`    | string | Yes      | -            | Node ID                        |
| `pin_name`   | string | Yes      | -            | Pin name                       |
| `value`      | string | Yes      | -            | Value as string                |
| `graph_name` | string | No       | `EventGraph` | Graph name                     |

### `delete_node`
Delete a node from a Blueprint graph.

| Parameter    | Type   | Required | Default      | Description    |
|--------------|--------|----------|--------------|----------------|
| `asset_path` | string | Yes      | -            | Blueprint path |
| `node_id`    | string | Yes      | -            | Node to delete |
| `graph_name` | string | No       | `EventGraph` | Graph name     |

---

## 6. Actor Management

### `spawn_actor`
Spawn an actor in the current level.

| Parameter        | Type          | Required | Default           | Description                                   |
|------------------|---------------|----------|-------------------|-----------------------------------------------|
| `actor_class`    | string        | No       | `StaticMeshActor` | Class (e.g., `PointLight`, `CameraActor`). Ignored if `blueprint_path` set |
| `name`           | string        | No       | `""`              | Actor label                                   |
| `location`       | [x, y, z]     | No       | [0, 0, 0]         | World location                                |
| `rotation`       | [p, y, r]     | No       | [0, 0, 0]         | World rotation (degrees)                      |
| `scale`          | [x, y, z]     | No       | [1, 1, 1]         | World scale                                   |
| `blueprint_path` | string        | No       | `""`              | Spawn Blueprint instance instead of class     |

### `delete_actor`
Delete an actor from the current level.

| Parameter    | Type   | Required | Description                          |
|--------------|--------|----------|--------------------------------------|
| `actor_name` | string | Yes      | Name as shown in World Outliner      |

### `set_actor_transform`
Set position, rotation, and/or scale of an actor. Pass `null` for any to keep unchanged.

| Parameter    | Type      | Required | Default | Description                   |
|--------------|-----------|----------|---------|-------------------------------|
| `actor_name` | string    | Yes      | -       | Target actor                  |
| `location`   | [x, y, z] | No       | null    | New world location            |
| `rotation`   | [p, y, r] | No       | null    | New world rotation (degrees)  |
| `scale`      | [x, y, z] | No       | null    | New world scale               |

### `get_actors_in_level`
List all actors in the current level (World Outliner). Supports filtering.

| Parameter      | Type   | Required | Default | Description                                |
|----------------|--------|----------|---------|--------------------------------------------|
| `class_filter` | string | No       | `""`    | Filter by class (e.g., `StaticMeshActor`)  |
| `name_filter`  | string | No       | `""`    | Filter by name substring                   |
| `tag_filter`   | string | No       | `""`    | Filter by actor tag                        |

### `find_actors`
Search for actors by name in the current level.

| Parameter | Type   | Required | Description                    |
|-----------|--------|----------|--------------------------------|
| `query`   | string | Yes      | Actor name or partial name     |

### `duplicate_actor`
Duplicate an actor in the level.

| Parameter         | Type       | Required | Default | Description                    |
|-------------------|------------|----------|---------|--------------------------------|
| `actor_name`      | string     | Yes      | -       | Actor to duplicate             |
| `new_name`        | string     | No       | `""`    | Name for the duplicate         |
| `location_offset` | [x, y, z]  | No       | null    | Offset from original position  |

---

## 7. Property Inspection & Editing

### `get_object_properties`
Get all properties of a UObject (equivalent to the Details Panel).

| Parameter           | Type    | Required | Default | Description                                              |
|---------------------|---------|----------|---------|----------------------------------------------------------|
| `object_path`       | string  | Yes      | -       | Actor name or asset path                                 |
| `category_filter`   | string  | No       | `""`    | Filter by category                                       |
| `include_inherited` | boolean | No       | `true`  | Include parent class properties                          |

### `set_object_property`
Set a property on a UObject (triggers PreEditChange/PostEditChange).

| Parameter        | Type   | Required | Description                                                      |
|------------------|--------|----------|------------------------------------------------------------------|
| `object_path`    | string | Yes      | Actor name or asset path                                         |
| `property_name`  | string | Yes      | Property name (supports dot-notation: `RelativeLocation.X`)      |
| `property_value` | string | Yes      | New value as string                                              |

### `set_component_property`
Set a property on a specific component of an actor.

| Parameter        | Type   | Required | Description         |
|------------------|--------|----------|---------------------|
| `actor_name`     | string | Yes      | Actor name          |
| `component_name` | string | Yes      | Component name      |
| `property_name`  | string | Yes      | Property to modify  |
| `property_value` | string | Yes      | New value as string |

### `get_component_hierarchy`
Get the full component tree of an actor with types, properties, and children.

| Parameter    | Type   | Required | Description     |
|--------------|--------|----------|-----------------|
| `actor_name` | string | Yes      | Actor to inspect |

### `get_class_defaults`
Get the Class Default Object (CDO) properties for any class.

| Parameter    | Type   | Required | Description                                    |
|--------------|--------|----------|------------------------------------------------|
| `class_name` | string | Yes      | Class name or Blueprint path                   |

---

## 8. Material System

### `create_material`
Create a new Material asset with initial PBR values.

| Parameter    | Type       | Required | Default           | Description               |
|--------------|------------|----------|-------------------|---------------------------|
| `name`       | string     | Yes      | -                 | Material name (e.g., `M_Rock`) |
| `path`       | string     | No       | `/Game/Materials` | Content folder path       |
| `base_color` | [r, g, b]  | No       | [0.8, 0.8, 0.8]   | RGB (0.0-1.0)             |
| `roughness`  | float      | No       | 0.5               | Roughness (0.0-1.0)       |
| `metallic`   | float      | No       | 0.0               | Metallic (0.0-1.0)        |

### `assign_material`
Assign a material to a mesh component on an actor.

| Parameter        | Type    | Required | Default | Description                    |
|------------------|---------|----------|---------|--------------------------------|
| `actor_name`     | string  | Yes      | -       | Target actor                   |
| `material_path`  | string  | Yes      | -       | Material asset path            |
| `slot`           | integer | No       | 0       | Material slot index            |
| `component_name` | string  | No       | `""`    | Component (empty = first mesh) |

### `modify_material`
Modify an existing material or material instance.

| Parameter       | Type       | Required | Default | Description                                                |
|-----------------|------------|----------|---------|------------------------------------------------------------|
| `asset_path`    | string     | Yes      | -       | Material asset path                                        |
| `base_color`    | [r, g, b]  | No       | null    | New base color (base materials only)                       |
| `roughness`     | float      | No       | null    | New roughness                                              |
| `metallic`      | float      | No       | null    | New metallic                                               |
| `scalar_params` | object     | No       | null    | Scalar overrides for instances: `{"Roughness": 0.3}`       |
| `vector_params` | object     | No       | null    | Vector overrides for instances: `{"BaseColor": [1,0,0,1]}` |

### `get_material_info`
Get detailed material info: parameters, expression count, parent material.

| Parameter    | Type   | Required | Description        |
|--------------|--------|----------|--------------------|
| `asset_path` | string | Yes      | Material asset path |

---

## 9. Level / Map Management

### `get_level_info`
Get current world info: map name, persistent level, streaming sub-levels (with visibility, loaded state, actor count, transform).

*No parameters.*

### `create_level`
Create a new blank map or from a template.

| Parameter       | Type    | Required | Default | Description                                    |
|-----------------|---------|----------|---------|------------------------------------------------|
| `save_path`     | string  | No       | `""`    | Path to save (e.g., `/Game/Maps/NewLevel`)     |
| `template_path` | string  | No       | `""`    | Template map path (empty = blank)              |
| `save_existing` | boolean | No       | `true`  | Save current map before creating               |

### `load_level`
Open an existing map in the editor.

| Parameter       | Type    | Required | Default | Description                         |
|-----------------|---------|----------|---------|-------------------------------------|
| `map_path`      | string  | Yes      | -       | Asset path (e.g., `/Game/Maps/MyLevel`) |
| `save_existing` | boolean | No       | `true`  | Save current map before loading     |

### `save_level`
Save the current map, save-as, or save all dirty packages.

| Parameter    | Type    | Required | Default | Description                                  |
|--------------|---------|----------|---------|----------------------------------------------|
| `asset_path` | string  | No       | `""`    | Save-as path (empty = save in place)         |
| `save_all`   | boolean | No       | `false` | Save all dirty map and content packages      |

### `add_streaming_level`
Add a streaming sub-level (existing or create new).

| Parameter         | Type       | Required | Default    | Description                                     |
|-------------------|------------|----------|------------|-------------------------------------------------|
| `package_name`    | string     | Yes      | -          | Level path (e.g., `/Game/Maps/SubLevel1`)       |
| `streaming_class` | string     | No       | `Dynamic`  | `Dynamic` or `AlwaysLoaded`                     |
| `location`        | [x, y, z]  | No       | null       | World offset                                    |
| `rotation`        | [p, y, r]  | No       | null       | Rotation                                        |
| `create_new`      | boolean    | No       | `false`    | Create new empty level instead of referencing    |

### `remove_streaming_level`
Remove a streaming sub-level from the world (does not delete the asset on disk).

| Parameter      | Type   | Required | Description          |
|----------------|--------|----------|----------------------|
| `package_name` | string | Yes      | Level package name   |

### `set_level_visibility`
Show/hide a streaming sub-level and optionally make it the current editing level.

| Parameter      | Type    | Required | Default | Description                                       |
|----------------|---------|----------|---------|---------------------------------------------------|
| `package_name` | string  | Yes      | -       | Level package name                                |
| `visible`      | boolean | No       | `true`  | Visibility                                        |
| `make_current` | boolean | No       | `false` | Make this the current level for editing            |

---

## 10. Content Browser / Asset Operations

### `search_assets`
Search the Content Browser by type, path, and/or name pattern.

| Parameter      | Type    | Required | Default | Description                                                                                       |
|----------------|---------|----------|---------|---------------------------------------------------------------------------------------------------|
| `path`         | string  | No       | `""`    | Content path (e.g., `/Game/Meshes`)                                                               |
| `type`         | string  | No       | `""`    | Asset type: `StaticMesh`, `SkeletalMesh`, `Texture2D`, `Material`, `MaterialInstanceConstant`, `SoundWave`, `Blueprint`, `World`, `AnimSequence`, `ParticleSystem` |
| `name_pattern` | string  | No       | `""`    | Case-insensitive substring match                                                                  |
| `recursive`    | boolean | No       | `true`  | Search subdirectories                                                                             |
| `max_results`  | integer | No       | 100     | Max results                                                                                       |

### `get_asset_info`
Get detailed metadata for an asset: package info, class, disk size, dirty state, referencers, dependencies.

| Parameter    | Type   | Required | Description     |
|--------------|--------|----------|-----------------|
| `asset_path` | string | Yes      | Full asset path |

### `import_asset`
Import external files (FBX, OBJ, PNG, JPG, WAV, etc.) into the project.

| Parameter          | Type   | Required | Default | Description                                                      |
|--------------------|--------|----------|---------|------------------------------------------------------------------|
| `file_path`        | string | Yes      | -       | Absolute path on disk (e.g., `C:/Assets/chair.fbx`)             |
| `destination_path` | string | Yes      | -       | Content Browser path (e.g., `/Game/Meshes`)                     |
| `asset_name`       | string | No       | `""`    | Custom name (empty = use filename)                               |
| `options`          | object | No       | null    | `{replace_existing, import_materials, generate_collision}` (all bool) |

### `delete_asset`
Delete an asset with reference checking. Refuses if referenced unless forced.

| Parameter    | Type    | Required | Default | Description                                 |
|--------------|---------|----------|---------|---------------------------------------------|
| `asset_path` | string  | Yes      | -       | Asset to delete                             |
| `force`      | boolean | No       | `false` | Delete even if referenced (creates redirectors) |

### `rename_asset`
Rename or move an asset. Creates redirectors automatically.

| Parameter    | Type   | Required | Description                                            |
|--------------|--------|----------|--------------------------------------------------------|
| `asset_path` | string | Yes      | Current path (e.g., `/Game/Meshes/OldName`)            |
| `new_path`   | string | Yes      | New path (e.g., `/Game/Props/Chair` to move + rename)  |

---

## 11. Viewport & Screenshots

### `take_screenshot`
Capture the editor viewport as PNG.

| Parameter  | Type    | Required | Default | Description                             |
|------------|---------|----------|---------|-----------------------------------------|
| `width`    | integer | No       | 1280    | Width in pixels                         |
| `height`   | integer | No       | 720     | Height in pixels                        |
| `filename` | string  | No       | `""`    | Custom filename (empty = timestamp)     |

### `focus_viewport`
Focus the editor viewport on a target actor or location.

| Parameter  | Type       | Required | Default | Description                                 |
|------------|------------|----------|---------|---------------------------------------------|
| `target`   | string     | No       | `""`    | Actor name to focus on (priority over location) |
| `location` | [x, y, z]  | No       | null    | World location (used if target is empty)    |
| `rotation` | [p, y, r]  | No       | null    | Camera rotation                             |
| `distance` | float      | No       | 500     | Distance from target                        |

---

## 12. Console & Logging

### `execute_console_command`
Execute a console command in the editor or in a running PIE session.

| Parameter | Type   | Required | Default | Description                                          |
|-----------|--------|----------|---------|------------------------------------------------------|
| `command` | string | Yes      | -       | Console command (e.g., `stat fps`, `obj list`)       |
| `target`  | string | No       | `""`    | `""` or `"editor"` for editor, `"pie"` for PIE world |

### `get_console_logs`
Get recent console log messages with filtering.

| Parameter          | Type    | Required | Default | Description                                                      |
|--------------------|---------|----------|---------|------------------------------------------------------------------|
| `count`            | integer | No       | 50      | Max messages                                                     |
| `verbosity_filter` | string  | No       | `""`    | `""` (all), `Error`, `Warning`, `Display`, `Log`                 |
| `category_filter`  | string  | No       | `""`    | e.g., `LogBlueprint`, `LogTemp`, `LogCompile`                    |

---

## 13. Play-in-Editor (PIE)

### `start_pie`
Start a Play-in-Editor session.

| Parameter | Type   | Required | Default | Description                                                       |
|-----------|--------|----------|---------|-------------------------------------------------------------------|
| `mode`    | string | No       | `""`    | `""` (last settings), `viewport`, `new_window`, `simulate`        |

### `stop_pie`
Stop the current PIE session.

*No parameters.*

### `get_pie_status`
Get current PIE session status: `is_running`, `is_paused`, `is_simulating`, `world_name`, `player_count`.

*No parameters.*

### `set_pie_paused`
Pause or resume the current PIE session.

| Parameter | Type    | Required | Description                |
|-----------|---------|----------|----------------------------|
| `paused`  | boolean | Yes      | `true` to pause, `false` to resume |

---

## 14. Batch Operations

These tools combine multiple operations into a single TCP round-trip for maximum efficiency.

### `batch_execute`
Execute multiple arbitrary commands sequentially in one call.

| Parameter       | Type    | Required | Default | Description                                                |
|-----------------|---------|----------|---------|------------------------------------------------------------|
| `commands`      | array   | Yes      | -       | List of `{"command": "tool_name", "params": {...}}` objects |
| `stop_on_error` | boolean | No       | `false` | Stop on first failure                                      |

**Example:**
```json
[
  {"command": "spawn_actor", "params": {"actor_class": "PointLight", "location": [0, 0, 300]}},
  {"command": "spawn_actor", "params": {"actor_class": "PointLight", "location": [200, 0, 300]}}
]
```

### `batch_add_nodes`
Add multiple nodes to a Blueprint graph in one call.

| Parameter       | Type    | Required | Default      | Description                                              |
|-----------------|---------|----------|--------------|----------------------------------------------------------|
| `asset_path`    | string  | Yes      | -            | Blueprint path                                           |
| `nodes`         | array   | Yes      | -            | List of node definitions (same params as `add_node`)     |
| `graph_name`    | string  | No       | `EventGraph` | Target graph                                             |
| `stop_on_error` | boolean | No       | `true`       | Stop on first failure                                    |

**Returns:** `node_ids[]` list for use with `batch_pin_operations`.

### `batch_pin_operations`
Connect pins and set pin values in one call. Connections processed first, then values.

| Parameter       | Type    | Required | Default      | Description                                                                        |
|-----------------|---------|----------|--------------|------------------------------------------------------------------------------------|
| `asset_path`    | string  | Yes      | -            | Blueprint path                                                                     |
| `connections`   | array   | No       | null         | `[{"source_node_id", "source_pin", "target_node_id", "target_pin"}, ...]`          |
| `pin_values`    | array   | No       | null         | `[{"node_id", "pin_name", "value"}, ...]`                                          |
| `graph_name`    | string  | No       | `EventGraph` | Target graph                                                                       |
| `stop_on_error` | boolean | No       | `false`      | Stop on first failure                                                              |

### `batch_spawn_actors`
Spawn multiple actors in one call.

| Parameter       | Type    | Required | Default | Description                                                         |
|-----------------|---------|----------|---------|---------------------------------------------------------------------|
| `actors`        | array   | Yes      | -       | List of actor defs (same params as `spawn_actor`)                   |
| `stop_on_error` | boolean | No       | `false` | Stop on first failure                                               |

**Example:**
```json
[
  {"blueprint_path": "/Game/BP_Door", "name": "Door1", "location": [0, 0, 0]},
  {"blueprint_path": "/Game/BP_Door", "name": "Door2", "location": [500, 0, 0]},
  {"actor_class": "PointLight", "location": [250, 0, 300]}
]
```

---

## Quick Workflow Examples

### Create a Blueprint with logic
```
1. create_blueprint("BP_Enemy", parent_class="Character")
2. add_blueprint_variable(asset_path, "Health", "Float", default_value="100")
3. add_blueprint_component(asset_path, "StaticMeshComponent", "VisualMesh")
4. batch_add_nodes(asset_path, [BeginPlay event, PrintString node])
5. batch_pin_operations(asset_path, connections=[...], pin_values=[...])
6. compile_blueprint(asset_path)
```

### Build a scene
```
1. batch_spawn_actors([lights, meshes, blueprint instances])
2. set_actor_transform(actor, location, rotation, scale)
3. create_material("M_Floor", base_color=[0.3, 0.2, 0.1])
4. assign_material("FloorMesh", "/Game/Materials/M_Floor.M_Floor")
5. take_screenshot()
```

### Test gameplay
```
1. start_pie(mode="viewport")
2. get_pie_status()
3. execute_console_command("stat fps", target="pie")
4. get_console_logs(verbosity_filter="Error")
5. set_pie_paused(true)
6. stop_pie()
```

# MCP Manual Tasks - Door Interaction System

Things MCP could not create or handle that need manual verification/fixing in the editor.

## Blueprint Interface Limitations
- **Cannot create Blueprint Interfaces** - BPI_Interactable had to be created manually (Right-click > Blueprints > Blueprint Interface)
- **Cannot add function parameters to existing functions** - Caller input on Interact had to be added manually

## Variable Type Issues
- **Cannot create Object Reference variables** - `current_interactable` (Actor ref) had to be added manually in BP_BastionCharacter
- **Float variables created as Integer** - OpenAngle and open_direction in BP_Door were created as int32 despite specifying Float. ToFloat conversion nodes were auto-added as a workaround. Consider deleting these variables and recreating them manually as Float type, then re-dragging fresh Get nodes.
- **Variable defaults may not apply** - bCanInteract defaulted to False instead of True, OpenAngle to 0 instead of 90, open_direction to 0 instead of 1. RESOLVED: CDO defaults can be set via `set_object_property` using the path format `Default__BP_Door_C` (e.g., `/Game/Bastion/Blueprints/Interactables/BP_Door.Default__BP_Door_C`)

## Node Types MCP Cannot Create
- **Enhanced Input Action events** - EnhancedInputAction IA_Interact had to be added manually
- **Interface Message calls** - "Interact (Message)" node targeting BPI_Interactable had to be added manually
- **OnComponentBeginOverlap / OnComponentEndOverlap** delegate bindings had to be added manually
- **Blueprint Interface implementation** - Adding BPI_Interactable to BP_Door's Class Settings > Interfaces had to be done manually
- **Event Interact (from interface)** - The interface event node had to be added manually

## Component Properties
- **GenerateOverlapEvents** - Verify InteractionBox has this enabled (was set via MCP but verify)
- **CollisionProfileName** - InteractionBox set to "OverlapAllDynamic" via MCP, verify it stuck

## Things to Verify in Editor

### BP_Door
- [ ] Open BP_Door, check variable defaults: bCanInteract=True, OpenAngle=90.0, open_direction=1.0
- [ ] Check InteractionBox collision: Profile = OverlapAllDynamic, Generate Overlap Events = checked
- [ ] Check DoorTimeline: has DoorAlpha float track with keyframes (0.0, 0.0) and (0.6, 1.0)
- [ ] Verify Event Interact (BPI_Interactable) is connected to first Branch node
- [ ] Verify InteractionBox box extent is large enough (200x200x200)

### BP_BastionCharacter
- [ ] Verify IA_Interact is mapped to E key in IMC_Sandbox input mapping context
- [ ] Verify IMC_Sandbox is added to the player controller or character at BeginPlay
- [ ] Verify EnhancedInputAction IA_Interact Triggered -> IsValid -> Interact (Message)

### Input Mapping
- [ ] Open IMC_Sandbox, verify IA_Interact action is mapped to keyboard E key
- [ ] Verify IMC_Sandbox is being added to the Enhanced Input Local Player Subsystem on BeginPlay (check CBP_SandboxCharacter parent blueprint)

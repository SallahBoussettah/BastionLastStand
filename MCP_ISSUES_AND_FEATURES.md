# UnrealMCP - Issues Found & Feature Requests

This document logs all MCP tool issues discovered during development of Bastion Last Stand (Phases 2.5 through 3.5), along with suggested features that would have significantly improved the workflow.

---

## Part 1: Issues & Bugs Found

### 1. `set_object_property` Does Not Persist Blueprint Instance Variables
**Severity:** Critical
**Context:** When using `set_object_property` on placed Blueprint instances to change per-instance variable values (e.g., setting `resource_type` from "Wood" to "Stone" on a BP_ResourceNode instance), the tool reports success with correct old/new values, but the change does not actually persist. Reading the property back immediately returns the original default value. This affects both String and Integer Blueprint variables. The tool appears to modify a transient representation rather than the actual serialized level data.

**Workaround:** Added auto-detection logic in BeginPlay that parses the actor's display name to determine variable values at runtime (e.g., "ResourceNode_Stone1" -> Contains("Stone") -> Set resource_type = "Stone").

**Expected Behavior:** `set_object_property` should persistently modify per-instance Blueprint variable overrides on placed actors, just like editing the value in the Details panel.

---

### 2. `set_blueprint_component_defaults` (SCS Template) Changes Don't Propagate to Existing Instances
**Severity:** High
**Context:** Setting collision properties (CollisionProfileName, BodyInstance.CollisionEnabled) on a Blueprint's SCS component template via `set_blueprint_component_defaults`, then compiling, does not update already-placed instances in the level. Newly spawned instances inherit the changes, but existing ones retain old defaults.

**Workaround:** Deleted all 9 existing instances and re-spawned them fresh via `batch_spawn_actors` after the SCS template was updated and compiled.

**Expected Behavior:** After compile, existing instances that don't have per-instance overrides should inherit the new SCS template values (this is standard UE behavior in the editor).

---

### 3. `set_component_property` Cannot Find Collision Properties
**Severity:** Medium
**Context:** Attempting to set `CollisionProfileName` or `BodyInstance.CollisionProfileName` on instance components via `set_component_property` returns "Property not found." Collision-related properties are nested inside BodyInstance and may require special handling.

**Workaround:** Could not fix collision per-instance. Had to delete and re-spawn instances.

**Expected Behavior:** Collision properties should be settable via dot-notation paths like `BodyInstance.CollisionProfileName`.

---

### 4. `batch_add_nodes` Returns "Unknown Error" But May Partially Execute
**Severity:** Medium
**Context:** Calling `batch_add_nodes` with 7 nodes returned "Unknown error" with no further details. In some previous cases (per MEMORY.md notes), batch operations return this error but still execute. In this particular case, the nodes were NOT created, requiring individual `add_node` calls as fallback.

**Workaround:** Verify with `get_graph_nodes` after any batch operation. Fall back to individual calls if batch fails.

**Expected Behavior:** Batch operations should return clear success/failure per-operation, or at minimum a descriptive error message.

---

### 5. Cannot Remove Blueprint Components
**Severity:** Medium
**Context:** There is no `remove_blueprint_component` tool. After adding a BoxComponent (BlockingCollision) to BP_ResourceNode that turned out to be unnecessary, it could not be removed via MCP. Had to disable it by setting NoCollision and shrinking to 1x1x1 size.

**Workaround:** Set the unwanted component to NoCollision with minimal size, effectively disabling it.

**Expected Behavior:** A `remove_blueprint_component` tool should exist to delete components from the SCS hierarchy.

---

### 6. Cannot Create EnhancedInputAction Event Nodes
**Severity:** High
**Context:** MCP's `add_node` with `node_type: "Event"` creates a generic K2Node_Event instead of K2Node_EnhancedInputAction. The resulting node has wrong pins and doesn't respond to input. This is a known limitation but remains a significant blocker for any input-related Blueprint work.

**Workaround:** User must manually add EnhancedInputAction event nodes in the Blueprint editor.

---

### 7. Cannot Create Property Setter Nodes (e.g., Set bShowMouseCursor)
**Severity:** Medium
**Context:** `bShowMouseCursor` on PlayerController is a property, not a UFUNCTION. MCP's `add_node` with CallFunction cannot create property setter nodes (K2Node_VariableSet targeting external objects). Only functions exposed as UFUNCTION can be called.

**Workaround:** User manually adds "Set Show Mouse Cursor" nodes.

**Expected Behavior:** Support for creating property getter/setter nodes on external objects (equivalent to dragging a variable and choosing "Set" in the Blueprint editor).

---

### 8. Sequence Node Only Creates 2 Output Pins
**Severity:** Low
**Context:** When creating a Sequence node via `add_node`, it only has `then_0` and `then_1` pins. There is no way to add more output pins (then_2, then_3, etc.) via MCP.

**Workaround:** Chain the last Sequence output into additional nodes via exec pin connections.

**Expected Behavior:** Either auto-expand when more connections are made, or provide a parameter to specify the desired number of output pins.

---

### 9. `set_widget_property` Cannot Set Border BrushColor
**Severity:** Low
**Context:** Border widgets have a BrushColor property that controls their background color. `set_widget_property` does not support this property, so Border backgrounds remain transparent/default.

**Workaround:** User must set BrushColor manually in the Widget editor.

**Expected Behavior:** Add BrushColor to the supported properties list for Border widgets.

---

### 10. Widget RootPanel Visibility Causes Hidden Content
**Severity:** Low (Design footgun)
**Context:** Setting `Visibility = Collapsed` on the root CanvasPanel inside a widget means the entire widget content is hidden. If the toggle logic then sets visibility on the UserWidget itself (the parent), the root panel inside stays Collapsed, so nothing renders even when the widget is "visible." The fix was to keep the root panel at SelfHitTestInvisible and control visibility at the UserWidget level, with an explicit SetVisibility(Collapsed) call after AddToViewport.

**Not a bug per se,** but worth noting that widget visibility operates at multiple levels and the interaction between UserWidget visibility and child widget visibility can be confusing.

---

### 11. `GetActorLabel` Causes Compile Error (Editor-Only)
**Severity:** Low
**Context:** `GetActorLabel` is an editor-only function. Using it in a Blueprint that runs at runtime causes a compile error (has_errors=True but error_count=0, no error message shown).

**Workaround:** Used `GetDisplayName` from KismetSystemLibrary instead, which works at runtime.

---

### 12. Create Widget Node WidgetType Pin Not Auto-Set
**Severity:** Medium
**Context:** When creating a "Create Widget" node via `add_node`, the `WidgetType` class pin defaults to empty. The pin value must be explicitly set via `set_pin_value` with the full class path including `_C` suffix (e.g., `/Game/Path/WBP_InventoryPanel.WBP_InventoryPanel_C`). Without this, the widget creation silently fails at runtime (returns null).

**Expected Behavior:** Either require the class path as a parameter when creating the node, or surface a warning when the pin is empty.

---

## Part 2: Feature Requests

### 1. Read Component Properties Tool
**Priority:** High
A `get_component_properties` tool that reads all properties from a specific component on an actor instance (including collision settings, mesh reference, material assignments). Currently there is no way to inspect component-level properties on placed actors, only actor-level properties via `get_object_properties`.

### 2. Remove Blueprint Component Tool
**Priority:** High
`remove_blueprint_component` to delete components from a Blueprint's SCS hierarchy. Currently components can only be added, never removed.

### 3. Per-Instance Variable Override Tool
**Priority:** High
A dedicated tool to set Blueprint variable overrides on placed instances, equivalent to editing the value in the Details panel. This should handle the proper serialization that `set_object_property` currently fails to do.

### 4. EnhancedInputAction Event Node Support
**Priority:** High
Native support for creating K2Node_EnhancedInputAction nodes, with a parameter for the InputAction asset reference. This is needed for any input-driven Blueprint logic.

### 5. Property Setter/Getter Node Support
**Priority:** Medium
Support for creating K2Node_VariableSet and K2Node_VariableGet nodes targeting external object properties (e.g., `Set bShowMouseCursor` on PlayerController). This would eliminate a common class of manual steps.

### 6. Sequence Node Pin Count Parameter
**Priority:** Low
Allow specifying the number of output pins when creating a Sequence node, or automatically expand when new connections are made.

### 7. Border Widget BrushColor Support
**Priority:** Low
Add `BrushColor` to the list of supported properties in `set_widget_property` for Border widgets.

### 8. Blueprint Interface Creation Tool
**Priority:** Medium
`create_blueprint_interface` to create Blueprint Interfaces with function signatures. Currently interfaces must be created manually.

### 9. Enum and Struct Creation Tools
**Priority:** Medium
Tools to create UEnum and UStruct assets from MCP, which would eliminate the need for string-based workarounds and separate counter variables.

### 10. Batch Operation Error Reporting
**Priority:** Medium
When `batch_add_nodes` or `batch_pin_operations` encounter errors, the error message should include the specific operation index and a descriptive error, not just "Unknown error."

---

*Document created: 2026-02-19*
*Project: Bastion Last Stand*

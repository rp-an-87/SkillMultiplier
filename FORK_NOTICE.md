# Fork notice

Fork of [Dazzuh/SkillMultiplier](https://github.com/Dazzuh/SkillMultiplier) 2.1.2, MIT licensed (see `LICENSE`, unchanged).
Personal fork, not published. `main` mirrors upstream; the work is on `crafting-xp-4-1-6`.

## What changed

- **Crafting rows now work on SPT 4.1.x.** The client cannot progress Crafting, and the server pays crafting XP
  from the hideout config, not from the globals the old rows scaled. The four globals rows
  (`PointsPerCraftingCycle`, `CraftingCycleHours`, `PointsPerUniqueCraftCycle`, `UniqueCraftsPerCycle`) did nothing
  and are removed. Two rows replace them, scaling the hideout config live: `Settings.Crafting.CraftingExpAmount`
  (XP per alternating craft) and `Settings.Crafting.CraftingXpPerHour` (XP per hour of recipe time, via
  `hoursForSkillCrafting`).
- Client-keyed rows of skills the client cannot progress (`Crafting[0..1]`, `HideoutManagement[0..5]`,
  `WeaponTreatment[0]`) are no longer listed.
- The server-grant multiplier never applies to Crafting or HideoutManagement, so their grants are not scaled twice.

Client half unchanged.

# ERW (EarlyRespawnWave)

EarlyRespawnWave is a plugin on the SiteFunnyArea SCP:SL Heavily Modded Server, EarlyRespawnWave gives people who die early into the game, or early into the first MTF/CI spawnwave, a second chance at life by doing a early-game CustomRole spawnwave a few minutes into the game/spawnwave.

# ERW Tickets
> To see the old system (1.0.0-1.1.0), [view this page](https://github.com/SiteFunnyArea/EarlyRespawnWave/blob/27a521b53330018043a473ef343cf61a4832a973/README.md#erw-tickets).

The ERW ticket system works like this. When a player dies, if a player is classified as having a 'passive' role (such as Scientist, Mobile Task Force, Facility Guard) and they kill another player who were classified as having a 'non-passive' role (such as SCPs, Class-D, Chaos Insurgency), then it will add one Rapid Response Team ticket, if it's reversed to where a player who has a 'non-passive' role kills a player who has a 'passive' role, it will add one Infiltration Insurgency Squad ticket. If a player dies without being killed by another player, if the player had a 'passive' role, it will add one Infiltration Insurgency Squad ticket, and if the player had a 'non-passive' role, it will add one Rapid Response Team ticket. If Rapid Response Team tickets are over Infiltration Insurgency Squad tickets, it will spawn Rapid Response Team. If Infiltration Insurgency Squad tickets are over Rapid Response Team tickets, it will spawn Infiltration Insurgency Squad. If they are equal, it will do a 50/50 for which team spawns. During second wave, the ticket system doesn't matter as it is a guarantee Serpents Hand spawn.

# When Does A Early Spawnwave Occur
By default, in the config, an early spawnwave occurs after 2 minutes and 30 seconds after a game begins, and also 2 minutes and 30 seconds after the first spawnwave occurs. 

Those are the only two times an early spawnwave occurs.

# ERW Spawnable Teams
There are three teams right now that a player can spawn as when a early-game spawnwave occurs

| Team Name  | Role Type | Total Roles  | What Wave Do They Spawn |
| ------------- | ------------- | ------------- | ------------- |
| Rapid Response Team  | NtfPrivate  | 1  | First Wave (start of game)  |
| Infiltration Insurgency Squad  | ChaosConscript  | 1  | First Wave (start of game)   |
| Serpents Hand [DISABLED CURRENTLY]  | Tutorial  | 8  | Second Wave (first MTF/CI spawnwave)  |


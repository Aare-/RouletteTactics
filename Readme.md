# Roulette Tactics
Simple battle simulator mixed with betting similar to roulette.
Requires Unity 2019.4.40 LTS

## Architecture
I decided to keep it simple and minimal. No frameworks, no UniRx, no bloat of MVVM / MVC. It should be a good start for the team to extend in direction of their choice.

## Performance
Performance of the unit path-finding could be greatly optimised using f.e. kd-trees or physic2d queries. 
As it run sufficiently fast (given 40 units) using simplest approach I kept the performance optimisation for future.

## Additional Feature
I choose a betting meta-game simillar to the roulette. Player starts with 10$ and can bet on the outcome of the fight. 
As seemed like a red trait increases chance of survival (+200hp) it's reflected in the payout table.
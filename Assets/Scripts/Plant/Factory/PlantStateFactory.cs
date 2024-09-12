using System.Collections.Generic;
using Plant.States;
using Plant.States.Aloecura;
using Plant.States.Bamburst;
using Plant.States.Boomkin;
using Plant.States.Cactharn;
using Plant.States.Cobcorn;
using Plant.States.Duricane;
using Plant.States.Explomato;
using Plant.States.Sneezeweed;
using Plant.States.Magnesprout;
using Plant.States.Swiftglory;
using Plant.States.Triblastberry;
using Plant.States.Weisshooter;

namespace Plant.Factory
{
    public class PlantStateFactory
    {
        public static Dictionary<EPlantState, PlantState> CreateStates(EPlant type, Plant plant)
        {
            return type switch
            {
               EPlant.Cactharn => new Dictionary<EPlantState, PlantState>
                {
                    { EPlantState.Grow , new PlantGrowState(plant)},
                    { EPlantState.Idle , new PlantIdleState(plant)},
                    { EPlantState.Attack , new CactharnAttackState(plant)},
                    { EPlantState.Select , new PlantSelectState(plant)},
                    { EPlantState.Die, new PlantDieState(plant)}
                },
                EPlant.Cobcorn => new Dictionary<EPlantState, PlantState>
                {
                    { EPlantState.Grow , new PlantGrowState(plant)},
                    { EPlantState.Idle , new PlantIdleState(plant)},
                    { EPlantState.Attack , new CobcornAttackState(plant)},
                    { EPlantState.Select , new PlantSelectState(plant)},
                    { EPlantState.Die, new PlantDieState(plant)}
                },
                EPlant.Weisshooter => new Dictionary<EPlantState, PlantState>
                {
                    { EPlantState.Grow , new PlantGrowState(plant)},
                    { EPlantState.Idle , new PlantIdleState(plant)},
                    { EPlantState.Attack , new WeisshooterAttackState(plant)},
                    { EPlantState.Select , new PlantSelectState(plant)},
                    { EPlantState.Die, new PlantDieState(plant)}
                },
                EPlant.Duricane => new Dictionary<EPlantState, PlantState>
                {
                    { EPlantState.Grow , new PlantGrowState(plant)},
                    { EPlantState.Idle , new PlantIdleState(plant)},
                    { EPlantState.Attack , new DuricaneAttackState(plant)},
                    { EPlantState.Select , new PlantSelectState(plant)},
                    { EPlantState.Die, new PlantDieState(plant)}
                },
                EPlant.Boomkin => new Dictionary<EPlantState, PlantState>
                {
                    { EPlantState.Grow , new PlantGrowState(plant)},
                    { EPlantState.Idle , new BoomkinIdleState(plant)},
                    { EPlantState.Select , new PlantSelectState(plant)},
                    { EPlantState.Die, new BoomkinDieState(plant)}
                },
                EPlant.Bamburst => new Dictionary<EPlantState, PlantState>
                {
                    { EPlantState.Grow , new PlantGrowState(plant)},
                    { EPlantState.Idle , new PlantIdleState(plant)},
                    { EPlantState.Attack , new BamburstAttackState(plant)},
                    { EPlantState.Select , new PlantSelectState(plant)},
                    { EPlantState.Die, new PlantDieState(plant)}
                },
                EPlant.Aloecura => new Dictionary<EPlantState, PlantState>
                {
                    { EPlantState.Grow , new PlantGrowState(plant)},
                    { EPlantState.Idle , new AloecuraIdleState(plant)},
                    { EPlantState.Attack , new AloecuraAttackState(plant)},
                    { EPlantState.Select , new PlantSelectState(plant)},
                    { EPlantState.Die, new PlantDieState(plant)}
                },
               EPlant.Triblastberry => new Dictionary<EPlantState, PlantState>
               {
                   { EPlantState.Grow , new PlantGrowState(plant)},
                   { EPlantState.Idle , new PlantIdleState(plant)},
                   { EPlantState.Attack , new TriblastberryAttackState(plant)},
                   { EPlantState.Select , new PlantSelectState(plant)},
                   { EPlantState.Die, new PlantDieState(plant)}
               },
               EPlant.Explomato => new Dictionary<EPlantState, PlantState>
               {
                   { EPlantState.Grow , new PlantGrowState(plant)},
                   { EPlantState.Idle , new PlantIdleState(plant)},
                   { EPlantState.Attack , new ExplomatoAttackState(plant)},
                   { EPlantState.Select , new PlantSelectState(plant)},
                   { EPlantState.Die, new PlantDieState(plant)}
               },
               EPlant.Raflessnare => new Dictionary<EPlantState, PlantState>
               {
                   { EPlantState.Grow , new PlantGrowState(plant)},
                   { EPlantState.Idle , new PassiveIdleState(plant)},
                   { EPlantState.Select , new PlantSelectState(plant)},
                   { EPlantState.Die, new PlantDieState(plant)}
               },
               EPlant.Cocowall => new Dictionary<EPlantState, PlantState>
               {
                   { EPlantState.Grow , new PlantGrowState(plant)},
                   { EPlantState.Idle , new PassiveIdleState(plant)},
                   { EPlantState.Select , new PlantSelectState(plant)},
                   { EPlantState.Die, new PlantDieState(plant)}
               },
               EPlant.Luckyclover => new Dictionary<EPlantState, PlantState>
               {
                   { EPlantState.Grow , new PlantGrowState(plant)},
                   { EPlantState.Idle , new PassiveIdleState(plant)},
                   { EPlantState.Select , new PlantSelectState(plant)},
                   { EPlantState.Die, new PlantDieState(plant)}
               },
               EPlant.Sneezeweed => new Dictionary<EPlantState, PlantState>
               {
                   { EPlantState.Grow , new PlantGrowState(plant)},
                   { EPlantState.Idle , new PlantIdleState(plant)},
                   { EPlantState.Attack , new SneezeweedAttackState(plant)},
                   { EPlantState.Select , new PlantSelectState(plant)},
                   { EPlantState.Die, new PlantDieState(plant)}
               },
               EPlant.Magnesprout => new Dictionary<EPlantState, PlantState>
               {
                   { EPlantState.Grow , new PlantGrowState(plant)},
                   { EPlantState.Idle , new MagnesproutIdleState(plant)},
                   { EPlantState.Attack , new MagnesproutAttackState(plant)},
                   { EPlantState.Select , new PlantSelectState(plant)},
                   { EPlantState.Die, new PlantDieState(plant)}
               },
               EPlant.Swiftglory => new Dictionary<EPlantState, PlantState>
               {
                   { EPlantState.Grow , new PlantGrowState(plant)},
                   { EPlantState.Idle , new SwiftgloryIdleState(plant)},
                   { EPlantState.Attack , new SwiftgloryAttackState(plant)},
                   { EPlantState.Select , new PlantSelectState(plant)},
                   { EPlantState.Die, new PlantDieState(plant)}
               },
                _ => new Dictionary<EPlantState, PlantState>
                {
                    { EPlantState.Grow , new PlantGrowState(plant)},
                    { EPlantState.Idle, new PlantIdleState(plant)},
                    { EPlantState.Attack, new PlantAttackState(plant)},
                    { EPlantState.Select, new PlantSelectState(plant)},
                    { EPlantState.Die, new PlantDieState(plant)}
                }
            };
        }
    }
}
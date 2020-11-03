using System;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;

namespace HunieMod
{
    /// <summary>
    /// Helper class that provides access to all the <see cref="Definition"/> objects in the game
    /// </summary>
    public static class Definitions
    {
        private static List<T> GetDefinitions<T>(object instance) where T : Definition
        {
            if (instance == null) throw new ArgumentNullException(nameof(instance));
            var field = AccessTools.Field(instance.GetType(), "_definitions") ?? throw new InvalidOperationException($"Field _definitions was not found on type {instance.GetType()}");
            return (field.GetValue(instance) as Dictionary<int, T>).Values.ToList();
        }

        #region Public methods

        /// <summary>
        /// Gets a random definition of the specified type
        /// </summary>
        /// <typeparam name="T">The type of the definition</typeparam>
        /// <returns>A random definition of the specified type</returns>
        public static T GetRandom<T>() where T : Definition
        {
            List<T> definitions;
            switch (typeof(T).Name)
            {
                case nameof(AbilityDefinition):
                    definitions = Abilities as List<T>;
                    break;
                case nameof(ActionMenuItemDefinition):
                    definitions = ActionMenuItems as List<T>;
                    break;
                case nameof(CellAppDefinition):
                    definitions = CellApps as List<T>;
                    break;
                case nameof(DebugProfile):
                    definitions = DebugProfiles as List<T>;
                    break;
                case nameof(DialogSceneDefinition):
                    definitions = DialogScenes as List<T>;
                    break;
                case nameof(DialogTriggerDefinition):
                    definitions = DialogTriggers as List<T>;
                    break;
                case nameof(EnergyTrailDefinition):
                    definitions = EnergyTrails as List<T>;
                    break;
                case nameof(GirlDefinition):
                    definitions = Girls as List<T>;
                    break;
                case nameof(ItemDefinition):
                    definitions = Items as List<T>;
                    break;
                case nameof(LocationDefinition):
                    definitions = Locations as List<T>;
                    break;
                case nameof(MessageDefinition):
                    definitions = Messages as List<T>;
                    break;
                case nameof(ParticleEmitter2DDefinition):
                    definitions = Particles as List<T>;
                    break;
                case nameof(PuzzleTokenDefinition):
                    definitions = PuzzleTokens as List<T>;
                    break;
                case nameof(SpriteGroupDefinition):
                    definitions = SpriteGroups as List<T>;
                    break;
                case nameof(TraitDefinition):
                    definitions = Traits as List<T>;
                    break;
                default:
                    definitions = new List<T>();
                    break;
            }
            return definitions[UnityEngine.Random.Range(0, definitions.Count)];
        }

        /// <summary>
        /// Tries to find an instance of a girl's definition that matches with the specified ID
        /// </summary>
        /// <param name="girlId">The ID of the girl to find</param>
        /// <returns>The definition of the girl, or default if not found</returns>
        public static GirlDefinition GetGirl(GirlId girlId) => Girls.FirstOrDefault(girl => (GirlId)girl.id == girlId);

        /// <summary>
        /// Tries to find an instance of a girl's definition with the specified name, case-insensitive
        /// </summary>
        /// <param name="firstName">The first name of the girl to find</param>
        /// <returns>The definition of the girl, or default if not found</returns>
        public static GirlDefinition GetGirl(string firstName) => Girls.FirstOrDefault(girl => string.Equals(girl.firstName, firstName, StringComparison.OrdinalIgnoreCase));

        #endregion

        #region Public properties

        /// <summary>
        /// Instances of all the Ability definitions in the game
        /// </summary>
        public static List<AbilityDefinition> Abilities => GetDefinitions<AbilityDefinition>(GameManager.Data.Abilities);

        /// <summary>
        /// Instances of all the Action Menu Item definitions in the game
        /// </summary>
        public static List<ActionMenuItemDefinition> ActionMenuItems => GetDefinitions<ActionMenuItemDefinition>(GameManager.Data.ActionMenuItems);

        /// <summary>
        /// Instances of all the Cellphone App definitions in the game
        /// </summary>
        public static List<CellAppDefinition> CellApps => GetDefinitions<CellAppDefinition>(GameManager.Data.CellApps);

        /// <summary>
        /// Instances of all the Debug Profile definitions in the game
        /// </summary>
        public static List<DebugProfile> DebugProfiles => GetDefinitions<DebugProfile>(GameManager.Data.DebugProfiles);

        /// <summary>
        /// Instances of all the Dialog Scene definitions in the game
        /// </summary>
        public static List<DialogSceneDefinition> DialogScenes => GetDefinitions<DialogSceneDefinition>(GameManager.Data.DialogScenes);

        /// <summary>
        /// Instances of all the Dialog Trigger definitions in the game
        /// </summary>
        public static List<DialogTriggerDefinition> DialogTriggers => GetDefinitions<DialogTriggerDefinition>(GameManager.Data.DialogTriggers);

        /// <summary>
        /// Instances of all the Energy Trail definitions in the game
        /// </summary>
        public static List<EnergyTrailDefinition> EnergyTrails => GetDefinitions<EnergyTrailDefinition>(GameManager.Data.EnergyTrails);

        /// <summary>
        /// Instances of all the Girl definitions in the game
        /// </summary>
        public static List<GirlDefinition> Girls => GameManager.Data.Girls.GetAll();

        /// <summary>
        /// Instances of all the Item definitions in the game
        /// </summary>
        public static List<ItemDefinition> Items => GetDefinitions<ItemDefinition>(GameManager.Data.Items);

        /// <summary>
        /// Instances of all the Location definitions in the game
        /// </summary>
        public static List<LocationDefinition> Locations => GetDefinitions<LocationDefinition>(GameManager.Data.Locations);

        /// <summary>
        /// Instances of all the Message definitions in the game
        /// </summary>
        public static List<MessageDefinition> Messages => GetDefinitions<MessageDefinition>(GameManager.Data.Messages);

        /// <summary>
        /// Instances of all the 2D Particle Emitter definitions in the game
        /// </summary>
        public static List<ParticleEmitter2DDefinition> Particles => GetDefinitions<ParticleEmitter2DDefinition>(GameManager.Data.Particles);

        /// <summary>
        /// Instances of all the Puzzle Token definitions in the game
        /// </summary>
        public static List<PuzzleTokenDefinition> PuzzleTokens => GameManager.Data.PuzzleTokens.GetAll().ToList();

        /// <summary>
        /// Instances of all the Sprite Group definitions in the game
        /// </summary>
        public static List<SpriteGroupDefinition> SpriteGroups => GetDefinitions<SpriteGroupDefinition>(GameManager.Data.SpriteGroups);

        /// <summary>
        /// Instances of all the Trait definitions in the game
        /// </summary>
        public static List<TraitDefinition> Traits => GetDefinitions<TraitDefinition>(GameManager.Data.Traits);

        #endregion
    }
}

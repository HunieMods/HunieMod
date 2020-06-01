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

        /// <summary>
        /// Gets a random definition of the specified type
        /// </summary>
        /// <typeparam name="T">The type of the definition</typeparam>
        /// <returns>A random definition of the specified type</returns>
        public static T GetRandom<T>() where T : Definition
        {
            List<T> definitions;
            var type = typeof(T);

            if (type == typeof(AbilityDefinition)) definitions = Abilities as List<T>;
            else if (type == typeof(ActionMenuItemDefinition)) definitions = ActionMenuItems as List<T>;
            else if (type == typeof(CellAppDefinition)) definitions = CellApps as List<T>;
            else if (type == typeof(DebugProfile)) definitions = DebugProfiles as List<T>;
            else if (type == typeof(DialogSceneDefinition)) definitions = DialogScenes as List<T>;
            else if (type == typeof(DialogTriggerDefinition)) definitions = DialogTriggers as List<T>;
            else if (type == typeof(EnergyTrailDefinition)) definitions = EnergyTrails as List<T>;
            else if (type == typeof(GirlDefinition)) definitions = Girls as List<T>;
            else if (type == typeof(ItemDefinition)) definitions = Items as List<T>;
            else if (type == typeof(LocationDefinition)) definitions = Locations as List<T>;
            else if (type == typeof(MessageDefinition)) definitions = Messages as List<T>;
            else if (type == typeof(ParticleEmitter2DDefinition)) definitions = Particles as List<T>;
            else if (type == typeof(SpriteGroupDefinition)) definitions = SpriteGroups as List<T>;
            else if (type == typeof(TraitDefinition)) definitions = Traits as List<T>;
            else definitions = new List<T>();

            return definitions[UnityEngine.Random.Range(0, definitions.Count)];
        }

        /// <summary>
        /// Instances of all the Ability definitions in the game
        /// </summary>
        public static List<AbilityDefinition> Abilities => GetDefinitions<AbilityDefinition>(GameManager.Data.Abilities);

        /// <summary>
        /// Instances of all the Action Menu Item definitions in the game
        /// </summary>
        public static List<ActionMenuItemDefinition> ActionMenuItems => GetDefinitions<ActionMenuItemDefinition>(GameManager.Data.ActionMenuItems);

        /// <summary>
        /// Instances of all the cellphone app definitions in the game
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
        /// Instances of all the girl definitions in the game
        /// </summary>
        public static List<GirlDefinition> Girls => GameManager.Data.Girls.GetAll();

        /// <summary>
        /// Instances of all the Item definitions in the game
        /// </summary>
        public static List<ItemDefinition> Items => GetDefinitions<ItemDefinition>(GameManager.Data.Items);

        /// <summary>
        /// Instances of all the location definitions in the game
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
        /// Instances of all the Sprite Group definitions in the game
        /// </summary>
        public static List<SpriteGroupDefinition> SpriteGroups => GetDefinitions<SpriteGroupDefinition>(GameManager.Data.SpriteGroups);

        /// <summary>
        /// Instances of all the Trait definitions in the game
        /// </summary>
        public static List<TraitDefinition> Traits => GetDefinitions<TraitDefinition>(GameManager.Data.Traits);
    }
}

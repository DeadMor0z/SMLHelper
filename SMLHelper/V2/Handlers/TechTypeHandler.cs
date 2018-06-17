﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMLHelper.V2.Patchers;

namespace SMLHelper.V2.Handlers
{
    /// <summary>
    /// Handles everything related to creating new TechTypes.
    /// </summary>
    public class TechTypeHandler
    {
        /// <summary>
        /// Adds a new TechType into the game.
        /// </summary>
        /// <param name="internalName">The internal name of the TechType. Should not contain special characters.</param>
        /// <param name="displayName">The display name of the TechType. Can be anything.</param>
        /// <param name="tooltip">The tooltip, displayed when hovered in an inventory. Can be anything.</param>
        /// <returns>The new TechType that is created.</returns>
        public static TechType AddTechType(string internalName, string displayName, string tooltip, bool unlockedAtStart = true)
        {
            // Register the TechType.
            var techType = TechTypePatcher.AddTechType(internalName);

            // Register Language lines.
            LanguagePatcher.customLines[internalName] = displayName;
            LanguagePatcher.customLines["Tooltip_" + internalName] = tooltip;

            // Unlock the TechType on start
            if (unlockedAtStart)
                KnownTechPatcher.unlockedAtStart.Add(techType);

            // Return the new TechType.
            return techType;
        }

        /// <summary>
        /// Adds a new TechType into the game, with a sprite.
        /// </summary>
        /// <param name="internalName">The internal name of the TechType. Should not contain special characters.</param>
        /// <param name="displayName">The display name of the TechType. Can be anything.</param>
        /// <param name="tooltip">The tooltip, displayed when hovered in an inventory. Can be anything.</param>
        /// <param name="sprite">The sprite that will related to this TechType.</param>
        /// <param name="unlockAtStart">Whether this TechType should be unlocked on game start, or not. By default, true.</param>
        /// <returns>The new TechType that is created.</returns>
        public static TechType AddTechType(string internalName, string displayName, string tooltip, Atlas.Sprite sprite, bool unlockAtStart = true)
        {
            // Register the TechType using overload.
            var techType = AddTechType(internalName, displayName, tooltip, unlockAtStart);

            // Register the Sprite
            if(sprite != null)
                CustomSpriteHandler.customSprites.Add(new CustomSprite(techType, sprite));

            // Return the new TechType
            return techType;
        }

        /// <summary>
        /// Adds a new TechType into the game, with a sprite.
        /// </summary>
        /// <param name="internalName">The internal name of the TechType. Should not contain special characters.</param>
        /// <param name="displayName">The display name of the TechType. Can be anything.</param>
        /// <param name="tooltip">The tooltip, displayed when hovered in an inventory. Can be anything.</param>
        /// <param name="sprite">The sprite that will related to this TechType.</param>
        /// <param name="unlockAtStart">Whether this TechType should be unlocked on game start, or not. By default, true.</param>
        /// <returns>The new TechType that is created.</returns>
        public static TechType AddTechType(string internalName, string displayName, string tooltip, UnityEngine.Sprite sprite, bool unlockAtStart = true)
        {
            // Register the TechType using overload.
            var techType = AddTechType(internalName, displayName, tooltip, unlockAtStart);

            // Register the Sprite
            if (sprite != null)
                CustomSpriteHandler.customSprites.Add(new CustomSprite(techType, sprite));

            // Return the new TechType
            return techType;
        }
    }
}
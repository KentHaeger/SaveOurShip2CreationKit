﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using Verse;
using RimWorld;

namespace SaveOurShip2
{
	class Designator_NewShipMap : Designator
	{
		public override AcceptanceReport CanDesignateCell(IntVec3 loc)
		{
			if (Find.CurrentMap.IsSpace())
				return true;
			Messages.Message("Ship editor works only on space maps!", MessageTypeDefOf.RejectInput);
			return false;
		}

		public Designator_NewShipMap()
		{
			defaultLabel = "New Ship Map";
			defaultDesc = "Create a new map in outer space";
			icon = ContentFinder<Texture2D>.Get("UI/New_Ship_Map");
			soundDragSustain = SoundDefOf.Designate_DragStandard;
			soundDragChanged = SoundDefOf.Designate_DragStandard_Changed;
			useMouseIcon = true;
			soundSucceeded = SoundDefOf.Designate_Deconstruct;
		}

		public override void DesignateSingleCell(IntVec3 loc)
		{
			int newTile = -1;
			for (int i = 0; i < 420; i++)
			{
				if (!Find.World.worldObjects.AnyMapParentAt(i))
				{
					newTile = i;
					break;
				}
			}
			Map map = GetOrGenerateMapUtility.GetOrGenerateMap(newTile, ResourceBank.WorldObjectDefOf.WreckSpace);
			map.GetComponent<ShipMapComp>().ShipMapState = ShipMapState.isGraveyard;
			GetOrGenerateMapUtility.UnfogMapFromEdge(map);
			CameraJumper.TryJump(map.Center, map);
		}
	}
}

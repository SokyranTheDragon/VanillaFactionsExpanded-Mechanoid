﻿using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using Verse.AI;

namespace VFEMech
{
	public class JobGiver_DisassembleAnyNonMechBuildings : ThinkNode_JobGiver
	{
		public float radius;
		protected override Job TryGiveJob(Pawn pawn)
		{
			var allThings = pawn.Map.listerThings.AllThings.Where(x => x.Faction != pawn.Faction && x.def.building != null);
			Predicate<Thing> validator = (Thing t) => pawn.CanReserve(t);
			if (allThings.Any())
			{
				var thing = GenClosest.ClosestThing_Global_Reachable(pawn.Position, pawn.Map, allThings, PathEndMode.Touch, TraverseParms.For(TraverseMode.PassAllDestroyableThings), radius, validator);
				if (thing != null)
				{
					Log.Message(pawn + " deconstructs " + thing);
					return JobMaker.MakeJob(VFEMDefOf.VFEM_Disassemble, thing);
				}
			}
			return null;
		}
	}
}
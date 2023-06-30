﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeklaResultsInterrogator.Core;
using TSD.API.Remoting.Structure;

namespace TeklaResultsInterrogator.Commands
{
    public class SteelBeamForces : ForceInterrogator
    {
        public SteelBeamForces()
        {
            HasOutput = true;
            RequestedMemberType = new List<MemberConstruction>() { MemberConstruction.SteelBeam };
        }

        public override async Task ExecuteAsync()
        {
            // Initialize parents
            await InitializeAsync();

            // Check for null properties
            if (Flag)
            {
                return;
            }

            // Data setup and diagnostics initialization
            Stopwatch stopwatch = Stopwatch.StartNew();
            int bufferSize = 65536;

            // Unpacking loading data
            FancyWriteLine("Loading Summary:", TextColor.Title);
            Console.WriteLine("Unpacking loading data...");
            Console.WriteLine($"{AllLoadcases.Count} loadcases found, {SolvedCases.Count} solved.");
            Console.WriteLine($"{AllCombinations.Count} load combinations found, {SolvedCombinations.Count} solved.");
            Console.WriteLine($"{AllEnvelopes.Count} load envelopes found, {SolvedEnvelopes.Count} solved.\n");

            // Unpacking member data
            FancyWriteLine("Member summary:", TextColor.Title);
            Console.WriteLine("Unpacking member data...");

            List<IMember> steelBeams = AllMembers.Where(c => RequestedMemberType.Contains(c.Data.Value.Construction.Value)).ToList();

            Console.WriteLine($"{AllMembers.Count} structural members found in model.");
            Console.WriteLine($"{steelBeams.Count} steel beams found.");

            



            // Call all command routines and subroutines here within this method

            // Finish up
            stopwatch.Stop();
            ExecutionTime = stopwatch.Elapsed.TotalSeconds;

            Check();

            return;
        }
    }
}

// $ [brew install mono|apt-get install mono-complete|yum install mono-complete]
// $ mcs day12_2.cs && mono day12_2.exe

using System;
using System.Collections.Generic;
using System.Linq;

class Day12
{
  static void Main()
  {
    var map = parseInputIntoMap();
    var groups = findGroups(ref map);
    Console.WriteLine($"The number of groups is {groups.Count}");
  }

  static Dictionary<int, List<int>> parseInputIntoMap()
  {
    var map = new Dictionary<int, List<int>>();
    foreach (var line in input().Split('\n')) {
      var lineData = parseLine(line);
      map.Add(lineData.ProgramId, lineData.LinkedIds);
    }
    return map;
  }

  static dynamic parseLine(string line) {
    var parts = line.Split(new [] {" <-> "}, StringSplitOptions.None);
    var programId = Convert.ToInt32(parts[0]);
    var linkedIds = parts[1]
                    .Split(new [] {", "}, StringSplitOptions.None)
                    .Select(num => Convert.ToInt32(num));

    return new { ProgramId = programId, LinkedIds = new List<int>(linkedIds) };
  }

  static List<List<int>> findGroups(ref Dictionary<int, List<int>> map)
  {
    var groups = new List<List<int>>();
    foreach (var programId in map.Keys) {
      if (groups.Any(g => g.Contains(programId))) continue;

      var group = findAssociatedIds(programId, ref map);
      groups.Add(group);
    }
    return groups;
  }

  static List<int> findAssociatedIds(int programId,
                                     ref Dictionary<int, List<int>> map)
  {
    var ids = new List<int>();
    findAssociatedIdsRecursively(programId, ref ids, ref map);
    return ids;
  }

  static void findAssociatedIdsRecursively(int programId,
                                           ref List<int> accumulator,
                                           ref Dictionary<int, List<int>> map)
  {
    foreach (var linkedId in map[programId]) {
      if (accumulator.Contains(linkedId)) continue;

      accumulator.Add(linkedId);
      findAssociatedIdsRecursively(linkedId, ref accumulator, ref map);
    }
  }

  static string input() => @"... paste input here w/o indentation ...";
}

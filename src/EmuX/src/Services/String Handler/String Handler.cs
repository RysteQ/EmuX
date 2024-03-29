﻿namespace EmuX.Services;

class StringHandler
{
    public static string[] RemoveEmptyLines(string[] lines_to_remove_from) => lines_to_remove_from.Where(line => string.IsNullOrWhiteSpace(line) == false).ToArray();

    public static int GetCharOccurrences(string string_to_check, char char_to_scan_for) => string_to_check.ToArray().Where(character => character == char_to_scan_for).ToArray().Length;

    public static string ReverseString(string string_to_reverse) => string.Join(string.Empty, string_to_reverse.Reverse());
}
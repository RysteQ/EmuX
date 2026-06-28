using EmuXCore.Interpreter.Models.Interfaces;
using EmuXCore.Common.Interfaces;
using System.Collections.Generic;

namespace EmuXCore.Interpreter.LexicalAnalysis.Interfaces;

/// <summary>
/// The IParser is responsible for grammatical rules and parsing the lexer tokens.
/// </summary>
public interface IParser
{
    /// <summary>
    /// Parses the source code into executable instructions of type <see cref="IInstruction"/>. <br /> 
    /// If any error in encountered then the appropriate logs and status will be saved to the <see cref="IParserResult.Success"/> property and <see cref="IParserResult.Errors"/> property inside the <see cref="IParserResult"/>.
    /// </summary>
    /// <param name="tokens">The source code to parse.</param>
    /// <returns>The result of the parse process.</returns>
    IParserResult Parse(IList<IToken> tokens);
}
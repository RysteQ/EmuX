using EmuXCore.Interpreter.Models.Interfaces;

namespace EmuXCore.Interpreter.LexicalAnalysis.Interfaces;

/// <summary>
/// The ILexer is responsible for parsing the source code into the IL represenation of said code
/// </summary>
public interface ILexer
{
    /// <summary>
    /// Tokenizes the source code for the IParser implementation
    /// </summary>
    /// <param name="sourceCode">The source code to tokenize</param>
    /// <returns>The tokenized source code</returns>
    public IList<IToken> Tokenize(string sourceCode);
}
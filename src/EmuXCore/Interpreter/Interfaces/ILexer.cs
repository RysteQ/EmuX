namespace EmuXCore.Interpreter.Interfaces;

public interface ILexer
{
    /// <summary>
    /// Parses the source code into executable instructions. If any error in encountered then the appropriate logs and status will be saved to the Success property and ErrorLog property
    /// </summary>
    /// <param name="codeToParse">The source code to parse</param>
    /// <returns>The parsed instructions</returns>
    ILexerResult Parse(string codeToParse);
}
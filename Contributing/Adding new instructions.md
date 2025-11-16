# Adding new instructions

Adding new instructions is a very straightforward process that includes minimal code. To add a new instructions first head off to `EmuXCore\InstructionLogic\Instructions` and create your new instruction with the naming convention `InstructiongXXX` where **XXX** represents the mnemonic of the instruction.

Inside the newly created file copy and paste the following code.

```csharp
public sealed class InstructionXXX : IInstruction
{
    public InstructionXXX(InstructionVariant variant, IPrefix? prefix, IOperand? firstOperand, IOperand? secondOperand, IOperand? thirdOperand, IOperandDecoder operandDecoder, IFlagStateProcessor flagStateProcessor, ulong bytes)
    {
        Variant = variant;
        Prefix = prefix;
        FirstOperand = firstOperand;
        SecondOperand = secondOperand;
        ThirdOperand = thirdOperand;
        OperandDecoder = operandDecoder;
        FlagStateProcessor = flagStateProcessor;
        Bytes = bytes;
    }

    public void Execute(IVirtualMachine virtualMachine)
    {
    }

    public bool IsValid()
    {
    }

    public string Opcode => "XXX";
    public ulong Bytes { get; private set; }

    public IOperandDecoder OperandDecoder { get; init; }
    public IFlagStateProcessor FlagStateProcessor { get; init; }
    public InstructionVariant Variant { get; init; }
    public IPrefix? Prefix { get; init; }
    public IOperand? FirstOperand { get; init; }
    public IOperand? SecondOperand { get; init; }
    public IOperand? ThirdOperand { get; init; }
}
```

With this code the only modification you need to do besides the name of the class are of the `Opcode` property to have the correct mnemonic of the instruction and the logic behind the `IsValid` and `Execute` method. Once that is done you need to navigate to the `IInstructionLookup` implementation and add the new instruction with its mnemonic to the `_allInstructionNamesAndTypes` field and then also go and modify the `ToIInstruction` method in the `ILexeme` implementation to account for the new instruction.

Please keep in mind the instructions are responsible for handling the `RIP` register which is not updated automatically after executing an instruction. For examples on how new instructions should look please have a look at the already implemented instructions.
# Adding new BIOS interrupts

Adding a new BIOS interrupt is quite a long process so this document is necessary for the correct implementation of a new BIOS interrupt.

First of all navigate to the `InterruptCode` enum and check if a value has been set aside for your chosen **interrupt type**, if it already exist please skip to this 

## Adding new BIOS interrupt

For this scenario add a new value to the `InterruptCode` enum and if possible assign a value to it that represents real BIOS interrupt values. After that add a new enumeration value for the sub interrupt code of the new interrupt under the `EmuXCore\VM\Interfaces\Components\BIOS\Enums\SubInterrupts` folder and that enum should be of type byte. Now add all the sub interrupt codes you want to add that, if possible, should adhere to real BIOS interrupt codes. Lastly, add a new method to the `IVirtualBIOS` implementation and then call that method in the `Interrupt` method of the `IVirtualMachine` implementation.

After you do that you also need to create an interrupt handler interface under `EmuXCore\VM\Interfaces\Components\BIOS\Interfaces` and its implementation under the folder `EmuXCore\VM\Internal\BIOS\InterruptHandlers`. Once that is done you should add a new generator method in the `DIFactory` singleton class to generate the newly created interrupt handler. After that modify the `IVirtualBIOS` implementation to accept the new interrupt handler and also modify its generator method in the `DIFactory` singleton class the `GenerateIVirtualBIOS` method. Once that is done modify all the calls to the modified `GenerateIVirtualBIOS` method and add to the new required interrupt handler method a call to the new interrupt handler generator method.

After all that add a new methord inside the `IVirtualBIOS` implementation to invoke the newly added interrupt handler.

Lastly you also need to modify the `Interrupt` method inside the `IVirtualMachine` implementation to account for the new BIOS interrupt code.

## Using an existing BIOS interrupt

If the interrupt already exist then you should modify the interrupt handler class inside of the `EmuXCore\VM\Internal\BIOS\InterruptHandlers` to add the new interrupt method. Once that is done you need to modify the `IVirtualBIOS` implementation to handle the new interrupt handler method.
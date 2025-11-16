# Video interrupts

## Draw pixel

Interrupt code: **0x0c**

Description: Draws a single pixel in the memory buffer of the GPU in the specified XY coordinates and the specified RGB values.

|  Parameter   | Register | Notes                              |
| ------------ | -------- | ---------------------------------- |
| X coordinate | ECX      | The 0xffff0000 bitmask is applied  |
| Y coordinate | EDX      | The 0xffff0000 bitmask is applied  |
| Red          | RBX      | The 0x00ff0000 bitmask is applied  |
| Green        | RBX      | The 0x00ff0000 bitmask is applied  |
| Blue         | RBX      | The 0x00ff0000 bitmask is applied  |

## Read pixel

Interrupt code: **0x0d**

Description: Read the RGB value of a single pixel.

|  Parameter   | Register | Notes                              |
| ------------ | -------- | ---------------------------------- |
| X coordinate | CX       |                                    |
| Y coordinate | DX       |                                    |

| Return value | Register | Notes                                  |
| ------------ | -------- | -------------------------------------- |
| Red          | EBX      | The value is stored in the second byte |
| Green        | EBX      | The value is stored in the third byte  |
| Blue         | EBX      | The value is stored in the fourt byte  |

## Get resolution

Interrupt code: **0x0f**

Description: Gets the resolution the GPU is operating at.

| Return value | Register | Notes                                  |
| ------------ | -------- | -------------------------------------- |
| Height       | AX       |                                        |
| Widht        | CX       |                                        |

## Draw line

Interrupt code: **0x50**

Description: Draws a single line in the memory buffer of the GPU in the specified XY coordinates and the specified RGB values.

|  Parameter            | Register | Notes                              |
| --------------------- | -------- | ---------------------------------- |
| Starting X coordinate | ECX      | The 0xffff0000 bitmask is applied  |
| Ending X coordinate   | ECX      | The 0x0000ffff bitmask is applied  |
| Starting Y coordinate | EDX      | The 0xffff0000 bitmask is applied  |
| Ending Y coordinate   | EDX      | The 0x0000ffff bitmask is applied  |
| Red                   | RBX      | The 0x00ff0000 bitmask is applied  |
| Green                 | RBX      | The 0x00ff0000 bitmask is applied  |
| Blue                  | RBX      | The 0x00ff0000 bitmask is applied  |

## Draw box

Interrupt code: **0x51**

Description: Draws a box pixel in the memory buffer of the GPU in the specified XY coordinates and the specified RGB values.

|  Parameter            | Register | Notes                              |
| --------------------- | -------- | ---------------------------------- |
| Starting X coordinate | ECX      | The 0xffff0000 bitmask is applied  |
| Ending X coordinate   | ECX      | The 0x0000ffff bitmask is applied  |
| Starting Y coordinate | EDX      | The 0xffff0000 bitmask is applied  |
| Ending Y coordinate   | EDX      | The 0x0000ffff bitmask is applied  |
| Red                   | RBX      | The 0x00ff0000 bitmask is applied  |
| Green                 | RBX      | The 0x00ff0000 bitmask is applied  |
| Blue                  | RBX      | The 0x00ff0000 bitmask is applied  |
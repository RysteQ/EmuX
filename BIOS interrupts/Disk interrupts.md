# Disk interrupts

## Read track

Interrupt code: **0x02**

Description: Reads N amount of sectors from the specified disk.

|   Parameter    | Register | Notes                                            |
| -------------- | -------- | ------------------------------------------------ |
| Sectos to read | AL       | The amount of sectors to read                    |
| Platter        | DH       | The platter to read from                         |
| Track          | CH       | The track to read from                           |
| Sector         | DL       | The sector to read from                          |
| Disk           | DL       | The disk to read from                            |
| Address        | ES:BX    | The address in memory to store the read bytes to |

## Write track

Interrupt code: **0x03**

Description: Writes N amount of sectors from the specified disk.

|   Parameter    | Register | Notes                                             |
| -------------- | -------- | ------------------------------------------------- |
| Sectos to read | AL       | The amount of sectors to read                     |
| Platter        | DH       | The platter to read from                          |
| Track          | CH       | The track to read from                            |
| Sector         | DL       | The sector to read from                           |
| Disk           | DL       | The disk to read from                             |
| Address        | ES:BX    | The address in memory to read the read bytes from |
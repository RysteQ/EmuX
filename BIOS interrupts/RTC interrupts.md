# RTC interrupts

## Read system clock

Interrupt code: **0x00**

Description: Reads the value of the system clock.

Note: The DL register is set to zero.

| Return value | Register | Notes                                  |
| ------------ | -------- | -------------------------------------- |
| Hour         | CH       |                                        |
| Minute       | CL       |                                        |
| Second       | DH       |                                        |

## Set system clock

Interrupt code: **0x01**

Description: Sets a new value for the system clock.

| Parameter  | Register | Notes                                  |
| ---------- | -------- | -------------------------------------- |
| Hour       | CH       |                                        |
| Minute     | CL       |                                        |
| Second     | DH       |                                        |

## Read RTC

Interrupt code: **0x02**

Description: Reads the value of the RTC.

Note: The DL register is set to zero.

| Return value | Register | Notes                                  |
| ------------ | -------- | -------------------------------------- |
| Hour         | CH       |                                        |
| Minute       | CL       |                                        |
| Second       | DH       |                                        |

## Set RTC

Interrupt code: **0x03**

Description: Sets a new value for the RTC.

| Parameter  | Register | Notes                                  |
| ---------- | -------- | -------------------------------------- |
| Hour       | CH       |                                        |
| Minute     | CL       |                                        |
| Second     | DH       |                                        |
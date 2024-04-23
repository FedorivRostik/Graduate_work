import { PressureEnum } from 'src/app/utilities/enums/pressure.enum';

export class UpdateUserPersonalParamsDto {
  public weight!: number;
  public age!: number;
  public region!: string;
  public city!: string;
  public pressure!: number;
  public AvgUpSystolicPressure!: number;
  public AvgDownSystolicPressure!: number;
  public AvgUpDialysticPressure!: number;
  public AvgDonwDialysticPressure!: number;
}

import { PressureEnum } from 'src/app/utilities/enums/pressure.enum';

export class User {
  public userId!: string;
  public email!: string;
  public name!: string;
  public phoneNumber!: string;
  public role!: string;
  public weight!: number;
  public age!: number;
  public region!: string;
  public city!: string;
  public pressure: PressureEnum = PressureEnum.None;
  public avgUpSystolicPressure!: number;
  public avgDownSystolicPressure!: number;
  public avgUpDialysticPressure!: number;
  public avgDonwDialysticPressure!: number;
}

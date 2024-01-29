import { User } from '../user/user.model';

export class LoginResponse {
  public user!: User;
  public token!: string;
}

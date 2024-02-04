import { CartDetails } from '../cartDetails/cartDetails.model';
import { CartHeader } from '../cartHeaders/cartHeader.model';

export class Cart {
  cartHeader!: CartHeader;
  cartDetails!: CartDetails[];
}

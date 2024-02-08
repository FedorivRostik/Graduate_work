import { Product } from '../../products/product.model';
import { CartHeader } from '../cartHeaders/cartHeader.model';

export class CartDetails {
  cartDetailsId!: string;
  cartHeaderId!: string;
  cartHeaderDto!: CartHeader;
  productId!: string;
  productDto!: Product;
  count!: number;
  price!: number;
  discount?: number;
}

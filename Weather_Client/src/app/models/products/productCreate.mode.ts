export class ProductCreate {
  public productId!: string;
  public name!: string;
  public slug!: string;
  public amount!: number;
  public price!: number;
  public discount?: number;
  public description?: string;
  public couponCode?: string;
  public imageUrl?: string;
  public genreId?: string;
}

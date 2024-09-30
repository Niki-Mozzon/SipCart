import { ePaymentMethod } from "./ePaymentMethod";
import { Product } from "./product";

export interface Order {
 couponCode: string;
 percentageReduction: number;
 products: Product[];
 totalPrice: number;
 fullPrice: number;
 paymentMethod: ePaymentMethod;
}

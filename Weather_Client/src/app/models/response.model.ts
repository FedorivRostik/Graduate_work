export class ResponseModel<T> {
  isSuccess?: boolean;
  message?: string;
  resultObj?: T;
}

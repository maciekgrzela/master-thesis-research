import { CreateProductDto } from 'src/products/dtos/createProductDto';

export class CreateProductCommand {
  constructor(public payload: CreateProductDto) {}
}

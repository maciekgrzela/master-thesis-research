import { IQuery } from '@nestjs/cqrs';

export class GetSingleOrderQuery implements IQuery {
  public id: string;

  constructor(id: string) {
    this.id = id;
  }
}

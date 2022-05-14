import { IQuery } from '@nestjs/cqrs';

export class GetAllBillsQuery implements IQuery {
  public pageNumber?: number;
  public pageSize?: number;

  constructor(pageNumber: number | undefined, pageSize: number | undefined) {
    this.pageNumber = pageNumber;
    this.pageSize = pageSize;
  }
}

import { Bill, Order } from '@prisma/client';
import { IQueryHandler, QueryHandler } from '@nestjs/cqrs';

import { DataAccessRepository } from 'src/core/repositories/data-access.repository';
import { GetSingleOrderQuery } from './get-single-order.query';

@QueryHandler(GetSingleOrderQuery)
export class GetSingleOrderQueryHandler
  implements IQueryHandler<GetSingleOrderQuery>
{
  private dataAccessRepository: DataAccessRepository;

  constructor(dataAccessRepository: DataAccessRepository) {
    this.dataAccessRepository = dataAccessRepository;
  }

  async execute(query: GetSingleOrderQuery): Promise<Order | null> {
    return this.dataAccessRepository.getSingleOrderAsync({
      id: query.id,
    });
  }
}

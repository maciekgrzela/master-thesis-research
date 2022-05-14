import { IQueryHandler, QueryHandler } from '@nestjs/cqrs';

import { Bill } from '@prisma/client';
import { DataAccessRepository } from 'src/core/repositories/data-access.repository';
import { GetAllBillsQuery } from './get-all-bills.query';

@QueryHandler(GetAllBillsQuery)
export class GetAllBillsQueryHandler
  implements IQueryHandler<GetAllBillsQuery>
{
  private dataAccessRepository: DataAccessRepository;

  constructor(dataAccessRepository: DataAccessRepository) {
    this.dataAccessRepository = dataAccessRepository;
  }

  async execute(query: GetAllBillsQuery): Promise<Bill[]> {
    return this.dataAccessRepository.getAllBillsAsync({
      skip:
        query.pageNumber && query.pageSize
          ? (query.pageNumber - 1) * query.pageSize
          : undefined,
      take: query.pageSize,
    });
  }
}

import { CommandBus, QueryBus } from '@nestjs/cqrs';
import { Controller, Get, Query } from '@nestjs/common';

import { GetAllBillsQuery } from './queries/get-all-bills/get-all-bills.query';
import { ApiQuery } from '@nestjs/swagger';

@Controller('api/bills')
export class BillsController {
  private readonly commandBus: CommandBus;
  private readonly queryBus: QueryBus;

  constructor(commandBus: CommandBus, queryBus: QueryBus) {
    this.commandBus = commandBus;
    this.queryBus = queryBus;
  }

  @Get()
  @ApiQuery({ name: 'pageSize' })
  @ApiQuery({ name: 'pageNumber' })
  async getAllAsync(
    @Query('pageSize') pageSize: number | undefined,
    @Query('pageNumber') pageNumber: number | undefined,
  ) {
    return await this.queryBus.execute(
      new GetAllBillsQuery(pageNumber, pageSize),
    );
  }
}

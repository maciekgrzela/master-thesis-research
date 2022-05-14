import { CommandBus, QueryBus } from '@nestjs/cqrs';

import {
  Controller,
  Get,
  HttpException,
  HttpStatus,
  Param,
} from '@nestjs/common';
import { GetSingleOrderQuery } from './queries/get-single-order/get-single-order.query';

@Controller('api/orders')
export class OrdersController {
  private readonly commandBus: CommandBus;
  private readonly queryBus: QueryBus;

  constructor(commandBus: CommandBus, queryBus: QueryBus) {
    this.commandBus = commandBus;
    this.queryBus = queryBus;
  }

  @Get(':id')
  async getSingleAsync(@Param('id') id: string) {
    const order = await this.queryBus.execute(new GetSingleOrderQuery(id));

    if (order === null) {
      throw new HttpException(
        `Resource with id: ${id} not found`,
        HttpStatus.NOT_FOUND,
      );
    }

    return order;
  }
}

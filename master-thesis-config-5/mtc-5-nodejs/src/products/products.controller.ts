import { CommandBus, QueryBus } from '@nestjs/cqrs';

import { Body, Controller, Post } from '@nestjs/common';
import { CreateProductDto } from './dtos/createProductDto';
import { CreateProductCommand } from './commands/create-product/create-product.command';
import { ApiBody, ApiResponse } from '@nestjs/swagger';

@Controller('api/products')
export class ProductsController {
  private readonly commandBus: CommandBus;
  private readonly queryBus: QueryBus;

  constructor(commandBus: CommandBus, queryBus: QueryBus) {
    this.commandBus = commandBus;
    this.queryBus = queryBus;
  }

  @Post()
  @ApiResponse({
    status: 201,
    description: 'The record has been successfully created.',
  })
  @ApiResponse({ status: 409, description: 'Conflict.' })
  @ApiBody({
    required: true,
  })
  create(@Body() createProductDto: CreateProductDto) {
    return this.commandBus.execute(new CreateProductCommand(createProductDto));
  }
}

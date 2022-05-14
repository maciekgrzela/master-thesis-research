import { CommandHandler, ICommandHandler } from '@nestjs/cqrs';
import { HttpException, HttpStatus } from '@nestjs/common';

import { CreateProductCommand } from './create-product.command';
import { DataAccessRepository } from 'src/core/repositories/data-access.repository';

@CommandHandler(CreateProductCommand)
export class CreateProductHandler
  implements ICommandHandler<CreateProductCommand>
{
  private dataAccessRepository: DataAccessRepository;

  constructor(dataAccessRepository: DataAccessRepository) {
    this.dataAccessRepository = dataAccessRepository;
  }

  async execute(command: CreateProductCommand): Promise<void> {
    const category =
      await this.dataAccessRepository.getSingleProductsCategoryAsync({
        id: command.payload.productsCategoryId,
      });

    if (category === null) {
      throw new HttpException(
        `Resource with id:${command.payload.productsCategoryId} does not exist`,
        HttpStatus.CONFLICT,
      );
    }

    await this.dataAccessRepository.createProduct({
      amount: command.payload.amount,
      name: command.payload.name,
      unit: command.payload.unit,
      ingredients: undefined,
      productsCategory: {
        connect: {
          id: category.id,
        },
      },
    });
  }
}

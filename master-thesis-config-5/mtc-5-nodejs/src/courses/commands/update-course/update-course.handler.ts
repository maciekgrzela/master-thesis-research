import { CommandHandler, ICommandHandler } from '@nestjs/cqrs';
import { HttpException, HttpStatus } from '@nestjs/common';

import { DataAccessRepository } from 'src/core/repositories/data-access.repository';
import { Decimal } from '@internal/prisma/client/runtime';
import { Ingredient } from '@internal/prisma/client';
import { UpdateCourseCommand } from './update-course.command';
import { v4 as uuidv4 } from 'uuid';

@CommandHandler(UpdateCourseCommand)
export class UpdateCourseHandler
  implements ICommandHandler<UpdateCourseCommand>
{
  private dataAccessRepository: DataAccessRepository;

  constructor(dataAccessRepository: DataAccessRepository) {
    this.dataAccessRepository = dataAccessRepository;
  }

  async execute(command: UpdateCourseCommand): Promise<void> {
    const course = await this.dataAccessRepository.getSingleCourseAsync({
      id: command.id,
    });

    if (course === null) {
      throw new HttpException(
        `Resource with id:${command.id} does not exist`,
        HttpStatus.NOT_FOUND,
      );
    }

    const courseCategory =
      await this.dataAccessRepository.getSingleCourseCategoryAsync({
        id: command.payload.coursesCategoryId,
      });

    if (courseCategory === null) {
      throw new HttpException(
        `Resource with id:${command.payload.coursesCategoryId} does not exist`,
        HttpStatus.CONFLICT,
      );
    }

    const createdIngredients: Ingredient[] = [];

    for (const ingredient of command.payload.ingredients) {
      createdIngredients.push({
        amount: Decimal.floor(ingredient.amount),
        courseId: command.id,
        productId: ingredient.productId,
        id: uuidv4(),
      });
    }

    await this.dataAccessRepository.updateCourse({
      data: {
        coursesCategory: {
          connect: courseCategory,
        },
        grossPrice: command.payload.grossPrice,
        name: command.payload.name,
        netPrice: command.payload.netPrice,
        preparationTimeInMinutes: command.payload.preparationTimeInMinutes,
        tax: command.payload.tax,
        ingredients: {
          createMany: {
            data: createdIngredients,
          },
        },
        orderedCourses: undefined,
      },
      where: {
        id: command.id,
      },
    });
  }
}

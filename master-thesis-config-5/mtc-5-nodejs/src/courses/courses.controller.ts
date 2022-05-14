import { CommandBus, QueryBus } from '@nestjs/cqrs';

import { Body, Controller, Param, Put } from '@nestjs/common';
import { UpdateCourseDto } from './dtos/updateCourseDto';
import { UpdateCourseCommand } from './commands/update-course/update-course.command';

@Controller('api/courses')
export class CoursesController {
  private readonly commandBus: CommandBus;
  private readonly queryBus: QueryBus;

  constructor(commandBus: CommandBus, queryBus: QueryBus) {
    this.commandBus = commandBus;
    this.queryBus = queryBus;
  }

  @Put(':id')
  update(@Param('id') id: string, @Body() updateCourseDto: UpdateCourseDto) {
    return this.commandBus.execute(
      new UpdateCourseCommand(id, updateCourseDto),
    );
  }
}

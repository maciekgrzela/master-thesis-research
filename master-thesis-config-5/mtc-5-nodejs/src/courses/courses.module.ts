import { CoursesController } from './courses.controller';
import { CqrsModule } from '@nestjs/cqrs';
import { DataAccessRepository } from 'src/core/repositories/data-access.repository';
import { Module } from '@nestjs/common';
import { PrismaAccessor } from 'src/core/services/prisma.accessor';
import { PrismaWriteAccessor } from 'src/core/services/prisma.write.accessor';
import { UpdateCourseHandler } from './commands/update-course/update-course.handler';

@Module({
  imports: [CqrsModule],
  controllers: [CoursesController],
  providers: [
    PrismaAccessor,
    PrismaWriteAccessor,
    DataAccessRepository,
    UpdateCourseHandler,
  ],
})
export class CoursesModule {}

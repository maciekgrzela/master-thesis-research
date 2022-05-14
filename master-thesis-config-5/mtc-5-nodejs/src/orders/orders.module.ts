import { CqrsModule } from '@nestjs/cqrs';
import { DataAccessRepository } from 'src/core/repositories/data-access.repository';
import { Module } from '@nestjs/common';
import { OrdersController } from './orders.controller';
import { PrismaAccessor } from 'src/core/services/prisma.accessor';
import { PrismaWriteAccessor } from 'src/core/services/prisma.write.accessor';
import { QueryHandlers } from './queries';

@Module({
  imports: [CqrsModule],
  controllers: [OrdersController],
  providers: [
    PrismaAccessor,
    PrismaWriteAccessor,
    DataAccessRepository,
    ...QueryHandlers,
  ],
})
export class OrdersModule {}

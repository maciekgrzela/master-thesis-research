import { BillsController } from './bills.controller';
import { CqrsModule } from '@nestjs/cqrs';
import { DataAccessRepository } from 'src/core/repositories/data-access.repository';
import { Module } from '@nestjs/common';
import { PrismaAccessor } from 'src/core/services/prisma.accessor';
import { PrismaWriteAccessor } from 'src/core/services/prisma.write.accessor';
import { QueryHandlers } from './queries';

@Module({
  imports: [CqrsModule],
  controllers: [BillsController],
  providers: [
    PrismaAccessor,
    PrismaWriteAccessor,
    DataAccessRepository,
    ...QueryHandlers,
  ],
})
export class BillsModule {}

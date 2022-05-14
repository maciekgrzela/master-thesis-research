import { CqrsModule } from '@nestjs/cqrs';
import { DataAccessRepository } from 'src/core/repositories/data-access.repository';
import { DeleteReservationHandler } from './commands/delete-reservation/delete-reservation.handler';
import { Module } from '@nestjs/common';
import { PrismaAccessor } from 'src/core/services/prisma.accessor';
import { PrismaWriteAccessor } from 'src/core/services/prisma.write.accessor';
import { ReservationsController } from './reservations.controller';

@Module({
  imports: [CqrsModule],
  controllers: [ReservationsController],
  providers: [
    PrismaAccessor,
    PrismaWriteAccessor,
    DataAccessRepository,
    DeleteReservationHandler,
  ],
})
export class ReservationsModule {}

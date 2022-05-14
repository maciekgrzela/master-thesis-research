import { CommandBus, QueryBus } from '@nestjs/cqrs';

import { Controller, Delete, Param } from '@nestjs/common';
import { DeleteReservationCommand } from './commands/delete-reservation/delete-reservation.command';

@Controller('api/reservations')
export class ReservationsController {
  private readonly commandBus: CommandBus;
  private readonly queryBus: QueryBus;

  constructor(commandBus: CommandBus, queryBus: QueryBus) {
    this.commandBus = commandBus;
    this.queryBus = queryBus;
  }

  @Delete(':id')
  remove(@Param('id') id: string) {
    return this.commandBus.execute(new DeleteReservationCommand(id));
  }
}

import { CommandHandler, ICommandHandler } from '@nestjs/cqrs';
import { HttpException, HttpStatus } from '@nestjs/common';

import { DataAccessRepository } from 'src/core/repositories/data-access.repository';
import { DeleteReservationCommand } from './delete-reservation.command';

@CommandHandler(DeleteReservationCommand)
export class DeleteReservationHandler
  implements ICommandHandler<DeleteReservationCommand>
{
  private dataAccessRepository: DataAccessRepository;

  constructor(dataAccessRepository: DataAccessRepository) {
    this.dataAccessRepository = dataAccessRepository;
  }

  async execute(command: DeleteReservationCommand): Promise<void> {
    const reservation =
      await this.dataAccessRepository.getSingleReservationAsync({
        id: command.id,
      });

    if (reservation === null) {
      throw new HttpException(
        `Resource with id:${command.id} does not exist`,
        HttpStatus.NOT_FOUND,
      );
    }

    await this.dataAccessRepository.deleteReservation({
      id: command.id,
    });
  }
}

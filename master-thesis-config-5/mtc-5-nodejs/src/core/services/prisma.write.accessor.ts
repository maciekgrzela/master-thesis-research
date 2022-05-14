import { INestApplication, Injectable, OnModuleInit } from '@nestjs/common';

import { PrismaClient } from '@internal/prisma/client';

@Injectable()
export class PrismaWriteAccessor extends PrismaClient implements OnModuleInit {
  async onModuleInit() {
    await this.$connect();
  }

  async enableShutdownHooks(app: INestApplication) {
    this.$on('beforeExit', async () => {
      await app.close();
    });
  }
}

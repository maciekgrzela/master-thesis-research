import { CqrsModule } from '@nestjs/cqrs';
import { CreateProductHandler } from './commands/create-product/create-product.handler';
import { DataAccessRepository } from 'src/core/repositories/data-access.repository';
import { Module } from '@nestjs/common';
import { PrismaAccessor } from 'src/core/services/prisma.accessor';
import { PrismaWriteAccessor } from 'src/core/services/prisma.write.accessor';
import { ProductsController } from './products.controller';

@Module({
  imports: [CqrsModule],
  controllers: [ProductsController],
  providers: [
    PrismaAccessor,
    PrismaWriteAccessor,
    DataAccessRepository,
    CreateProductHandler,
  ],
})
export class ProductsModule {}

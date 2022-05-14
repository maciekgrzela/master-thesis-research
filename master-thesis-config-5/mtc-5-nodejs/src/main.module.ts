import { BillsModule } from './bills/bills.module';
import { CoursesModule } from './courses/courses.module';
import { Module } from '@nestjs/common';
import { OrdersModule } from './orders/orders.module';
import { ProductsModule } from './products/products.module';
import { ReservationsModule } from './reservations/reservations.module';

@Module({
  imports: [
    BillsModule,
    CoursesModule,
    OrdersModule,
    ProductsModule,
    ReservationsModule,
  ],
  controllers: [],
  providers: [],
})
export class MainModule {}

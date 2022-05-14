import {
  Bill,
  Course,
  CoursesCategory,
  Order,
  Prisma,
  Product,
  ProductsCategory,
  Reservation,
} from '@prisma/client';
import {
  Course as CourseWrite,
  Ingredient as IngredientWrite,
  Prisma as PrismaWrite,
  Product as ProductWrite,
  Reservation as ReservationWrite,
} from '@internal/prisma/client';

import { Injectable } from '@nestjs/common';
import { PrismaAccessor } from '../services/prisma.accessor';
import { PrismaWriteAccessor } from '../services/prisma.write.accessor';

@Injectable()
export class DataAccessRepository {
  private prisma: PrismaAccessor;
  private prismaWrite: PrismaWriteAccessor;

  constructor(prisma: PrismaAccessor, prismaWrite: PrismaWriteAccessor) {
    this.prisma = prisma;
    this.prismaWrite = prismaWrite;
  }

  async getSingleOrderAsync(
    orderWhereUniqueInput: Prisma.OrderWhereUniqueInput,
  ): Promise<Order | null> {
    return this.prisma.order.findUnique({
      where: orderWhereUniqueInput,
      include: {
        bills: true,
        orderedCourses: true,
        statusEntries: true,
        table: true,
        user: true,
      },
    });
  }

  async getSingleCourseAsync(
    courseWhereUniqueInput: Prisma.CourseWhereUniqueInput,
  ): Promise<Course | null> {
    return this.prisma.course.findUnique({
      where: courseWhereUniqueInput,
    });
  }

  async getSingleProductAsync(
    productWhereUniqueInput: Prisma.ProductWhereUniqueInput,
  ): Promise<Product | null> {
    return this.prisma.product.findUnique({
      where: productWhereUniqueInput,
    });
  }

  async getSingleCourseCategoryAsync(
    courseCategoryWhereUniqueInput: Prisma.CoursesCategoryWhereUniqueInput,
  ): Promise<CoursesCategory | null> {
    return this.prisma.coursesCategory.findUnique({
      where: courseCategoryWhereUniqueInput,
    });
  }

  async getSingleProductsCategoryAsync(
    productsCategoryWhereUniqueInput: Prisma.ProductsCategoryWhereUniqueInput,
  ): Promise<ProductsCategory | null> {
    return this.prisma.productsCategory.findUnique({
      where: productsCategoryWhereUniqueInput,
    });
  }

  async getSingleReservationAsync(
    reservationWhereUniqueInput: Prisma.ReservationWhereUniqueInput,
  ): Promise<Reservation | null> {
    return this.prisma.reservation.findUnique({
      where: reservationWhereUniqueInput,
    });
  }

  async getAllBillsAsync(params: {
    skip?: number;
    take?: number;
    cursor?: Prisma.BillWhereUniqueInput;
    where?: Prisma.BillWhereInput;
    orderBy?: Prisma.BillOrderByWithRelationInput;
  }): Promise<Bill[]> {
    const { skip, take, cursor, where, orderBy } = params;
    return this.prisma.bill.findMany({
      skip,
      take: parseInt(take.toString()),
      cursor,
      where,
      orderBy,
      include: {
        customer: true,
        order: true,
        orderedCourses: true,
      },
    });
  }

  async createProduct(
    data: PrismaWrite.ProductCreateInput,
  ): Promise<ProductWrite> {
    return this.prismaWrite.product.create({
      data,
    });
  }

  async createIngredient(
    data: PrismaWrite.IngredientCreateInput,
  ): Promise<IngredientWrite> {
    return this.prismaWrite.ingredient.create({
      data,
    });
  }

  async deleteReservation(
    where: PrismaWrite.ReservationWhereUniqueInput,
  ): Promise<ReservationWrite> {
    return this.prismaWrite.reservation.delete({
      where,
    });
  }

  async updateCourse(params: {
    where: PrismaWrite.CourseWhereUniqueInput;
    data: PrismaWrite.CourseUpdateInput;
  }): Promise<CourseWrite> {
    const { where, data } = params;
    return this.prismaWrite.course.update({
      data,
      where,
    });
  }
}
